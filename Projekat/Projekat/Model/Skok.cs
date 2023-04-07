using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Skok
    {
        public string IdSk { get; set; }
        public int IdSc { get; set; }
        public string IdSa { get; set; }
        public double BDuzina { get; set; }
        public double BStil { get; set; }
        public double BVetar { get; set; }

        public Skok() { }

        public Skok(string IdSk)
        {
            this.IdSk = IdSk;
        }

        public Skok(string idSk, int idSc, string idSa, double bDuzina, double bStil, double bVetar)
        {
            IdSk = idSk;
            IdSc = idSc;
            IdSa = idSa;
            BDuzina = bDuzina;
            BStil = bStil;
            BVetar = bVetar;
        }

        public override string ToString()
        {
            return string.Format("{0,-8} {1,-8} {2,-8} {3,-30} {4,-30} {5,-8}",
                IdSk, IdSc, IdSa, BDuzina, BStil, BVetar);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-8} {1,-8} {2,-8} {3,-30} {4,-30} {5,-8}",
                "IDSK", "IdSc", "IDSA", "BDUZINA", "BSTIL", "BVETAR");
        }

    }
}
