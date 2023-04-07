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
    public class SkakaonicaService
    {
        private static readonly ISkakaonicaDAO skakaonicaDAO = new SkakaonicaDAOImpl();
        
        public int Count()
        {
            return skakaonicaDAO.Count();
        }

        public void DeleteAll()
        {
            skakaonicaDAO.DeleteAll();
        }

        public void DeleteById(string id)
        {
            skakaonicaDAO.DeleteById(id);
        }

        public void FindById(string id)
        {
            Console.WriteLine(skakaonicaDAO.FindById(id));
        }

        public void FindAll()
        {
            List<Skakaonica> skakaonice = new List<Skakaonica>();
            skakaonice = skakaonicaDAO.FindAll().ToList();
            Console.WriteLine(Skakaonica.GetFormattedHeader());
            skakaonice.ForEach(s => Console.WriteLine(s));
        }

        public void Save(Skakaonica entity)
        {
            skakaonicaDAO.Save(entity);
        }

        public void OdredjenaDuzina(int min,int max)
        {
            List<Skakaonica> skakaonicaList = skakaonicaDAO.OdredjenaDuzina(min,max);
            
            foreach(Skakaonica s in skakaonicaList)
            {
                Console.WriteLine(s + "\t" + skakaonicaDAO.NazivDrzave(s.IdD));
            }
        }
    }
}
