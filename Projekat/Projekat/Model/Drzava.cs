using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Drzava
    {
        public string IdD { get; set; }
        public string NazivD { get; set; }

        public Drzava(string idD, string nazivD)
        {
            IdD = idD;
            NazivD = nazivD;
        }

        public Drzava(string idD)
        {
            IdD = idD;
        }

        public Drzava() { }

        public override string ToString()
        {
            return string.Format("{0,-3} {1,-30}",
                IdD, NazivD);
        }

        public static string GetFormattedHeader()
        {
            return string.Format("{0,-3} {1,-30} ",
                "IDD", "NAZIVD");
        }
    }
}
