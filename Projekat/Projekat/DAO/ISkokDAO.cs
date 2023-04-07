using ODP_NET_Skakaonica.DAO;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.DAO
{
    public interface ISkokDAO : ICRUDDao<Skok,string>
    {
        List<Skok> NadjiSkokoveNaStazi(string idsa);
        int BrojSkakacaNaStazi(string idsa);
        double IzmeniBVetar(string s,double a);
    }
}
