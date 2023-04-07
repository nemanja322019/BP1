using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.UIHandler
{
    public class SkakaonicaUIHandler
    {
        private static readonly SkakaonicaService skakaonicaService = new SkakaonicaService();
        public void HandleSkakaonicaMenu()
        {
            string answer;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Odaberite opciju:");
                Console.WriteLine("1 - Izbroj skakaonice");
                Console.WriteLine("2 - Izbrisi skakaonicu");
                Console.WriteLine("3 - Izbrisi sve skakaonice");
                Console.WriteLine("4 - Pronadji skakaonicu");
                Console.WriteLine("5 - Prikazi skakaonice");
                Console.WriteLine("6 - Skakaonice odredjene duzine");
                Console.WriteLine("X - Izlazak iz programa");

                answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        IzbrojSkakaonice();
                        break;
                    case "2":
                        IzbrisiSkakaonicu();
                        break;
                    case "3":
                        IzbrisiSveSkakaonice();
                        break;
                    case "4":
                        PronadjiSkakaonicu();
                        break;
                    case "5":
                        PrikaziSkakaonice();
                        break;
                    case "6":
                        OdredjenaDuzina();
                        break;

                }
            } while (!answer.ToUpper().Equals("X"));
        }

        public void IzbrojSkakaonice()
        {
            Console.WriteLine("Broj skakaonica: " + skakaonicaService.Count());
        }

        public void IzbrisiSkakaonicu()
        {
            Console.WriteLine("Unesi id skakaonice:");
            string id=Console.ReadLine();
            skakaonicaService.DeleteById(id);
        }

        public void IzbrisiSveSkakaonice()
        {
            skakaonicaService.DeleteAll();
        }

        public void PronadjiSkakaonicu()
        {
            Console.WriteLine("Unesi id skakaonice:");
            string id = Console.ReadLine();
            skakaonicaService.FindById(id);
        }

        public void PrikaziSkakaonice()
        {
            skakaonicaService.FindAll();
        }

        public void OdredjenaDuzina()
        {
            Console.WriteLine("Unesi minimalnu duzinu: ");
            int min = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Unesi maksimalnu duzinu: ");
            int max = Int32.Parse(Console.ReadLine());
            if (min > max)
            {
                Console.WriteLine("Niste dobro uneli duzine!");
                return;
            }
            skakaonicaService.OdredjenaDuzina(min, max);
        }

    }
}
