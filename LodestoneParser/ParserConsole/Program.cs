using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new LodestoneParser.LodestoneParser();
            parser.LoadCharacter(6738422);

            var items = parser.GetCharacterItems();
        }
    }
}
