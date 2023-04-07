using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Skakac
    {
        public int IdSc { get; set; }
        public string ImeSc { get; set; }
        public string PrzSc { get; set; }
        public string IdD { get; set; }
        public int Titule { get; set; }
        public double PbSc { get; set; }

        public Skakac() { }

        public Skakac(int IdSc)
        {
            this.IdSc = IdSc;
        }

        public Skakac(int idSc, string imeSc, string przSc, string idD, int titule, double pbSc)
        {
            IdSc = idSc;
            ImeSc = imeSc;
            PrzSc = przSc;
            IdD = idD;
            Titule = titule;
            PbSc = pbSc;
        }

        public override string ToString()
        {
            return string.Format("{0,-2} {1,-30} {2,-30} {3,-3} {4,-4} {5,-30}",
                IdSc, ImeSc, PrzSc, IdD, Titule, PbSc);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-2} {1,-30} {2,-30} {3,-3} {4,-4} {5,-30}",
                "IDSC", "IMESC", "PRZSC", "IDD", "TITULE", "PBSC");
        }

    }
}
