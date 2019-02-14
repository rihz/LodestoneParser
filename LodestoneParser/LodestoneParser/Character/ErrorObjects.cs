using System;
using System.Collections.Generic;
using System.Text;

namespace LodestoneParser.Character
{
    /// <summary>
    /// Collection of objects to surface if an error is occurred during parsing.
    /// </summary>
    public class ErrorObjects
    {
        public static RepairInfo RepairInfo = new RepairInfo(Enums.JobEnum.Unknown, -1, -1);
    }
}
