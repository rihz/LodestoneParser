using LodestoneParser.Character;
using LodestoneParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Logic
{
    /// <summary>
    /// Responsible for parsing strings and spitting out a relevant JobEnum.
    /// </summary>
    public static class JobParser
    {
        #region Public Static Methods
        /// <summary>
        /// Parse a string to a JobEnum.
        /// </summary>
        /// <param name="toParse">String to be parsed.</param>
        /// <returns>JobEnum based on the parsed string.</returns>
        public static JobEnum Parse(string toParse)
        {
            switch (toParse)
            {
                case "Gladiator":
                case "GLD":
                    return JobEnum.GLD;
                case "Warrior":
                case "WAR":
                    return JobEnum.WAR;
                case "Dark Knight":
                case "DRK":
                    return JobEnum.DRK;
                case "White Mage":
                case "WHM":
                    return JobEnum.WHM;
                case "Scholar":
                case "SCH":
                    return JobEnum.SCH;
                case "Astrologian":
                case "AST":
                    return JobEnum.AST;
                case "Monk":
                case "MNK":
                    return JobEnum.MNK;
                case "Dragoon":
                case "DRG":
                    return JobEnum.DRG;
                case "Ninja":
                case "NIN":
                    return JobEnum.NIN;
                case "Samurai":
                case "SAM":
                    return JobEnum.SAM;
                case "Bard":
                case "BRD":
                    return JobEnum.BRD;
                case "Machinist":
                case "MCH":
                    return JobEnum.MCH;
                case "Black Mage":
                case "BLM":
                    return JobEnum.BLM;
                case "Summoner":
                case "SMN":
                    return JobEnum.SMN;
                case "Red Mage":
                case "RDM":
                    return JobEnum.RDM;
                case "Blue Mage":
                case "BLU":
                    return JobEnum.BLU;
                case "Carpenter":
                case "CRP":
                    return JobEnum.CRP;
                case "Blacksmith":
                case "BSM":
                    return JobEnum.BSM;
                case "Armorer":
                case "ARM":
                    return JobEnum.ARM;
                case "Goldsmith":
                case "GSM":
                    return JobEnum.GSM;
                case "Leatherworker":
                case "LTW":
                    return JobEnum.LTW;
                case "Weaver":
                case "WVR":
                    return JobEnum.WVR;
                case "Alchemist":
                case "ALC":
                    return JobEnum.ALC;
                case "Culinarian":
                case "CUL":
                    return JobEnum.CUL;
                case "Miner":
                case "MIN":
                    return JobEnum.MIN;
                case "Botanist":
                case "BTN":
                    return JobEnum.BTN;
                case "Fisher":
                case "FSH":
                default:
                    return JobEnum.FSH;

            }
        } 
        #endregion
    }
}
