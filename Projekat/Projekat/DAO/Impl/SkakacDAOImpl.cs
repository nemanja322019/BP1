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
    public class SkakacDAOImpl : ISkakacDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Skakac entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(int id)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(int id, IDbConnection connection)
        {
            string query = "select * from skakac where idsc=:idsc";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idsc", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsc", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Skakac> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Skakac> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public Skakac FindById(int id)
        {
            string query = "select * from skakac where idsc = :idsc";
            Skakac skakac = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsc", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsc", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            skakac = new Skakac(reader.GetInt32(0), reader.GetString(1),
                                reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetFloat(5));
                        }
                    }
                }

            }

            return skakac;
        }

        public void NoviPBSC(string s,double PBSC)
        {
            int idsc = PronadjiIDSC(s);
            Skakac skakac = FindById(idsc);
            skakac.PbSc = PBSC;
            Save(skakac);
        }

        public int PronadjiIDSC(string idsk)
        {
            string query = "select idsc from skok where idsk = :idsk";
            int idsc=0;
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsk", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsk", idsk);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idsc = reader.GetInt32(0);
                        }
                    }
                }
            }
            return idsc;
        }

        public int Save(Skakac entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Skakac skakac, IDbConnection connection)
        {
            // id_th intentionally in the last place, so that the order between commands remains the same
            string insertSql = "insert into skakac (idsc, imesc, przsc, idd, titule, pbsc) " +
                "values (:idsc, :imesc , :przsc, :idd, :titule, :pbsc)";
            string updateSql = "update skakac set idsc=:idsc, imesc=:imesc, " +
                "przsc=:przsc, idd=:idd, titule=:titule, pbsc=:pbsc where idsc=:idsc";
            using (IDbCommand command = connection.CreateCommand())
            {
                //Console.WriteLine();
                command.CommandText = ExistsById(skakac.IdSc, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "idsc", DbType.Int32);
                ParameterUtil.AddParameter(command, "imesc", DbType.String, 50);
                ParameterUtil.AddParameter(command, "przsc", DbType.String);
                ParameterUtil.AddParameter(command, "idd", DbType.String);
                ParameterUtil.AddParameter(command, "titule", DbType.Int32);
                ParameterUtil.AddParameter(command, "pbsc", DbType.Double);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsc", skakac.IdSc);
                ParameterUtil.SetParameterValue(command, "imesc", skakac.ImeSc);
                ParameterUtil.SetParameterValue(command, "przsc", skakac.PrzSc);
                ParameterUtil.SetParameterValue(command, "idd", skakac.IdD);
                ParameterUtil.SetParameterValue(command, "titule", skakac.Titule);
                ParameterUtil.SetParameterValue(command, "pbsc", skakac.PbSc);

                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Skakac> entities)
        {
            throw new NotImplementedException();
        }
    }
}
