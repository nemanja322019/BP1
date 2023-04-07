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
    internal class SkakaonicaDAOImpl : ISkakaonicaDAO
    {
        public int Count()
        {
            string query = "select count(*) from skakaonica";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int Delete(Skakaonica entity)
        {
            return DeleteById(entity.ToString());
        }

        public int DeleteAll()
        {
            string query = "delete from skakaonica";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteById(string id)
        {
            string query = "delete from skakaonica where idsa=:idsa";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsa", DbType.String);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "idsa", id);
                    return command.ExecuteNonQuery();
                }
            }
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
            string query = "select * from skakaonica where idsa=:idsa";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "idsa", DbType.String);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsa", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Skakaonica> FindAll()
        {
            string query = "select * from skakaonica";
            List<Skakaonica> skakaonicaList = new List<Skakaonica>();

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
                            Skakaonica skakaonica = new Skakaonica(reader.GetString(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                            skakaonicaList.Add(skakaonica);
                        }
                    }
                }
            }

            return skakaonicaList;
        }

        public IEnumerable<Skakaonica> FindAllById(IEnumerable<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from skakaonica where idsa in (");
            foreach (string id in ids)
            {
                sb.Append(":id" + id + ",");
            }
            sb.Remove(sb.Length - 1, 1); // delete last ','
            sb.Append(")");

            List<Skakaonica> skakaonicaList = new List<Skakaonica>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    foreach (string id in ids)
                    {
                        ParameterUtil.AddParameter(command, "id" + id, DbType.Int32);
                    }
                    command.Prepare();

                    foreach (string id in ids)
                    {
                        ParameterUtil.SetParameterValue(command, "id" + id, id);
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Skakaonica skakaonica = new Skakaonica(reader.GetString(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                            skakaonicaList.Add(skakaonica);
                        }
                    }
                }
            }

            return skakaonicaList;
        }

        public Skakaonica FindById(string id)
        {
            string query = "select * from skakaonica where idsa = :idsa";
            Skakaonica skakaonica = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "idsa", DbType.String);
                    command.Prepare();

                    ParameterUtil.SetParameterValue(command, "idsa", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            skakaonica = new Skakaonica(reader.GetString(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                        }
                    }
                }

            }

            return skakaonica;
        }

        public int Save(Skakaonica entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Skakaonica skakaonica, IDbConnection connection)
        {
            // id_th intentionally in the last place, so that the order between commands remains the same
            string insertSql = "insert into skakaonica (nazivsa, duzinasa, tipsa, idd, idsa) " +
                "values (:nazivsa, :duzinasa , :tipsa, :idd, :idsa)";
            string updateSql = "update skakaonica set nazivsa=:nazivsa, duzinasa=:duzinasa, " +
                "tipsa=:tipsa, idd=:idd where idsa=:idsa";
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(skakaonica.IdSa, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "nazivsa", DbType.String, 50);
                ParameterUtil.AddParameter(command, "duzinasa", DbType.Int32);
                ParameterUtil.AddParameter(command, "tipsa", DbType.String, 50);
                ParameterUtil.AddParameter(command, "idd", DbType.String, 50);
                ParameterUtil.AddParameter(command, "idsa", DbType.String,50);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "idsa", skakaonica.IdSa);
                ParameterUtil.SetParameterValue(command, "nazivsa", skakaonica.NazivSa);
                ParameterUtil.SetParameterValue(command, "duzinasa", skakaonica.DuzinaSa);
                ParameterUtil.SetParameterValue(command, "tipsa", skakaonica.TipSa);
                ParameterUtil.SetParameterValue(command, "idd", skakaonica.IdD);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<Skakaonica> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction(); // transaction start

                int numSaved = 0;

                // insert or update every theatre
                foreach (Skakaonica entity in entities)
                {
                    // changes are visible only to current connection
                    numSaved += Save(entity, connection);
                }

                // transaction ends successfully, changes are now visible to other connections as well
                transaction.Commit();

                return numSaved;
            }
        }

        public List<Skakaonica> OdredjenaDuzina(int min, int max)
        {
            List<Skakaonica> skakaonicaList = FindAll().ToList();
            List<Skakaonica> odgovara = new List<Skakaonica>();
            foreach(Skakaonica s in skakaonicaList)
            {
                if(s.DuzinaSa >= min && s.DuzinaSa <= max)
                {
                    odgovara.Add(s);
                }
            }
            return odgovara;
        }

        public string NazivDrzave(string idd)
        {
            string query = "select * from drzava where idd = :idd";
            string nazivD="";

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
                        if (reader.Read())
                        {
                            Drzava d = new Drzava(reader.GetString(0),reader.GetString(1));
                            nazivD = d.NazivD;
                        }
                    }
                }

            }

            return nazivD;
        }
    }
}
