using HtmlAgilityPack;
using LodestoneParser.Character;
using LodestoneParser.Enums;
using LodestoneParser.Exceptions;
using System;
using System.Collections.Generic;

namespace LodestoneParser
{
    /// <summary>
    /// Responsible for parsing the Final Fantasy XIV Lodestone website for details on a character.
    /// </summary>
    public class LodestoneParser
    {
        #region Variables
        const string _url = "https://na.finalfantasyxiv.com/lodestone/character/{0}/";

        HtmlNode _doc = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of Lodestone Parser.
        /// </summary>
        public LodestoneParser()
        { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Load a character's Lodestone page.
        /// </summary>
        /// <param name="characterId">Id of character.</param>
        public void LoadCharacter(int characterId)
        {
            var url = string.Format(_url, characterId);

            var web = new HtmlWeb();
            web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";

            _doc = web.Load(url).DocumentNode;
        }

        /// <summary>
        /// Get the loaded character's title.
        /// </summary>
        /// <returns>The character's title.</returns>
        public string GetTitle()
        {
            return GetSingleNodeForCharacter("//p[@class='frame__chara__title']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's name.
        /// </summary>
        /// <returns>The character's name.</returns>
        public string GetName()
        {
            return GetSingleNodeForCharacter("//p[@class='frame__chara__name']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's server.
        /// </summary>
        /// <returns>The character's server.</returns>
        public string GetServer()
        {
            return GetSingleNodeForCharacter("//p[@class='frame__chara__world']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's icon url.
        /// </summary>
        /// <returns>The character's icon url.</returns>
        public string GetIconUrl()
        {
            var div = GetSingleNodeForCharacter("//div[@class='frame__chara__face']/img");

            return div.Attributes["src"].Value;
        }

        /// <summary>
        /// Get the loaded character's level in a specific job.
        /// </summary>
        /// <param name="job">Job with desired level.</param>
        /// <returns>Level of the job for the loaded character.</returns>
        public string GetJobLevel(JobEnum job)
        {
            var xpath = "//div[@class='character__profile__detail']/div[{0}]/div/ul/";

            if (job == JobEnum.WAR || job == JobEnum.GLD || job == JobEnum.DRK
                || job == JobEnum.WHM || job == JobEnum.SCH || job == JobEnum.AST)
                xpath = string.Format(xpath, 2);
            else if (job == JobEnum.MNK || job == JobEnum.DRG || job == JobEnum.NIN
                || job == JobEnum.SMN || job == JobEnum.SAM || job == JobEnum.RDM
                || job == JobEnum.BLM || job == JobEnum.BRD || job == JobEnum.MCH
                || job == JobEnum.BLU)
                xpath = string.Format(xpath, 3);
            else if (job == JobEnum.BSM || job == JobEnum.ARM || job == JobEnum.ALC
                || job == JobEnum.CRP || job == JobEnum.CUL || job == JobEnum.GSM
                || job == JobEnum.LTW || job == JobEnum.WVR)
                xpath = string.Format(xpath, 4);
            else
                xpath = string.Format(xpath, 5);

            if (job == JobEnum.GLD || job == JobEnum.MNK || job == JobEnum.CRP || job == JobEnum.MIN)
                xpath += "li[1]";
            else if (job == JobEnum.WAR || job == JobEnum.DRG || job == JobEnum.BSM || job == JobEnum.BTN)
                xpath += "li[2]";
            else if (job == JobEnum.DRK || job == JobEnum.NIN || job == JobEnum.ARM || job == JobEnum.FSH)
                xpath += "li[3]";
            else if (job == JobEnum.WHM || job == JobEnum.SAM || job == JobEnum.GSM)
                xpath += "li[4]";
            else if (job == JobEnum.SCH || job == JobEnum.BRD || job == JobEnum.LTW)
                xpath += "li[5]";
            else if (job == JobEnum.AST || job == JobEnum.MCH || job == JobEnum.WVR)
                xpath += "li[6]";
            else if (job == JobEnum.BLM || job == JobEnum.ALC)
                xpath += "li[7]";
            else if (job == JobEnum.SMN || job == JobEnum.CUL)
                xpath += "li[8]";
            else if (job == JobEnum.RDM)
                xpath += "li[9]";
            else
                xpath += "li[10]";

            return GetSingleNodeForCharacter(xpath).InnerText;
        }

        /// <summary>
        /// Get the loaded character's profile text.
        /// </summary>
        /// <returns>The character's profile text.</returns>
        public string GetProfile()
        {
            return GetSingleNodeForCharacter("//div[@class='character__selfintroduction']").InnerText;
        }

        /// <summary>
        /// Get a list of all mounts owned by the loaded character.
        /// </summary>
        /// <returns>List of Mount objects with name and icon URL.</returns>
        public List<Mount> GetMounts()
        {
            var list = GetNodesForCharacter("//div[@class='character__mounts']/ul/li");
            var mounts = new List<Mount>();

            foreach (var node in list)
            {
                var name = node.FirstChild.Attributes["data-tooltip"].Value;
                var img = node.FirstChild.FirstChild.Attributes["src"].Value;

                mounts.Add(new Mount()
                {
                    Name = name,
                    Image = img
                });
            }

            return mounts;
        }

        /// <summary>
        /// Get a list of all minions owned by the loaded character.
        /// </summary>
        /// <returns>List of Minion objects with name and icon URL.</returns>
        public List<Minion> GetMinions()
        {
            var list = GetNodesForCharacter("//div[@class='character__minion']/ul/li");
            var minions = new List<Minion>();

            foreach (var node in list)
            {
                var name = node.FirstChild.Attributes["data-tooltip"].Value;
                var img = node.FirstChild.FirstChild.Attributes["src"].Value;

                minions.Add(new Minion()
                {
                    Name = name,
                    Image = img
                });
            }

            return minions;
        }

        /// <summary>
        /// Check that the loaded character's profile contains the passed string.
        /// </summary>
        /// <param name="check">String to find in profile.</param>
        /// <returns>True or false based on whether the value could be found.</returns>
        public bool CheckProfileContains(string check)
        {
            return GetProfile().Contains(check);
        }

        /// <summary>
        /// Get the weapon currently equipped on the character.
        /// </summary>
        /// <returns>CharacterWeapon object with details on character's weapon.</returns>
        public CharacterWeapon GetWeapon()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__class__arms']/div[1]/div/div");

            return new CharacterWeapon(node);
        }

        /// <summary>
        /// Get the head equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's head equipment.</returns>
        public CharacterGear GetHeadGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[1]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the chest equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's chest equipment.</returns>
        public CharacterGear GetChestGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[2]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the arm equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's arm equipment.</returns>
        public CharacterGear GetArmGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[3]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the waist equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's waist equipment.</returns>
        public CharacterGear GetWaistGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[4]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the leg equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's leg equipment.</returns>
        public CharacterGear GetLegGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[5]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the foot equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's foot equipment.</returns>
        public CharacterGear GetFootGear()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div/div[6]/div[last()]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the offhand equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's offhand equipment.</returns>
        public CharacterGear GetOffhand()
        {
            try
            {
                var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[1]/div[2]");

                return new CharacterGear(node);
            }
            catch (Exception ex)
            {
                return new CharacterGear();
            }
        }

        /// <summary>
        /// Get the earring equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's earring equipment.</returns>
        public CharacterGear GetEarrings()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[2]/div[2]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the necklace equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's necklace equipment.</returns>
        public CharacterGear GetNecklace()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[3]/div[2]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the bracelet equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's bracelet equipment.</returns>
        public CharacterGear GetBracelet()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[4]/div[2]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the first ring equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's first ring equipment.</returns>
        public CharacterGear GetRing1()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[5]/div[2]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the second ring equipment currently equipped on the character.
        /// </summary>
        /// <returns>CharacterGear object with details on character's second ring equipment.</returns>
        public CharacterGear GetRing2()
        {
            var node = GetSingleNodeForCharacter("//div[@class='character__detail']/div[3]/div[6]/div[2]");

            return new CharacterGear(node);
        }

        /// <summary>
        /// Get the character's full inventory.
        /// </summary>
        /// <returns>CharacterInventory object with details on all of the character's equipment.</returns>
        public CharacterInventory GetCharacterInventory()
        {
            return new CharacterInventory(GetWeapon(), GetHeadGear(), GetChestGear(), GetArmGear(),
                GetWaistGear(), GetLegGear(), GetFootGear(), GetOffhand(), GetEarrings(), GetNecklace(),
                GetBracelet(), GetRing1(), GetRing2());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Get a node on the loaded character's Lodestone page.
        /// </summary>
        /// <param name="xpath">Xpath to the HTML node.</param>
        /// <returns>HtmlNode located at xpath.</returns>
        /// <exception cref="CharacterNotLoadedException"></exception>
        HtmlNode GetSingleNodeForCharacter(string xpath)
        {
            return _doc != null
                ? _doc.SelectSingleNode(xpath)
                : throw new CharacterNotLoadedException();
        }

        /// <summary>
        /// Get a collection of nodes on the loaded character's Lodestone page.
        /// </summary>
        /// <param name="xpath">Xpath to the HTML nodes.</param>
        /// <returns>HtmlNodeCollection located at xpath.</returns>
        HtmlNodeCollection GetNodesForCharacter(string xpath)
        {
            return _doc != null
                ? _doc.SelectNodes(xpath)
                : throw new CharacterNotLoadedException();
        }
        #endregion
    }
}
