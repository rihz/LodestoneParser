using HtmlAgilityPack;
using LodestoneParser.Enums;
using LodestoneParser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LodestoneParser.Character
{
    public class CharacterItem
    {
        public string IconUrl { get; set; }

        public string Name { get; set; }

        public int ItemLevel { get; set; }

        public RepairInfo RepairInfo { get; set; }

        public List<EquippableJob> EquippableBy { get; set; }

        public List<BonusStat> BonusStats { get; set; }

        public bool CompanyCrest { get; set; }

        public bool GlamourChest { get; set; }

        public bool Armoire { get; set; }

        public bool Convertible { get; set; }

        public bool Projectable { get; set; }

        public bool Desynthesizable { get; set; }

        public bool Dyable { get; set; }

        public List<Materia> MateriaInfo { get; set; }

        public int EmptyMateriaSlots { get; set; }

        public bool Validate()
        {
            return RepairInfo != ErrorObjects.RepairInfo;
        }
    }

    public class CharacterWeapon : CharacterItem
    {
        public int Damage { get; set; }

        public decimal AutoAttack { get; set; }

        public decimal Delay { get; set; }

        public CharacterWeapon(HtmlNode node)
        {
            IconUrl = node.FirstChild.FirstChild.ChildNodes[0].ChildNodes[0].ChildNodes["img"].Attributes["src"].Value;
            Name = node.FirstChild.FirstChild.FirstChild.ChildNodes[1].ChildNodes["h2"].InnerText;
            ItemLevel = int.Parse(node.FirstChild.ChildNodes[2].InnerText.Substring(node.FirstChild.ChildNodes[2].InnerText.LastIndexOf(" ") + 1));
            Damage = int.Parse(node.FirstChild.ChildNodes[3].FirstChild.LastChild.FirstChild.FirstChild.InnerText);
            AutoAttack = decimal.Parse(node.FirstChild.ChildNodes[3].FirstChild.LastChild.ChildNodes[1].FirstChild.InnerText);
            Delay = decimal.Parse(node.FirstChild.ChildNodes[3].FirstChild.LastChild.ChildNodes[2].FirstChild.InnerText);
            EquippableBy = new List<EquippableJob>()
                {
                    new EquippableJob(node.FirstChild.ChildNodes[4].FirstChild)
                };

            var emptySlotCount = node.FirstChild.ChildNodes[4].Descendants("ul").Where(d => d.GetAttributeValue("class", "").Contains("db-tooltip__materia__normal")).Count();
            var materiaNodes = node.FirstChild.ChildNodes[4].Descendants("ul").Where(d => d.GetAttributeValue("class", "").Contains("db-tooltip__materia"));

            MateriaInfo = new List<Materia>();
            if (materiaNodes.Count() > 0)
            {
                var materia = materiaNodes.First().ChildNodes;

                foreach (var mat in materia)
                {
                    MateriaInfo.Add(new Materia(mat));
                }
            }

            EmptyMateriaSlots = emptySlotCount;

            var stats = new List<BonusStat>();
            var statNodes = node.FirstChild.ChildNodes[4].ChildNodes["ul"].ChildNodes;

            foreach (var n in statNodes)
            {
                stats.Add(new BonusStat(n));
            }

            BonusStats = stats;
            RepairInfo = new RepairInfo(node.FirstChild.ChildNodes[4].ChildNodes[9].ChildNodes);
            var company = node.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[0].FirstChild.Attributes["data-tooltip"].Value;
            var glamour = node.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[1].FirstChild.Attributes["data-tooltip"].Value;
            var armoire = node.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[2].FirstChild.Attributes["data-tooltip"].Value;

            CompanyCrest = !company.Contains("Cannot");
            GlamourChest = !glamour.Contains("Cannot");
            Armoire = !armoire.Contains("Cannot");

            Convertible = (node.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[0].ChildNodes["span"].InnerText == "Yes");
            Projectable = (node.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[1].ChildNodes["span"].InnerText == "Yes");
            Desynthesizable = (node.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[2].ChildNodes["span"].InnerText == "Yes");
            Dyable = (node.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[3].ChildNodes["span"].InnerText == "Yes");
        }

        public CharacterWeapon()
        { }
    }

    public class CharacterGear : CharacterItem
    {
        public int Defense { get; set; }

        public int MagicDefense { get; set; }

        public CharacterGear()
        {
            RepairInfo = ErrorObjects.RepairInfo;
        }

        public CharacterGear(HtmlNode node)
        {
            IconUrl = node.FirstChild.FirstChild.FirstChild.ChildNodes[0].ChildNodes[0].ChildNodes["img"].Attributes["src"].Value;
            Name = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].ChildNodes["h2"].InnerText;
            ItemLevel = int.Parse(node.FirstChild.FirstChild.ChildNodes[2].InnerText.Substring(node.FirstChild.FirstChild.ChildNodes[2].InnerText.LastIndexOf(" ") + 1));

            if (node.FirstChild.FirstChild.ChildNodes[4].FirstChild.OriginalName != "hr")
                EquippableBy = EquippableJob.CreateList(node.FirstChild.FirstChild.ChildNodes[4].FirstChild);
            else
                EquippableBy = EquippableJob.CreateList(node.FirstChild.FirstChild.ChildNodes[3].FirstChild);

            var stats = new List<BonusStat>();
            var statNodes = node.FirstChild.FirstChild.ChildNodes[4].ChildNodes["ul"].ChildNodes;

            foreach (var n in statNodes)
            {
                stats.Add(new BonusStat(n));
            }

            BonusStats = stats;

            if (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes.Count > 8)
            {
                // Account for Materia
                RepairInfo = new RepairInfo(node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[9].ChildNodes);

                var company = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[0].FirstChild.Attributes["data-tooltip"].Value;
                var glamour = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[1].FirstChild.Attributes["data-tooltip"].Value;
                var armoire = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[2].FirstChild.Attributes["data-tooltip"].Value;

                CompanyCrest = !company.Contains("Cannot");
                GlamourChest = !glamour.Contains("Cannot");
                Armoire = !armoire.Contains("Cannot");

                Convertible = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[0].ChildNodes["span"].InnerText == "Yes");
                Projectable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[1].ChildNodes["span"].InnerText == "Yes");
                Desynthesizable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[2].ChildNodes["span"].InnerText == "Yes");
                Dyable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[10].ChildNodes[3].ChildNodes["span"].InnerText == "Yes");
            }
            else
            {
                // No Materia
                RepairInfo = ErrorObjects.RepairInfo;

                CompanyCrest = false;
                GlamourChest = false;
                Armoire = false;

                Convertible = false;
                Projectable = false;
                Desynthesizable = false;
                Dyable = false;
            }
        }
    }

    public class EquippableJob
    {
        public JobEnum Job { get; set; }

        public int Level { get; set; }

        public EquippableJob(HtmlNode node)
        {
            JobEnum parsed;
            Enum.TryParse(node.FirstChild.InnerText, out parsed);

            Job = parsed;
            Level = int.Parse(node.LastChild.InnerText.Substring(node.LastChild.InnerText.LastIndexOf(" ") + 1));
        }

        public EquippableJob(JobEnum job, int level)
        {
            Job = job;
            Level = level;
        }

        public static List<EquippableJob> CreateList(HtmlNode node)
        {
            var list = new List<EquippableJob>();
            var fullString = node.FirstChild.FirstChild.InnerText;
            var level = int.Parse(node.LastChild.InnerText.Substring(node.LastChild.InnerText.LastIndexOf(" ") + 1));

            var job = fullString.Split(' ');

            foreach (var j in job)
            {
                JobEnum parsed;
                Enum.TryParse(j, out parsed);

                list.Add(new EquippableJob(parsed, level));
            }

            return list;
        }
    }

    public class BonusStat
    {
        public StatEnum Stat { get; set; }

        public int Bonus { get; set; }

        public BonusStat(HtmlNode node)
        {
            Stat = StatParser.Parse(node.ChildNodes["span"].InnerText);
            Bonus = int.Parse(node.InnerText.Substring(node.InnerText.IndexOf('+') + 1));
        }
    }

    public class RepairInfo
    {
        public JobEnum Job { get; set; }

        public int Level { get; set; }

        public int MaterialGrade { get; set; }

        public RepairInfo(JobEnum job, int level, int grade)
        {
            Job = job;
            Level = level;
            MaterialGrade = grade;
        }

        public RepairInfo(HtmlNodeCollection nodes)
        {
            var repair = nodes[2].LastChild.InnerText;
            var jobString = repair.Substring(0, repair.IndexOf(" "));
            var lvString = repair.Substring(repair.LastIndexOf(" ") + 1);
            var grade = nodes[3].LastChild.InnerText[6].ToString();

            Job = JobParser.Parse(jobString);
            Level = int.Parse(lvString);
            MaterialGrade = int.Parse(grade);
        }
    }

    public class Materia
    {
        public StatEnum Stat { get; set; }

        public int Value { get; set; }

        public string Name { get; set; }

        public Materia(HtmlNode node)
        {
            Name = node.InnerText;
            Stat = StatParser.Parse(node.LastChild.InnerText);
            Value = int.Parse(node.InnerText.Substring(node.InnerText.IndexOf('+') + 1));
        }
    }
}
