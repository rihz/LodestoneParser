using LodestoneParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Logic
{
    public static class StatParser
    {
        public static StatEnum Parse(string toParse)
        {
            switch(toParse)
            {
                case "Strength":
                    return StatEnum.Strength;
                case "Dexterity":
                    return StatEnum.Dexterity;
                case "Vitality":
                    return StatEnum.Vitality;
                case "Intelligence":
                    return StatEnum.Intelligence;
                case "Mind":
                    return StatEnum.Mind;
                case "Skill Speed":
                    return StatEnum.SkillSpeed;
                case "Spell Speed":
                    return StatEnum.SpellSpeed;
                case "Craftsmanship":
                    return StatEnum.Craftsmanship;
                case "Control":
                    return StatEnum.Control;
                case "Gathering":
                    return StatEnum.Gathering;
                case "Perception":
                    return StatEnum.Perception;
                case "Critical Hit":
                    return StatEnum.CriticalHit;
                case "CP":
                    return StatEnum.CP;
                case "GP":
                    return StatEnum.GP;
                case "Determination":
                default:
                    return StatEnum.Determination;
            }
        }
    }
}
