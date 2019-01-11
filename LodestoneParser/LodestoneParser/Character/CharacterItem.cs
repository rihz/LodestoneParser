using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Character
{
    public class CharacterItem
    {
        public string IconUrl { get; set; }

        public string Name { get; set; }

        public int ItemLevel { get; set; }
    }

    public class CharacterWeapon : CharacterItem
    {
        public int Damage { get; set; }

        public decimal AutoAttack { get; set; }

        public decimal Delay { get; set; }
    }
}
