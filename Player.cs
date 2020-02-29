using System;
using System.Collections.Generic;

public class Player
{
    public Player() { }
    public Player(string name, string nhlTeam, string positions)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.TeamNHL = nhlTeam ?? throw new ArgumentNullException(nameof(nhlTeam));
        this.Positions = positions ?? throw new ArgumentNullException(nameof(positions));
    }

    public const string KEY_OWNER = "Owner";
    public const string KEY_GAMES_PLAYED = "GP*"; // The astrisk denotes a non-scoring stat.
    public const string KEY_FANTASY_POINTS = "Fan Pts";
    public const string KEY_PRESEASON_RANK = "Pre-Season";
    public const string KEY_CURRENT_RANK = "Current";
    public const string KEY_OWNED_PERCENTAGE = "% Owned";

    public string Name { get; set; }
    public string TeamNHL { get; set; }
    public string TeamFantasy { get; set; }
    public string Positions { get; set; }

    // General Stats
    public int StatGamesPlayed { get; set; }
    public float StatFantasyPoints { get; set; }
    public float StatOwnedPercentage { get; set; }
    public int StatPreSeasonRank { get; set; }
    public int StatCurrentRank { get; set; }
    public string StatTimeOnIce { get; set; }

    // Draft/Keeper Info
    public int RoundDrafted { get; set; } = int.MaxValue;
    public string DraftedBy { get; set; }
    public bool IsKeeper { get; set; }
    public bool WasDropped { get; set; } // Used to determing if the "Keep Cost" of a drafted player should be minimized.
    public int PickLostIfKept { get; set; } = -1;

    public void SetStatByHeader(string header, string value)
    {
        switch (header)
        {
            case KEY_OWNER:
                TeamFantasy = value; // Originally used ".//div//a", revert back if problem occurs.
                break;
            case KEY_GAMES_PLAYED:
                StatGamesPlayed = int.Parse(value);
                break;
            case KEY_FANTASY_POINTS:
                StatFantasyPoints = float.Parse(value);
                break;
            case KEY_PRESEASON_RANK:
                StatPreSeasonRank = int.Parse(value);
                break;
            case KEY_CURRENT_RANK:
                StatCurrentRank = int.Parse(value);
                break;
            case KEY_OWNED_PERCENTAGE:
                if (value.Equals("-"))
                {
                    StatOwnedPercentage = 0f;
                }
                else
                {
                    StatOwnedPercentage = float.Parse(value.Replace("%", "")) * 0.01f;
                }
                break;
        }
    }

    public object GetStatByHeader(string header)
    {
        switch (header)
        {
            case KEY_OWNER:
                return TeamFantasy;
            case KEY_GAMES_PLAYED:
                return StatGamesPlayed;
            case KEY_FANTASY_POINTS:
                return StatFantasyPoints;
            case KEY_PRESEASON_RANK:
                return StatPreSeasonRank;
            case KEY_CURRENT_RANK:
                return StatCurrentRank;
            case KEY_OWNED_PERCENTAGE:
                return KEY_OWNED_PERCENTAGE;
            default:
                //throw new ArgumentException("Header does not exist.", "header");
                return null;
        }
    }
}
