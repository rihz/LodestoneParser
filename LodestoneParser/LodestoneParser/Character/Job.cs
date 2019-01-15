using LodestoneParser.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Character
{
    public class Job
    {
        public JobEnum Enum { get; set; }

        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public Job(JobEnum jobe, string abbr, string name)
        {
            Enum = jobe;
            Abbreviation = abbr;
            Name = name;
        }
    }
}
