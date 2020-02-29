using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlDownload
{
    static class CSVOutput
    {
        public static string csvFileName;
        public static int numberOfKeepersPerTeam = 3;
        public static int keepCostPenalty = 1;

        public const string HEADER_OWNER = "Owner";
        public const string HEADER_DRAFTED_BY = "Drafted By";
        public const string HEADER_ROUND_DRAFTED = "Round Drafted";
        public const string HEADER_NAME = "Name";
        public const string HEADER_CURRENT_TEAM = "Current Team";
        public const string HEADER_POSITIONS = "Position(s)";
        public const string HEADER_FANTASY_POINTS = "Fan Pts";
        public const string HEADER_KEEP_COST = "Pick Lost If Kept";
        public const string HEADER_PREVIOUS_RANK = "Last Year's Rank";
        public const string HEADER_CURRENT_RANK = "Current Yahoo! Rank";

        public const string DELIMITER = ",";

        public const string MARK_KEEPER = "K";
        public const string MARK_FREE_AGENT = "Free Agent";
        public const string MARK_CAN_NOT_KEEP = "Can't Keep";

        public static void GenerateCSV()
        {
            Scraper.players = Scraper.players.OrderBy(a => a.TeamFantasy)
                .ThenBy(a => a.RoundDrafted)
                .ThenByDescending(a => a.StatFantasyPoints)
                .ToList();

            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(csvFileName))
            {
                string headers = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{0}{10}",
                    DELIMITER,
                    HEADER_OWNER,
                    HEADER_DRAFTED_BY,
                    HEADER_ROUND_DRAFTED,
                    HEADER_NAME,
                    HEADER_CURRENT_TEAM,
                    HEADER_POSITIONS,
                    HEADER_FANTASY_POINTS,
                    HEADER_KEEP_COST,
                    HEADER_PREVIOUS_RANK,
                    HEADER_CURRENT_RANK
                    );

                file.WriteLine(headers);

                foreach (Player p in Scraper.players)
                {
                    if (p.TeamFantasy == "FA") continue; // Skip the dropped keepers.

                    file.Write(p.TeamFantasy + DELIMITER);
                    file.Write(p.DraftedBy + DELIMITER);
                    file.Write(FormatRoundDrafted(p) + DELIMITER);
                    file.Write(p.Name + DELIMITER);
                    file.Write(p.TeamNHL + DELIMITER);
                    file.Write(p.Positions + DELIMITER);
                    file.Write(p.StatFantasyPoints + DELIMITER);
                    file.Write(p.PickLostIfKept == int.MinValue ? MARK_CAN_NOT_KEEP + DELIMITER : p.PickLostIfKept + DELIMITER);
                    file.Write(p.StatCurrentRank + DELIMITER);
                    file.Write(p.StatPreSeasonRank + DELIMITER);

                    file.WriteLine("");
                }
            }
        }

        // Overwrite the player's draft position in the following cases:
        // If the player was a Keeper and wasn't dropped, mark is as "K"
        // If the player was never drafted or was dropped, mark as "Free Agent"
        private static string FormatRoundDrafted(Player p)
        {
            return p.RoundDrafted == -1 && !p.WasDropped ? MARK_KEEPER :
                   p.RoundDrafted == int.MaxValue || p.WasDropped ? MARK_FREE_AGENT :
                   p.RoundDrafted.ToString();
        }

        public static void DetermineKeeperPickCosts()
        {
            Scraper.players = Scraper.players.OrderBy(a => a.DraftedBy)
                .ThenBy(a => a.StatPreSeasonRank)
                .ToList();

            Dictionary<string, int> teamKeeperCounts = new Dictionary<string, int>();
            foreach (Player p in Scraper.players)
            {
                if (p.IsKeeper)
                {
                    if (p.WasDropped)
                    {
                        p.PickLostIfKept = Scraper.numberOfDraftRounds;
                    }
                    else
                    {
                        if (teamKeeperCounts.TryGetValue(p.DraftedBy, out int count))
                        {
                            teamKeeperCounts[p.DraftedBy] = count + 1;
                            p.PickLostIfKept = count + 1;
                        }
                        else
                        {
                            teamKeeperCounts.Add(p.DraftedBy, 0);
                            p.PickLostIfKept = int.MinValue;
                        }
                    }
                }
                else if (p.DraftedBy == null || p.WasDropped)
                {
                    p.PickLostIfKept = Scraper.numberOfDraftRounds;
                }
                else
                {
                    p.PickLostIfKept = p.RoundDrafted - keepCostPenalty + numberOfKeepersPerTeam;
                }
            }
        }
    }
}
