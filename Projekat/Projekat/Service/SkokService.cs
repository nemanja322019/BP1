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
    public class SkokService
    {
        private static readonly ISkokDAO skokDAO = new SkokDAOImpl();

        public List<Skok> SviSkokoviNaStazi(string idsa)
        {
            return skokDAO.NadjiSkokoveNaStazi(idsa);
        }

        public int BrojSkakacaNaStazi(string idsa)
        {
            return skokDAO.BrojSkakacaNaStazi(idsa);
        }

        public double IzmeniBVetar(string s,double a)
        {
            return skokDAO.IzmeniBVetar(s,a);
        }

    }
}
