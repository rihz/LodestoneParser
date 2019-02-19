using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LodestoneParser.Enums
{
    public static class Disciplines
    {
        public static JobEnum[] GetDisciplesOfHand()
        {
            return new JobEnum[] { JobEnum.ARM, JobEnum.BSM, JobEnum.CRP, JobEnum.CUL, JobEnum.GSM, JobEnum.LTW, JobEnum.WVR };
        }

        public static JobEnum[] GetDisciplesOfLand()
        {
            return new JobEnum[] { JobEnum.BTN, JobEnum.FSH, JobEnum.MIN };
        }

        public static JobEnum[] GetDisciplesOfWar()
        {
            return new JobEnum[] { JobEnum.BRD, JobEnum.DRG, JobEnum.DRK, JobEnum.GLD, JobEnum.MCH, JobEnum.MNK, JobEnum.NIN, JobEnum.SAM, JobEnum.WAR };
        }

        public static JobEnum[] GetDisciplesOfMagic()
        {
            return new JobEnum[] { JobEnum.AST, JobEnum.BLM, JobEnum.BLU, JobEnum.RDM, JobEnum.SCH, JobEnum.SMN, JobEnum.WHM };
        }
    }
}
