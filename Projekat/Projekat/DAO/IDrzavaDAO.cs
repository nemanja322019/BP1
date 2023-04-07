using ODP_NET_Skakaonica.DAO;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.DAO
{
    public interface IDrzavaDAO : ICRUDDao<Drzava,string>
    {
        List<Skok> SkokoviUDrzavi(string idd);
    }
}
