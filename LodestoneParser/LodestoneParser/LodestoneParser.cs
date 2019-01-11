using System;
using System.Net;
using HtmlAgilityPack;
using LodestoneParser.Exceptions;

namespace LodestoneParser
{
    public class LodestoneParser
    {
        const string _url = "https://na.finalfantasyxiv.com/lodestone/character/{0}/";

        HtmlNode _doc = null;

        public LodestoneParser()
        { }

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
            return GetNodeForCharacter("//p[@class='frame__chara__title']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's name.
        /// </summary>
        /// <returns>The character's name.</returns>
        public string GetName()
        {
            return GetNodeForCharacter("//p[@class='frame__chara__name']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's server.
        /// </summary>
        /// <returns>The character's server.</returns>
        public string GetServer()
        {
            return GetNodeForCharacter("//p[@class='frame__chara__world']").InnerText;
        }

        /// <summary>
        /// Get the loaded character's icon url.
        /// </summary>
        /// <returns>The character's icon url.</returns>
        public string GetIconUrl()
        {
            var div = GetNodeForCharacter("//div[@class='frame__chara__face']/img");

            return div.Attributes["src"].Value;
        }

        /// <summary>
        /// Get a node on the loaded character's Lodestone page.
        /// </summary>
        /// <param name="xpath">Xpath to the HTML node.</param>
        /// <returns>HtmlNode located at xpath.</returns>
        /// <exception cref="CharacterNotLoadedException"></exception>
        HtmlNode GetNodeForCharacter(string xpath)
        {
            return _doc != null
                ? _doc.SelectSingleNode(xpath)
                : throw new CharacterNotLoadedException();
        }
    }
}
