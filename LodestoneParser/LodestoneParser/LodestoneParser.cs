using System;
using HtmlAgilityPack;

namespace LodestoneParser
{
    public class LodestoneParser
    {
        const string _url = "https://na.finalfantasyxiv.com/lodestone/character/{0}/";

        HtmlDocument _doc;

        public LodestoneParser(int userId)
        {
            var url = string.Format(_url, userId);
            var web = new HtmlWeb();

            _doc = web.Load(url);
        }

        public string GetTitle()
        {
            return _doc.DocumentNode.SelectSingleNode("//div[@class='frame__chara__title']").InnerText;
        }

        public string GetName()
        {
            return _doc.DocumentNode.SelectSingleNode("//div[@class='frame__chara__name']").InnerText;
        }

        public string GetServer()
        {
            return _doc.DocumentNode.SelectSingleNode("//div[@class='frame__chara__world']").InnerText;
        }
    }
}
