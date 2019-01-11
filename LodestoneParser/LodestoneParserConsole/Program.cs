using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LodestoneParser;

namespace LodestoneParserConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new LodestoneParser.LodestoneParser(6738422);

            Console.Write(parser.GetName());
        }
    }
}
