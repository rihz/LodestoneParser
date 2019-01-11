using System;
using System.Net;
using HtmlAgilityPack;

namespace LodestoneParser
{
    public class LodestoneParser
    {
        const string _url = "https://na.finalfantasyxiv.com/lodestone/character/{0}/";

        HtmlNode _doc;

        public LodestoneParser(int userId)
        {
            var url = string.Format(_url, userId);
            var web = new HtmlWeb();
            web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";

            _doc = web.Load(url).DocumentNode;
        }

        public string GetTitle()
        {
            return _doc.SelectSingleNode("//p[@class='frame__chara__title']").InnerText;
        }

        public string GetName()
        {
            return _doc.SelectSingleNode("//p[@class='frame__chara__name']").InnerText;
        }

        public string GetServer()
        {
            return _doc.SelectSingleNode("//p[@class='frame__chara__world']").InnerText;
        }
    }
}
