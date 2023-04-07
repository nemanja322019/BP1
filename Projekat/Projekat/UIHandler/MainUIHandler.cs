using Projekat.Model;
using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.UIHandler
{
    public class MainUIHandler
    {
        private static readonly SkakaonicaUIHandler skakaonicaUIHandler = new SkakaonicaUIHandler();
        private static readonly SkokService skokService = new SkokService();
        private static readonly DrzavaService drzavaService = new DrzavaService();
        private static readonly SkakacService skakacService = new SkakacService();
        public void HandleMainMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Prikazi skokove");
                Console.WriteLine("2 - Prikazi podatke o drzavi");
                Console.WriteLine("3 - Izmeni BVETAR");
                Console.WriteLine("4 - Skakaonica meni");
                Console.WriteLine("X - Izlazak iz programa");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        PrikaziSkokove();
                        break;
                    case "2":
                        SveODrzavi();
                        break;
                    case "3":
                        IzmeniBVetar();
                        break;
                    case "4":
                        skakaonicaUIHandler.HandleSkakaonicaMenu();
                        break;

                }
            } while (!answer.ToUpper().Equals("X"));
        }

        private void PrikaziSkokove()
        {
            Console.WriteLine("IDSA: ");
            string idsa=Console.ReadLine();

            Console.WriteLine(Skok.GetFormattedHeader());
            skokService.SviSkokoviNaStazi(idsa).ForEach(x => Console.WriteLine(x));

            Console.WriteLine("Broj skakaca: " + skokService.BrojSkakacaNaStazi(idsa));
        }

        public void SveODrzavi()
        {
            drzavaService.PodaciODrzavi();
        }

        public void IzmeniBVetar()
        {
            Console.WriteLine("Unesi id skoka: ");
            string s = Console.ReadLine();
            Console.WriteLine("Unesi nove bodove za Vetar: ");
            double a = Double.Parse(Console.ReadLine());

            double PBSC = skokService.IzmeniBVetar(s,a);
            if(PBSC > 0)
            {
                skakacService.NoviPBSC(s,PBSC);
            }
        }

    }
}
