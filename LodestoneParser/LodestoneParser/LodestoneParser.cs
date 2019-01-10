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
    }
}
