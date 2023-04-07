using ODP_NET_Skakaonica.DAO;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.DAO
{
    public interface ISkakaonicaDAO : ICRUDDao<Skakaonica,string>
    {
         List<Skakaonica> OdredjenaDuzina(int min, int max);
         String NazivDrzave(string idd);
    }
}
