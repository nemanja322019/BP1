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
    internal class SkokDAOImpl : ISkokDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Skok entity)
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
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(string id, IDbConnection connection)
        {
            string query = "select * from skok where idsk=:idsk";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idsk", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsk", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Skok> FindAll()
        {
            string query = "select * from skok";
            List<Skok> skokList = new List<Skok>();

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
                            Skok skok = new Skok(reader.GetString(0), reader.GetInt32(1),
                                reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4),reader.GetDouble(5));
                            skokList.Add(skok);
                        }
                    }
                }
            }

            return skokList;
        }

        public IEnumerable<Skok> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Skok FindById(string id)
        {
            string query = "select * from skok where idsk = :idsk";
            Skok skok = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsk", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsk", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            skok = new Skok(reader.GetString(0), reader.GetInt32(1),
                                reader.GetString(2), reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5));
                        }
                    }
                }

            }

            return skok;
        }

        public int Save(Skok entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Skok skok, IDbConnection connection)
        {
            // id_th intentionally in the last place, so that the order between commands remains the same
            string insertSql = "insert into skok (idsc, idsa, bduzina, bstil, bvetar, idsk) " +
                "values (:idsc, :idsa , :bduzina, :bstil, :bvetar, :idsk)";
            string updateSql = "update skok set idsc=:idsc, idsa=:idsa, " +
                "bduzina=:bduzina, bstil=:bstil, bvetar=:bvetar where idsk=:idsk";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(skok.IdSk,connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "idsc", DbType.Int32);
                ParameterUtil.AddParameter(command, "idsa", DbType.String, 50);
                ParameterUtil.AddParameter(command, "bduzina", DbType.Double);
                ParameterUtil.AddParameter(command, "bstil", DbType.Double);
                ParameterUtil.AddParameter(command, "bvetar", DbType.Double);
                ParameterUtil.AddParameter(command, "idsk", DbType.String,50);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsk", skok.IdSk);
                ParameterUtil.SetParameterValue(command, "idsc", skok.IdSc);
                ParameterUtil.SetParameterValue(command, "idsa", skok.IdSa);
                ParameterUtil.SetParameterValue(command, "bduzina", skok.BDuzina);
                ParameterUtil.SetParameterValue(command, "bstil", skok.BStil);
                ParameterUtil.SetParameterValue(command, "bvetar", skok.BVetar);

                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Skok> entities)
        {
            throw new NotImplementedException();
        }

        public List<Skok> NadjiSkokoveNaStazi(string idsa)
        {
            List<Skok> skokovi = new List<Skok>();
            foreach(Skok s in FindAll().ToList())
            {
                if(s.IdSa == idsa)
                {
                    skokovi.Add(s);
                }
            }
            return skokovi;
        }

        public int BrojSkakacaNaStazi(string idsa)
        {
            int brSkakaca = 0;
            string query = "select count(distinct idsc) from skok where idsa = :idsa";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsa", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsa", idsa);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brSkakaca = reader.GetInt32(0);
                        }
                    }
                }
            }
            return brSkakaca;
        }

        public double IzmeniBVetar(string s,double a)
        {
            Skok skok = FindById(s);
            skok.BVetar = a;
            double noviPBSC = skok.BStil + skok.BDuzina + skok.BVetar;
            double maxPBSC = NadjiMax();
            if(noviPBSC > maxPBSC)
            {
                return noviPBSC;
            }          
            Save(skok);
            return 0;
        }

        public double NadjiMax()
        {
            string query = "select max(pbsc) from skakac" ;
            double maxPBSC=0;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                   // ParameterUtil.AddParameter(command, "idsa", DbType.String);
                    command.Prepare();

                    //ParameterUtil.SetParameterValue(command, "idsa", idsa);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            maxPBSC = reader.GetInt32(0);
                        }
                    }
                }
            }
            return maxPBSC;
        }

    }
}
