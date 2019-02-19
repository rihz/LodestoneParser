namespace LodestoneParser.Character
{
    /// <summary>
    /// Collection of objects to surface if an error is occurred during parsing.
    /// </summary>
    public class ErrorObjects
    {
        /// <summary>
        /// Error object for RepairInfo.
        /// </summary>
        public static RepairInfo RepairInfo = new RepairInfo(Enums.JobEnum.Unknown, -1, -1);
    }
}
