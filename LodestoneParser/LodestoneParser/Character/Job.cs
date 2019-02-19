using LodestoneParser.Enums;

namespace LodestoneParser.Character
{
    /// <summary>
    /// Contains information on a character's Job.
    /// </summary>
    public class Job
    {
        #region Properties
        public JobEnum Enum { get; set; }

        public string Abbreviation { get; set; }

        public string Name { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of Job.
        /// </summary>
        /// <param name="job">JobEnum related to Job.</param>
        /// <param name="abbr">Abbreviation for the Job's name.</param>
        /// <param name="name">Name of the Job.</param>
        public Job(JobEnum job, string abbr, string name)
        {
            Enum = job;
            Abbreviation = abbr;
            Name = name;
        }
        #endregion
    }
}
