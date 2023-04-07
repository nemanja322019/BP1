using Projekat.DAO;
using Projekat.DAO.Impl;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Service
{
    public class DrzavaService
    {
        private static readonly IDrzavaDAO drzavaDAO = new DrzavaDAOImpl();

        public void PodaciODrzavi()
        {
            List<Drzava> drzave = drzavaDAO.FindAll().ToList();
            //drzave.ForEach(d => Console.WriteLine(d));

            foreach(Drzava drzava in drzave)
            {
                Console.WriteLine(Drzava.GetFormattedHeader());
                Console.WriteLine(drzava.ToString());
                List<Skok> skokovi = drzavaDAO.SkokoviUDrzavi(drzava.IdD);

                if (skokovi.Count() > 0)
                {
                    Console.WriteLine("    " + Skok.GetFormattedHeader());
                    foreach (Skok s in skokovi)
                    {
                        Console.WriteLine( "    " + s);
                    }
                }
                else
                {
                    Console.WriteLine("\tnema skakace koji zadovoljavaju uslove pretrage");
                }

            }

        }

    }
}
