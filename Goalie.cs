using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlDownload
{
    class Goalie : Player
    {
        // Currently unused
        public enum StatCategory
        {
            GamesStarted,
            Wins,
            Losses,
            Shutouts,
            ShotsAgainst,
            Saves,
            GoalsAgainst,
            GoalsAgainstAverage,
            SavePercentage
        }

        public const string KEY_NAME_TEAM_POSITION = "Goaltenders";
        public const string KEY_TIME_ON_ICE = "TOI*"; // The astrisk denotes a non-scoring stat.

        public const string KEY_GAMES_STARTED = "GS";
        public const string KEY_WINS = "W";
        public const string KEY_LOSSES = "L";
        public const string KEY_SHUTOUTS = "SHO";
        public const string KEY_SHOTS_AGAINST = "SA";
        public const string KEY_SAVES = "SV";
        public const string KEY_GOALS_AGAINST = "GA";
        public const string KEY_GOALS_AGAINST_AVERAGE = "GAA";
        public const string KEY_SAVE_PERCENTAGE = "SV%";

        public int StatGamesStarted { get; set; }
        public int StatWins { get; set; }
        public int StatLosses { get; set; }
        public int StatShutouts { get; set; }
        public int StatShotsAgainst { get; set; }
        public int StatSaves { get; set; }
        public int StatGoalsAgaisnt { get; set; }
        public float StatGoalsAgainstAverage { get; set; }
        public float StatSavePercentage { get; set; }

        public new void SetStatByHeader(string header, string value)
        {
            value = value == "-" ? "0" : value;

            switch (header)
            {
                case KEY_GAMES_STARTED:
                    StatGamesStarted = int.Parse(value);
                    break;
                case KEY_WINS:
                    StatWins = int.Parse(value);
                    break;
                case KEY_LOSSES:
                    StatLosses = int.Parse(value);
                    break;
                case KEY_SHUTOUTS:
                    StatShutouts = int.Parse(value);
                    break;
                case KEY_SHOTS_AGAINST:
                    StatShotsAgainst = int.Parse(value);
                    break;
                case KEY_SAVES:
                    StatSaves = int.Parse(value);
                    break;
                case KEY_GOALS_AGAINST:
                    StatGoalsAgaisnt = int.Parse(value);
                    break;
                case KEY_GOALS_AGAINST_AVERAGE:
                    StatGoalsAgainstAverage = float.Parse(value);
                    break;
                case KEY_SAVE_PERCENTAGE:
                    StatSavePercentage = float.Parse(value);
                    break;
                default:
                    //throw new ArgumentException("Header does not exist.", "header");
                    break;
            }
        }

        public object GetStatByHeader(string header)
        {
            switch (header)
            {
                case KEY_GAMES_STARTED:
                    return StatGamesStarted;
                case KEY_WINS:
                    return StatWins;
                case KEY_LOSSES:
                    return StatLosses;
                case KEY_SHUTOUTS:
                    return StatShutouts;
                case KEY_SHOTS_AGAINST:
                    return StatShotsAgainst;
                case KEY_SAVES:
                    return StatSaves;
                case KEY_GOALS_AGAINST:
                    return StatGoalsAgaisnt;
                case KEY_GOALS_AGAINST_AVERAGE:
                    return StatGoalsAgainstAverage;
                case KEY_SAVE_PERCENTAGE:
                    return StatSavePercentage;
                default:
                    //throw new ArgumentException("Header does not exist.", "header");
                    return null;
            }
        }
    }
}
