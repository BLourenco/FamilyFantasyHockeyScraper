using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhlDownload
{
    class Skater : Player
    {
        // Currently unused
        public enum StatCategory
        {
            Goals,
            Assists,
            Points,
            PlusMinus,
            PenaltyMinutes,
            PowerplayGoals,
            PowerplayAssists,
            PowerplayPoints,
            ShorthandedGoals,
            ShorthandedAssists,
            ShorthandedPoints,
            GameWinningGoals,
            ShotsOnGoal,
            ShootingPercentage,
            FaceoffsWon,
            FaceoffsLost,
            Hits,
            Blocks
        }

        public const string KEY_NAME_TEAM_POSITION = "Forwards/Defensemen";
        public const string KEY_AVERAGE_TIME_ON_ICE = "TOI/G*"; // The astrisk denotes a non-scoring stat.

        public const string KEY_GOALS = "G";
        public const string KEY_ASSISTS = "A";
        public const string KEY_POINTS = "P";
        public const string KEY_PLUS_MINUS = "+/-";
        public const string KEY_PENALTIES_IN_MINUTES = "PIM";
        public const string KEY_POWERPLAY_GOALS = "PPG";
        public const string KEY_POWERPLAY_ASSISTS = "PPA";
        public const string KEY_POWERPLAY_POINTS = "PPP";
        public const string KEY_SHORTHANDED_GOALS = "SHG";
        public const string KEY_SHORTHANDED_ASSISTS = "SHA";
        public const string KEY_SHORTHANDED_POINTS = "SHP";
        public const string KEY_GAME_WINNING_GOALS = "GWG";
        public const string KEY_SHOTS_ON_GOAL = "SOG";
        public const string KEY_SHOOTING_PERCENTAGE = "SH%";
        public const string KEY_FACEOFFS_WON = "FOW";
        public const string KEY_FACEOFFS_LOST = "FOL";
        public const string KEY_HITS = "HIT";
        public const string KEY_BLOCKS = "BLK";

        public int StatGoals { get; set; }
        public int StatAssists { get; set; }
        public int StatPoints { get; set; }
        public int StatPlusMinus { get; set; }
        public int StatPenaltyMinutes { get; set; }
        public int StatPowerplayGoals { get; set; }
        public int StatPowerplayAssists { get; set; }
        public int StatPowerplayPoints { get; set; }
        public int StatShorthandedGoals { get; set; }
        public int StatShorthandedAssists { get; set; }
        public int StatShorthandedPoints { get; set; }
        public int StatGameWinningGoals { get; set; }
        public int StatShotsOnGoal { get; set; }
        public float StatShootingPercentage { get; set; }
        public int StatFaceoffsWon { get; set; }
        public int StatFaceoffsLost { get; set; }
        public int StatHits { get; set; }
        public int StatBlocks { get; set; }

        public new void SetStatByHeader(string header, string value)
        {
            switch (header)
            {
                case KEY_AVERAGE_TIME_ON_ICE:
                    if (value.Equals("-"))
                        StatTimeOnIce = "00:00";
                    else
                        StatTimeOnIce = value;
                    break;
                case KEY_GOALS:
                    StatGoals = int.Parse(value);
                    break;
                case KEY_ASSISTS:
                    StatAssists = int.Parse(value);
                    break;
                case KEY_POINTS:
                    StatPoints = int.Parse(value);
                    break;
                case KEY_PLUS_MINUS:
                    StatPlusMinus = int.Parse(value);
                    break;
                case KEY_PENALTIES_IN_MINUTES:
                    StatPenaltyMinutes = int.Parse(value);
                    break;
                case KEY_POWERPLAY_GOALS:
                    StatPowerplayGoals = int.Parse(value);
                    break;
                case KEY_POWERPLAY_ASSISTS:
                    StatPowerplayAssists = int.Parse(value);
                    break;
                case KEY_POWERPLAY_POINTS:
                    StatPowerplayPoints = int.Parse(value);
                    break;
                case KEY_SHORTHANDED_GOALS:
                    StatShorthandedGoals = int.Parse(value);
                    break;
                case KEY_SHORTHANDED_ASSISTS:
                    StatShorthandedAssists = int.Parse(value);
                    break;
                case KEY_SHORTHANDED_POINTS:
                    StatShorthandedPoints = int.Parse(value);
                    break;
                case KEY_GAME_WINNING_GOALS:
                    StatGameWinningGoals = int.Parse(value);
                    break;
                case KEY_SHOTS_ON_GOAL:
                    StatShotsOnGoal = int.Parse(value);
                    break;
                case KEY_SHOOTING_PERCENTAGE:
                    StatShootingPercentage = float.Parse(value);
                    break;
                case KEY_FACEOFFS_WON:
                    StatFaceoffsWon = int.Parse(value);
                    break;
                case KEY_FACEOFFS_LOST:
                    StatFaceoffsLost = int.Parse(value);
                    break;
                case KEY_HITS:
                    StatHits = int.Parse(value);
                    break;
                case KEY_BLOCKS:
                    StatBlocks = int.Parse(value);
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
                // Skater stats
                case KEY_GOALS:
                    return StatGoals;
                case KEY_ASSISTS:
                    return StatAssists;
                case KEY_POINTS:
                    return StatPoints;
                case KEY_PLUS_MINUS:
                    return StatPlusMinus;
                case KEY_PENALTIES_IN_MINUTES:
                    return StatPenaltyMinutes;
                case KEY_POWERPLAY_GOALS:
                    return StatPowerplayGoals;
                case KEY_POWERPLAY_ASSISTS:
                    return StatPowerplayAssists;
                case KEY_POWERPLAY_POINTS:
                    return StatPowerplayPoints;
                case KEY_SHORTHANDED_GOALS:
                    return StatShorthandedGoals;
                case KEY_SHORTHANDED_ASSISTS:
                    return StatShorthandedAssists;
                case KEY_SHORTHANDED_POINTS:
                    return StatShorthandedPoints;
                case KEY_GAME_WINNING_GOALS:
                    return StatGameWinningGoals;
                case KEY_SHOTS_ON_GOAL:
                    return StatShotsOnGoal;
                case KEY_SHOOTING_PERCENTAGE:
                    return StatShootingPercentage;
                case KEY_FACEOFFS_WON:
                    return StatFaceoffsWon;
                case KEY_FACEOFFS_LOST:
                    return StatFaceoffsLost;
                case KEY_HITS:
                    return StatHits;
                case KEY_BLOCKS:
                    return StatBlocks;
                default:
                    //throw new ArgumentException("Header does not exist.", "header");
                    return null;
            }
        }
    }
}
