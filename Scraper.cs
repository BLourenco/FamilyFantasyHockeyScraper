using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlDownload
{
    static class Scraper
    {
        public static int LeagueIDCurrent { get; set; }
        public static int LeagueIDPrevious { get; set; }
        public static int LeagueYearPrevious { get; set; }

        public static int numberOfDraftRounds;

        public static List<Player> players = new List<Player>();

        private const int NUMBER_OF_PAGES_TO_SCRAPE = 30; // 30 pages * 25 players per page = Top 625 players
        private static int numberOfPlayersPerPage = 25; // On the first page, count the players and overwrite this to try and keep this future-proof.
        
        private static Dictionary<int, string> skaterHeaders = new Dictionary<int, string>();
        private static Dictionary<int, string> goalieHeaders = new Dictionary<int, string>();
        private static Dictionary<int, string> searchSkaterHeaders = new Dictionary<int, string>();
        private static Dictionary<int, string> searchGoalieHeaders = new Dictionary<int, string>();



        public enum PlayerType
        {
            Skater,
            Goalie
        }

        // URLs to be formatted, with the 1st parameter being the appropriate League ID, and the 2nd parameter the previous year.
        private const string URL_OWNED_SKATERS = "https://hockey.fantasysports.yahoo.com/hockey/{0}/players?&sort=AR&sdir=1&status=T&pos=P&stat1=S_S_{1}&jsenabled=1";
        private const string URL_OWNED_GOALIES = "https://hockey.fantasysports.yahoo.com/hockey/{0}/players?&sort=AR&sdir=1&status=T&pos=G&stat1=S_S_{1}&jsenabled=1";
        private const string URL_DRAFTED_PLAYERS = "https://archive.fantasysports.yahoo.com/archive/nhl/{1}/{0}/draftresults?drafttab=round";
        private const string URL_DROPPED_PLAYERS = "https://archive.fantasysports.yahoo.com/archive/nhl/{1}/{0}/transactions?filter=drop";
        // This URL requires a 3rd parameter for the player name to be searched.
        private const string URL_SEARCH_PLAYER = "https://hockey.fantasysports.yahoo.com/hockey/{0}/playersearch?&search={2}&stat1=S_S_{1}&jsenabled=1";

        private const string XPATH_PLAYERS_TABLE_HEADERS = "//div[@id='players-table']//div[@class='players']//table//thead//tr[@class='Alt Last']//th//div";
        private const string XPATH_PLAYERS_TABLE_PLAYER_ROWS = "//div[@id='players-table']//div[@class='players']//table//tbody//tr";
        private const string XPATH_TRANSACTIONS_TABLE_DROPS = "//div[@id='transactions']//div//div//table//tbody//tr";
        private const string XPATH_DRAFT_RESULTS_TABLE_PLAYERS = "//div[@id='drafttables']//div//div//table//tbody//tr";
        private const string XPATH_DRAFT_RESULTS_TABLE_ROUNDS = "//div[@id='drafttables']//div//div";

        public static void ScrapeOwnedPlayers(PlayerType playerType)
        {
            string URLFormat = playerType == PlayerType.Skater ? URL_OWNED_SKATERS : URL_OWNED_GOALIES;
            string baseURL = string.Format(URLFormat, LeagueIDCurrent, LeagueYearPrevious);
            string completeURL;

            for (int pages = 0; pages < NUMBER_OF_PAGES_TO_SCRAPE; pages++) 
            {        
                // Set the page URL to scrape
                if (pages == 0) // First page has no additional parameters
                {
                    completeURL = baseURL;
                }
                else // add parameter to specificy which page of the table to scrape
                {
                    completeURL = baseURL + "&count=" + (pages * numberOfPlayersPerPage);
                }

                // Load page
                HtmlWeb getHtmlWeb = new HtmlWeb();
                HtmlDocument document = getHtmlWeb.Load(completeURL);

                // Page is loaded

                // Scrape the table headers
                if (pages == 0)
                {                    
                    var headerNodes = document.DocumentNode.SelectNodes(XPATH_PLAYERS_TABLE_HEADERS);

                    int headerIndex = 0;

                    Dictionary<int, string> headers = playerType == PlayerType.Skater ? skaterHeaders : goalieHeaders;
                    foreach (HtmlNode headerNode in headerNodes)
                    {
                        string header = headerNode.InnerText;

                        // Erase garbage headers
                        header = header.Replace("&nbsp;", "");
                        header = header.Replace("&#xe002;", "");
                        //header = header.Replace("*", "");
                        if (header.Contains("Opp:"))
                            header = "";

                        headers.Add(headerIndex++, header);
                    }
                }

                // Go through the stats for each player on this page
                var playerNodes = document.DocumentNode.SelectNodes(XPATH_PLAYERS_TABLE_PLAYER_ROWS);
                if (playerNodes != null)
                {
                    foreach (HtmlNode playerNode in playerNodes)
                    {                        
                        players.Add(ScrapePlayerNode(playerNode, playerType));  
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private static Player ScrapePlayerNode(HtmlNode playerNode, PlayerType playerType, bool isSearchedPlayer = false)
        {
            var columns = playerNode.Descendants("td");

            Player player = null;
            switch (playerType)
            {
                case PlayerType.Skater:
                    player = new Skater();
                    break;
                case PlayerType.Goalie:
                    player = new Goalie();
                    break;
            }

            Dictionary<int, string> headers = playerType == PlayerType.Skater ? skaterHeaders : goalieHeaders;

            int playerTableColumnCounter = 0;
            foreach (HtmlNode col in columns)
            {
                // Dirty fix for the headers of the player table missing the GP* column.
                if (isSearchedPlayer && playerTableColumnCounter == 5) playerTableColumnCounter++;

                if (headers.TryGetValue(playerTableColumnCounter, out string header))
                {
                    switch (header)
                    {
                        case "":
                            break;
                        case Skater.KEY_NAME_TEAM_POSITION:
                        case Goalie.KEY_NAME_TEAM_POSITION:
                            player.Name = col.SelectSingleNode(".//a[@class='Nowrap name F-link']").InnerText;
                            string[] teamAndPosition = col.SelectSingleNode(".//span[@class='Fz-xxs']").InnerText.Split('-');
                            player.TeamNHL = teamAndPosition[0].Trim();
                            player.Positions = "\"" + teamAndPosition[1].Trim() + "\""; // Added escaped quotes because commas are used to list positions and is our delimiter character.
                            break;
                        case Player.KEY_OWNER:
                        case Player.KEY_GAMES_PLAYED:
                        case Player.KEY_FANTASY_POINTS:
                        case Player.KEY_PRESEASON_RANK:
                        case Player.KEY_CURRENT_RANK:
                        case Player.KEY_OWNED_PERCENTAGE:
                            player.SetStatByHeader(header, col.SelectSingleNode(".//div").InnerText);
                            break;
                        case Skater.KEY_AVERAGE_TIME_ON_ICE:
                        case Skater.KEY_GOALS:
                        case Skater.KEY_ASSISTS:
                        case Skater.KEY_POINTS:
                        case Skater.KEY_PLUS_MINUS:
                        case Skater.KEY_PENALTIES_IN_MINUTES:
                        case Skater.KEY_POWERPLAY_GOALS:
                        case Skater.KEY_POWERPLAY_ASSISTS:
                        case Skater.KEY_POWERPLAY_POINTS:
                        case Skater.KEY_SHORTHANDED_GOALS:
                        case Skater.KEY_SHORTHANDED_ASSISTS:
                        case Skater.KEY_SHORTHANDED_POINTS:
                        case Skater.KEY_GAME_WINNING_GOALS:
                        case Skater.KEY_SHOTS_ON_GOAL:
                        case Skater.KEY_SHOOTING_PERCENTAGE:
                        case Skater.KEY_FACEOFFS_WON:
                        case Skater.KEY_FACEOFFS_LOST:
                        case Skater.KEY_HITS:
                        case Skater.KEY_BLOCKS:
                            ((Skater)player).SetStatByHeader(header, col.SelectSingleNode(".//div").InnerText);
                            break;
                        case Goalie.KEY_TIME_ON_ICE:
                        case Goalie.KEY_GAMES_STARTED:
                        case Goalie.KEY_WINS:
                        case Goalie.KEY_LOSSES:
                        case Goalie.KEY_SHUTOUTS:
                        case Goalie.KEY_SHOTS_AGAINST:
                        case Goalie.KEY_SAVES:
                        case Goalie.KEY_GOALS_AGAINST:
                        case Goalie.KEY_GOALS_AGAINST_AVERAGE:
                        case Goalie.KEY_SAVE_PERCENTAGE:
                            ((Goalie)player).SetStatByHeader(header, col.SelectSingleNode(".//div").InnerText);
                            break;
                        default:

                            break;
                    }
                }

                playerTableColumnCounter++;
            }

            return player;
        }        

        public static void ScrapeDroppedPlayers()
        {
            string baseUrl = string.Format(URL_DROPPED_PLAYERS, LeagueIDPrevious, LeagueYearPrevious);
            string pageUrl;

            for (int pages = 0; pages < 30; pages++)
            {                
                if (pages == 0)
                {
                    pageUrl = baseUrl;
                }
                else
                {
                    pageUrl = baseUrl + "&mid=&count=" + (pages * 25);
                }

                HtmlWeb getHtmlWeb = new HtmlWeb();
                HtmlDocument document = getHtmlWeb.Load(pageUrl);

                // Page is loaded

                // Go through the stats for each player on this page
                var droppedNodes = document.DocumentNode.SelectNodes(XPATH_TRANSACTIONS_TABLE_DROPS);
                if (droppedNodes != null)
                {
                    foreach (HtmlNode droppedNode in droppedNodes)
                    {        
                        var items = droppedNode.Descendants("td");

                        if (items.ElementAt(0).InnerText == "No recent transactions")
                            return;

                        var itemPlayer = items.ElementAt(1).Descendants("a").ElementAt(0).InnerText;

                        Player p = players.Find(x => x.Name.Contains(itemPlayer));

                        if (p != null)
                        {
                            p.WasDropped = true;
                        }
                    }
                }
            }
        }

        public static void ScrapeDraftRounds()
        {
            String baseUrl = string.Format(URL_DRAFTED_PLAYERS, LeagueIDPrevious, LeagueYearPrevious);

            HtmlWeb getHtmlWeb = new HtmlWeb();
            HtmlDocument document = getHtmlWeb.Load(baseUrl);

            // Page is loaded, go through the stats for each player
            var playerNodes = document.DocumentNode.SelectNodes(XPATH_DRAFT_RESULTS_TABLE_PLAYERS);
            var draftRoundNodes = document.DocumentNode.SelectNodes(XPATH_DRAFT_RESULTS_TABLE_ROUNDS);
            var picksPerRound = playerNodes.Count / draftRoundNodes.Count;
            int playerCounter = 0;

            numberOfDraftRounds = draftRoundNodes.Count;

            foreach (HtmlNode player in playerNodes)
            {
                var items = player.Descendants("td");

                if (items.ElementAt(1).InnerText == "--empty--")
                    return;

                var itemPlayer = items.ElementAt(1).Descendants("a").ElementAt(0).InnerText;

                Player p = players.Find(x => x.Name.Contains(itemPlayer));

                
                var itemPlayerExtra = items.ElementAt(1).Descendants("span");
                var itemDraftedBy = items.ElementAt(2).GetAttributeValue("title", "");

                if (itemPlayerExtra.Count() >= 2) // Checks if Keeper icon exists
                {
                    if (p == null)
                    {
                        // This player was a Keeper, but is no longer on a team, but we still need their stats to determine Keep Costs.
                        // Search for this player to get their info, and add it to the list of players.
                        // This player will be excluded from the final output.
                        p = ScrapeSearchedPlayer(itemPlayer);
                        players.Add(p);
                    }

                    p.IsKeeper = true;
                }

                if(p != null)
                {
                    p.DraftedBy = itemDraftedBy;
                    p.RoundDrafted = p.IsKeeper ? -1 : playerCounter / picksPerRound + 1;
                }

                playerCounter++;
            }
        }

        // Currently won't be able to differentiate between multiple players of the same full name.
        private static Player ScrapeSearchedPlayer(string playerName)
        {
            
            string baseUrl = string.Format(URL_SEARCH_PLAYER, LeagueIDCurrent, LeagueYearPrevious, playerName);
            
            HtmlWeb getHtmlWeb = new HtmlWeb();
            HtmlDocument document = getHtmlWeb.Load(baseUrl);

            // Page is loaded
            
            // Go through the stats for the first player on this page
            var nodes = document.DocumentNode.SelectNodes(XPATH_PLAYERS_TABLE_PLAYER_ROWS);
            if (nodes != null)
            {                
                var items = nodes[0].Descendants("td");

                var itemPlayerPosition = items.ElementAt(1).SelectSingleNode(".//span[@class='Fz-xxs']").InnerText.Split('-')[1].Trim();

                PlayerType type = itemPlayerPosition == "G" ? PlayerType.Goalie : PlayerType.Skater;
                
                return ScrapePlayerNode(nodes[0], type, true);                
            }
            else
            {
                //Player not found.
                throw new ArgumentException("No results found for search term \"" + playerName + "\"");
            }
            
        }
    }
}
