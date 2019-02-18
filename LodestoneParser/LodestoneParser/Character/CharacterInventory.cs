using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Character
{
    public class CharacterInventory
    {
        public CharacterWeapon Weapon { get; set; }

        public CharacterGear Head { get; set; }

        public CharacterGear Chest { get; set; }

        public CharacterGear Arms { get; set; }

        public CharacterGear Waist { get; set; }

        public CharacterGear Legs { get; set; }

        public CharacterGear Feet { get; set; }

        public CharacterGear Offhand { get; set; }

        public CharacterGear Earrings { get; set; }

        public CharacterGear Necklace { get; set; }

        public CharacterGear Bracelet { get; set; }

        public CharacterGear Ring1 { get; set; }

        public CharacterGear Ring2 { get; set; }

        public CharacterInventory(CharacterWeapon weapon, CharacterGear head, CharacterGear chest,
            CharacterGear arms, CharacterGear waist, CharacterGear legs, CharacterGear feet,
            CharacterGear offhand, CharacterGear earrings, CharacterGear necklace, 
            CharacterGear bracelet, CharacterGear ring1, CharacterGear ring2)
        {
            Weapon = weapon;
            Head = head;
            Chest = chest;
            Arms = arms;
            Waist = waist;
            Legs = legs;
            Feet = feet;
            Offhand = offhand;
            Earrings = earrings;
            Necklace = necklace;
            Bracelet = bracelet;
            Ring1 = ring1;
            Ring2 = ring2;
        }

        public CharacterLeftSide GetLeftSideGear()
        {
            return new CharacterLeftSide(Weapon, Head, Chest, Arms, Waist, Legs, Feet);
        }

        public CharacterRightSide GetRightSideGear()
        {
            return new CharacterRightSide(Offhand, Earrings, Necklace, Bracelet, Ring1, Ring2);
        }
    }

    public class CharacterLeftSide
    {
        public CharacterWeapon Weapon { get; set; }

        public CharacterGear Head { get; set; }

        public CharacterGear Chest { get; set; }

        public CharacterGear Arms { get; set; }

        public CharacterGear Waist { get; set; }

        public CharacterGear Legs { get; set; }

        public CharacterGear Feet { get; set; }

        public CharacterLeftSide(CharacterWeapon weapon, CharacterGear head, CharacterGear chest,
            CharacterGear arms, CharacterGear waist, CharacterGear legs, CharacterGear feet)
        {
            Weapon = weapon;
            Head = head;
            Chest = chest;
            Arms = arms;
            Waist = waist;
            Legs = legs;
            Feet = feet;
        }
    }

    public class CharacterRightSide
    {
        public CharacterGear Offhand { get; set; }

        public CharacterGear Earrings { get; set; }

        public CharacterGear Necklace { get; set; }

        public CharacterGear Bracelet { get; set; }

        public CharacterGear Ring1 { get; set; }

        public CharacterGear Ring2 { get; set; }

        public CharacterRightSide(CharacterGear offhand, CharacterGear earrings, CharacterGear necklace,
            CharacterGear bracelet, CharacterGear ring1, CharacterGear ring2)
        {
            Offhand = offhand;
            Earrings = earrings;
            Necklace = necklace;
            Bracelet = bracelet;
            Ring1 = ring1;
            Ring2 = ring2;
        }
    }
}
