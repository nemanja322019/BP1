using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Skakaonica
    {
        public string IdSa { get; set; }
        public string NazivSa { get; set; }
        public int DuzinaSa { get; set; }
        public string TipSa { get; set; }
        public string IdD { get; set; }

        public Skakaonica() { }

        public Skakaonica(string IdSa)
        {
            this.IdSa = IdSa;
        }

        public Skakaonica(string idSa, string nazivSa, int duzinaSa, string tipSa, string iDD)
        {
            IdSa = idSa;
            NazivSa = nazivSa;
            DuzinaSa = duzinaSa;
            TipSa = tipSa;
            IdD = iDD;
        }

        public override string ToString()
        {
            return string.Format("{0,-6} {1,-30} {2,-8} {3,-8} {4,-3}",
                IdSa, NazivSa, DuzinaSa, TipSa, IdD);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-6} {1,-30} {2,-8} {3,-8} {4,-3}",
                "IDSA", "NAZIVSA", "DUZINASA", "TIPSA", "IDD");
        }

    }
}
