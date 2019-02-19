namespace LodestoneParser.Enums
{
    /// <summary>
    /// Contains methods related to the different Disciplines in Final Fantasy XIV.
    /// </summary>
    public static class Disciplines
    {
        #region Public Static Methods
        /// <summary>
        /// Gets JobEnums related to Disciples of the Hand.
        /// </summary>
        /// <returns>Array of JobEnum with relevant values.</returns>
        public static JobEnum[] GetDisciplesOfHand()
        {
            return new JobEnum[] { JobEnum.ARM, JobEnum.BSM, JobEnum.CRP, JobEnum.CUL, JobEnum.GSM, JobEnum.LTW, JobEnum.WVR };
        }

        /// <summary>
        /// Gets JobEnums related to Disciples of the Land.
        /// </summary>
        /// <returns>Array of JobEnum with relevant values.</returns>
        public static JobEnum[] GetDisciplesOfLand()
        {
            return new JobEnum[] { JobEnum.BTN, JobEnum.FSH, JobEnum.MIN };
        }

        /// <summary>
        /// Gets JobEnums related to Disciples of War.
        /// </summary>
        /// <returns>Array of JobEnum with relevant values.</returns>
        public static JobEnum[] GetDisciplesOfWar()
        {
            return new JobEnum[] { JobEnum.BRD, JobEnum.DRG, JobEnum.DRK, JobEnum.GLD, JobEnum.MCH, JobEnum.MNK, JobEnum.NIN, JobEnum.SAM, JobEnum.WAR };
        }

        /// <summary>
        /// Gets JobEnums related to Disciples of the Magic.
        /// </summary>
        /// <returns>Array of JobEnum with relevant values.</returns>
        public static JobEnum[] GetDisciplesOfMagic()
        {
            return new JobEnum[] { JobEnum.AST, JobEnum.BLM, JobEnum.BLU, JobEnum.RDM, JobEnum.SCH, JobEnum.SMN, JobEnum.WHM };
        }
        #endregion
    }
}
