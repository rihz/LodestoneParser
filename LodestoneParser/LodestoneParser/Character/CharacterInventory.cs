namespace LodestoneParser.Character
{
    /// <summary>
    /// Contains information related to a character's overall equipment.
    /// </summary>
    public class CharacterInventory
    {
        #region Properties
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
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of CharacterInventory.
        /// </summary>
        /// <param name="weapon">The weapon equipped by the character.</param>
        /// <param name="head">The head equipment equipped by the character.</param>
        /// <param name="chest">The chest equipment equipped by the character.</param>
        /// <param name="arms">The arm equipment equipped by the character.</param>
        /// <param name="waist">The waist equipment equipped by the character.</param>
        /// <param name="legs">The leg equipment equipped by the character.</param>
        /// <param name="feet">The foot equipment equipped by the character.</param>
        /// <param name="offhand">The offhand equipment equipped by the character.</param>
        /// <param name="earrings">The earring equipment equipped by the character.</param>
        /// <param name="necklace">The necklace equipment equipped by the character.</param>
        /// <param name="bracelet">The bracelet equipment equipped by the character.</param>
        /// <param name="ring1">The first ring equipment equipped by the character.</param>
        /// <param name="ring2">The second ring equipment equipped by the character.</param>
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the equipment on the left side of the character sheet.
        /// </summary>
        /// <returns>CharacterLeftSide object with information on left-side equipment.</returns>
        public CharacterLeftSide GetLeftSideGear()
        {
            return new CharacterLeftSide(Weapon, Head, Chest, Arms, Waist, Legs, Feet);
        }

        /// <summary>
        /// Gets the equipment on the right side of the character sheet.
        /// </summary>
        /// <returns>CharacterRightSide object with information on right-side equipment.</returns>
        public CharacterRightSide GetRightSideGear()
        {
            return new CharacterRightSide(Offhand, Earrings, Necklace, Bracelet, Ring1, Ring2);
        }
        #endregion
    }

    /// <summary>
    /// Contains information related to equipment that appears on the character's left side.
    /// </summary>
    public class CharacterLeftSide
    {
        #region Properties
        public CharacterWeapon Weapon { get; set; }

        public CharacterGear Head { get; set; }

        public CharacterGear Chest { get; set; }

        public CharacterGear Arms { get; set; }

        public CharacterGear Waist { get; set; }

        public CharacterGear Legs { get; set; }

        public CharacterGear Feet { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of CharacterLeftSide.
        /// </summary>
        /// <param name="weapon">The weapon equipped by the character.</param>
        /// <param name="head">The head equipment equipped by the character.</param>
        /// <param name="chest">The chest equipment equipped by the character.</param>
        /// <param name="arms">The arm equipment equipped by the character.</param>
        /// <param name="waist">The waist equipment equipped by the character.</param>
        /// <param name="legs">The leg equipment equipped by the character.</param>
        /// <param name="feet">The foot equipment equipped by the character.</param>
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
        #endregion
    }

    /// <summary>
    /// Contains information related to equipment that appears on a character's right side.
    /// </summary>
    public class CharacterRightSide
    {
        #region Properties
        public CharacterGear Offhand { get; set; }

        public CharacterGear Earrings { get; set; }

        public CharacterGear Necklace { get; set; }

        public CharacterGear Bracelet { get; set; }

        public CharacterGear Ring1 { get; set; }

        public CharacterGear Ring2 { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of CharacterRightSide.
        /// </summary>
        /// <param name="offhand">The offhand equipment equipped by the character.</param>
        /// <param name="earrings">The earring equipment equipped by the character.</param>
        /// <param name="necklace">The necklace equipment equipped by the character.</param>
        /// <param name="bracelet">The bracelet equipment equipped by the character.</param>
        /// <param name="ring1">The first ring equipment equipped by the character.</param>
        /// <param name="ring2">The second ring equipment equipped by the character.</param>
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
        #endregion
    }
}
