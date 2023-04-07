using Projekat.Connection;
using Projekat.Model;
using Projekat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.DAO.Impl
{
    public class DrzavaDAOImpl : IDrzavaDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Drzava entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drzava> FindAll()
        {
            string query = "select * from drzava";
            List<Drzava> drzavaList = new List<Drzava>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Drzava drzava = new Drzava(reader.GetString(0), reader.GetString(1));
                            drzavaList.Add(drzava);
                        }
                    }
                }
            }

            return drzavaList;
        }

        public IEnumerable<Drzava> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Drzava FindById(string id)
        {
            throw new NotImplementedException();
        }

        public int Save(Drzava entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll(IEnumerable<Drzava> entities)
        {
            throw new NotImplementedException();
        }
        
        public List<Skok> SkokoviUDrzavi(string idd)
        {
            string query = "select * from skok sk join skakac sc on sk.idsc = sc.idsc inner join skakaonica sa on sk.idsa = sa.idsa where sc.idd = :idd and sa.idd = :idd";
            List<Skok> skokList = new List<Skok>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idd", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idd", idd);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Skok skok = new Skok(reader.GetString(0), reader.GetInt32(1),
                                reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));
                            skokList.Add(skok);
                        }
                    }
                }
            }
            return skokList;
        }

    }
}
