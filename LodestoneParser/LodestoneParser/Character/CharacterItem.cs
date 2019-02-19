using HtmlAgilityPack;
using LodestoneParser.Enums;
using LodestoneParser.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LodestoneParser.Character
{
    /// <summary>
    /// Contains information related to a character-equippable item.
    /// </summary>
    public class CharacterItem
    {
        #region Properties
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Check to see if the item was parsed correctly. TODO: Improve this method/functionality.
        /// </summary>
        /// <returns>Whether or not the item is valid.</returns>
        public bool Validate()
        {
            return RepairInfo != ErrorObjects.RepairInfo;
        }
        #endregion
    }

    /// <summary>
    /// Contains information related to a weapon equippable by a character.
    /// </summary>
    public class CharacterWeapon : CharacterItem
    {
        #region Properties
        public int Damage { get; set; }

        public decimal AutoAttack { get; set; }

        public decimal Delay { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of CharacterWeapon.
        /// </summary>
        /// <param name="node">HtmlNode with relevant information for the weapon.</param>
        public CharacterWeapon(HtmlNode node)
        {
            IconUrl = node.FirstChild.FirstChild.ChildNodes[0].ChildNodes[0].ChildNodes["img"].Attributes["src"].Value;
            Name = node.FirstChild.FirstChild.FirstChild.ChildNodes[1].ChildNodes["h2"].InnerText.Replace("&#39;", "'");
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

        /// <summary>
        /// Create an empty instance of CharacterWeapon
        /// </summary>
        public CharacterWeapon()
        { }
        #endregion
    }

    /// <summary>
    /// Contains information related to gear equippable by a character.
    /// </summary>
    public class CharacterGear : CharacterItem
    {
        #region Properties
        public int Defense { get; set; }

        public int MagicDefense { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of CharacterGear
        /// </summary>
        public CharacterGear()
        {
            RepairInfo = ErrorObjects.RepairInfo;
        }

        /// <summary>
        /// Create a new instance of CharacterGear.
        /// </summary>
        /// <param name="node">HtmlNode with information related to the gear piece.</param>
        public CharacterGear(HtmlNode node)
        {
            IconUrl = node.FirstChild.FirstChild.FirstChild.ChildNodes[0].ChildNodes[0].ChildNodes["img"].Attributes["src"].Value;
            Name = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].ChildNodes["h2"].InnerText.Replace("&#39;", "'");
            ItemLevel = int.Parse(node.FirstChild.FirstChild.ChildNodes[2].InnerText.Substring(node.FirstChild.FirstChild.ChildNodes[2].InnerText.LastIndexOf(" ") + 1));
            Defense = int.Parse(node.FirstChild.FirstChild.ChildNodes[3].FirstChild.LastChild.FirstChild.FirstChild.InnerText);
            MagicDefense = int.Parse(node.FirstChild.FirstChild.ChildNodes[3].FirstChild.LastChild.LastChild.FirstChild.InnerText);

            if (node.FirstChild.FirstChild.ChildNodes[4].FirstChild.OriginalName != "hr")
            {
                EquippableBy = EquippableJob.CreateList(node.FirstChild.FirstChild.ChildNodes[4].FirstChild);

                var materiaNodes = node.FirstChild.FirstChild.ChildNodes[4].Descendants("ul").Where(d => d.GetAttributeValue("class", "").Contains("db-tooltip__materia"));

                EmptyMateriaSlots = 0;
                MateriaInfo = new List<Materia>();
                if (materiaNodes.Count() > 0)
                {
                    var materia = materiaNodes.First().ChildNodes;

                    foreach (var mat in materia)
                    {
                        if (mat.InnerText == "&nbsp;")
                        {
                            EmptyMateriaSlots++;
                        }
                        else
                        {
                            MateriaInfo.Add(new Materia(mat));
                        }
                    }
                }

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
                    RepairInfo = new RepairInfo(node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[6].ChildNodes);

                    var company = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[0].FirstChild.Attributes["data-tooltip"].Value;
                    var glamour = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[1].FirstChild.Attributes["data-tooltip"].Value;
                    var armoire = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[2].FirstChild.Attributes["data-tooltip"].Value;

                    CompanyCrest = !company.Contains("Cannot");
                    GlamourChest = !glamour.Contains("Cannot");
                    Armoire = !armoire.Contains("Cannot");

                    Convertible = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[7].ChildNodes[0].ChildNodes["span"].InnerText == "Yes");
                    Projectable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[7].ChildNodes[1].ChildNodes["span"].InnerText == "Yes");
                    Desynthesizable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[7].ChildNodes[2].ChildNodes["span"].InnerText == "Yes");
                    Dyable = (node.FirstChild.FirstChild.ChildNodes[4].ChildNodes[7].ChildNodes[3].ChildNodes["span"].InnerText == "Yes");
                }
            }
            else
            {
                EquippableBy = EquippableJob.CreateList(node.FirstChild.FirstChild.ChildNodes[3].FirstChild);

                var stats = new List<BonusStat>();
                var statNodes = node.FirstChild.FirstChild.ChildNodes[3].ChildNodes["ul"].ChildNodes;

                foreach (var n in statNodes)
                {
                    stats.Add(new BonusStat(n));
                }

                BonusStats = stats;


                if (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes.Count > 8)
                {
                    // Account for Materia
                    RepairInfo = new RepairInfo(node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[9].ChildNodes);

                    var company = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[0].FirstChild.Attributes["data-tooltip"].Value;
                    var glamour = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[1].FirstChild.Attributes["data-tooltip"].Value;
                    var armoire = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[2].FirstChild.Attributes["data-tooltip"].Value;

                    CompanyCrest = !company.Contains("Cannot");
                    GlamourChest = !glamour.Contains("Cannot");
                    Armoire = !armoire.Contains("Cannot");

                    Convertible = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[0].ChildNodes["span"].InnerText == "Yes");
                    Projectable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[1].ChildNodes["span"].InnerText == "Yes");
                    Desynthesizable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[2].ChildNodes["span"].InnerText == "Yes");
                    Dyable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[3].ChildNodes["span"].InnerText == "Yes");
                }
                else
                {
                    // No Materia
                    RepairInfo = new RepairInfo(node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[9].ChildNodes);

                    var company = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[0].FirstChild.Attributes["data-tooltip"].Value;
                    var glamour = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[1].FirstChild.Attributes["data-tooltip"].Value;
                    var armoire = node.FirstChild.FirstChild.FirstChild.FirstChild.ChildNodes[1].FirstChild.ChildNodes["ul"].ChildNodes[2].FirstChild.Attributes["data-tooltip"].Value;

                    CompanyCrest = !company.Contains("Cannot");
                    GlamourChest = !glamour.Contains("Cannot");
                    Armoire = !armoire.Contains("Cannot");

                    Convertible = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[0].ChildNodes["span"].InnerText == "Yes");
                    Projectable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[1].ChildNodes["span"].InnerText == "Yes");
                    Desynthesizable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[2].ChildNodes["span"].InnerText == "Yes");
                    Dyable = (node.FirstChild.FirstChild.ChildNodes[3].ChildNodes[10].ChildNodes[3].ChildNodes["span"].InnerText == "Yes");
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Contains information on jobs which can equip a specified item.
    /// </summary>
    public class EquippableJob
    {
        #region Properties
        public JobEnum Job { get; set; }

        public int Level { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of EquippableJob.
        /// </summary>
        /// <param name="node">HtmlNode with information related to the job.</param>
        public EquippableJob(HtmlNode node)
        {
            JobEnum parsed;
            Enum.TryParse(node.FirstChild.InnerText, out parsed);

            Job = parsed;
            Level = int.Parse(node.LastChild.InnerText.Substring(node.LastChild.InnerText.LastIndexOf(" ") + 1));
        }

        /// <summary>
        /// Create a new instance of EquippableJob.
        /// </summary>
        /// <param name="job">JobEnum for the job that can equip the piece of gear.</param>
        /// <param name="level">Level at which the job can equip the piece.</param>
        public EquippableJob(JobEnum job, int level)
        {
            Job = job;
            Level = level;
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Create a list of EquippableJobs for an item.
        /// </summary>
        /// <param name="node">HtmlNode with information related to the jobs.</param>
        /// <returns>A List of EquippableJobs.</returns>
        public static List<EquippableJob> CreateList(HtmlNode node)
        {
            var list = new List<EquippableJob>();
            var fullString = node.FirstChild.FirstChild.InnerText;
            var level = int.Parse(node.LastChild.InnerText.Substring(node.LastChild.InnerText.LastIndexOf(" ") + 1));

            if (fullString == "All Classes")
            {
                foreach (JobEnum job in (JobEnum[])Enum.GetValues(typeof(JobEnum)))
                {
                    list.Add(new EquippableJob(job, level));
                }
            }
            else if (fullString == "Disciple of the Hand")
            {
                list.AddRange(CreateMultiple(Disciplines.GetDisciplesOfHand(), level));
            }
            else if (fullString == "Disciple of the Land")
            {
                list.AddRange(CreateMultiple(Disciplines.GetDisciplesOfLand(), level));
            }
            else if (fullString == "Disciple of War")
            {
                list.AddRange(CreateMultiple(Disciplines.GetDisciplesOfWar(), level));
            }
            else if (fullString == "Disciple of Magic")
            {
                list.AddRange(CreateMultiple(Disciplines.GetDisciplesOfMagic(), level));
            }
            else
            {
                var job = fullString.Split(' ');

                foreach (var j in job)
                {
                    JobEnum parsed;
                    Enum.TryParse(j, out parsed);

                    list.Add(new EquippableJob(parsed, level));
                }
            }

            return list;
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Create multiple EquippableJobx based on an array of JobEnums.
        /// </summary>
        /// <param name="jobs">JobEnums that can equip a piece of gear.</param>
        /// <param name="level">Level at which the jobs can equip the piece.</param>
        /// <returns>List of EquippableJobs.</returns>
        static List<EquippableJob> CreateMultiple(JobEnum[] jobs, int level)
        {
            var list = new List<EquippableJob>();

            foreach (var job in jobs)
            {
                list.Add(new EquippableJob(job, level));
            }

            return list;
        }
        #endregion
    }

    /// <summary>
    /// Contains information on stat bonuses given by a gear piece.
    /// </summary>
    public class BonusStat
    {
        #region Properties
        public StatEnum Stat { get; set; }

        public int Bonus { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of BonusStat.
        /// </summary>
        /// <param name="node">HtmlNode with information on the stat bonuses.</param>
        public BonusStat(HtmlNode node)
        {
            Stat = StatParser.Parse(node.ChildNodes["span"].InnerText);
            Bonus = int.Parse(node.InnerText.Substring(node.InnerText.IndexOf('+') + 1));
        }
        #endregion
    }

    /// <summary>
    /// Contains information on repairing the gear piece.
    /// </summary>
    public class RepairInfo
    {
        #region Properties
        public JobEnum Job { get; set; }

        public int Level { get; set; }

        public int MaterialGrade { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of RepairInfo.
        /// </summary>
        /// <param name="job">JobEnum that can repair the piece.</param>
        /// <param name="level">Level required to repair the piece.</param>
        /// <param name="grade">Grade of material required to repair the piece.</param>
        public RepairInfo(JobEnum job, int level, int grade)
        {
            Job = job;
            Level = level;
            MaterialGrade = grade;
        }

        /// <summary>
        /// Create a new instance of RepairInfo.
        /// </summary>
        /// <param name="nodes">Collection of HtmlNodes with relevant information.</param>
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
        #endregion
    }

    /// <summary>
    /// Contains information on materia within the gear piece.
    /// </summary>
    public class Materia
    {
        #region Properties
        public StatEnum Stat { get; set; }

        public int Value { get; set; }

        public string Name { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of Materia.
        /// </summary>
        /// <param name="node">HtmlNode with information on materia.</param>
        public Materia(HtmlNode node)
        {
            Name = node.LastChild.FirstChild.InnerText.Replace("&#39;", "'");
            Stat = StatParser.Parse(node.LastChild.LastChild.InnerText.Substring(0, node.LastChild.LastChild.InnerText.IndexOf('+') - 1));
            Value = int.Parse(node.InnerText.Substring(node.InnerText.IndexOf('+') + 1));
        }
        #endregion
    }
}
