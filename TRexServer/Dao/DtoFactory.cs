using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NLog;
using Oracle.ManagedDataAccess.Client;
using TRexServer.Models;
using TRexServer.Properties;

namespace TRexServer.Dao
{
    public class DtoFactory
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void InsertOrUpdateDatabase(StatusDTO data)
        {
            if (data != null)
            {
                var connections = new List<Connection>();
                try
                {
                    connections = JsonConvert.DeserializeObject<List<Connection>>(Settings.Default.Connections);
                }
                catch (Exception e)
                {
                    Logger.Error("Deserialize configuration failed.");
                    Logger.Error(e);
                }

                var conString = GetConnectionString(data.i, connections);
                var tableName = GetTableName(data.i,connections);

                if (string.IsNullOrWhiteSpace(conString) && string.IsNullOrWhiteSpace(tableName))
                {
                    var b = ExistDataInDatabase(data.i, conString, tableName);
                    switch (b)
                    {
                        case true:
                            UpdateDatabase(data, conString, tableName);
                            break;
                        case false:
                            InsertToDatabase(data, conString, tableName);
                            break;
                        case null:
                            Logger.Error("not connection to database");
                            break;
                    }
                }
            }
        }

        private bool? ExistDataInDatabase(string i, string conString, string tableName)
        {
            try
            {
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    var cmd = new OracleCommand
                    {
                        Connection = con,
                        CommandText =
                            string.Format("SELECT MU_NAME FROM {0} WHERE MU_NAME = :i", tableName)
                    };

                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = i,
                        ParameterName = "i"
                    });

                    var data = cmd.ExecuteReader();

                    return data.HasRows;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return null;
            }
        }

        private void InsertToDatabase(StatusDTO data, string conString, string tableName)
        {
            try
            {
                using (var con = new OracleConnection(conString))
                {
                    con.Open();

                    var cmd = new OracleCommand
                    {
                        Connection = con,
                        CommandText =
                            string.Format(
                                "INSERT INTO {0} (MU_NAME, LONGITUDE, LATITUDE, AZIMUTH, ACTION_STATE, POSITION_UPDATE, STATE_UPDATE) VALUES (:i, :a, :o, :b, :st, TO_DATE(:t, 'YYYY-MM-DD HH24:MI:SS'), TO_DATE(:tt, 'YYYY-MM-DD HH24:MI:SS'))",
                                tableName)
                    };

                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.i,
                        ParameterName = "i"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.o,
                        ParameterName = "o"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.a,
                        ParameterName = "a"
                    });

                    // is not in table in database
                    //commmand.Parameters.Add(new OracleParameter
                    //{
                    //    Value = value.l,
                    //    ParameterName = "l"
                    //});
                    //commmand.Parameters.Add(new OracleParameter
                    //{
                    //    Value = value.s,
                    //    ParameterName = "s"
                    //});
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.b,
                        ParameterName = "b"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = "SHUTDOWN",
                        ParameterName = "st"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.t?.ToString("yyyy-MM-dd HH:mm:ss"),
                        ParameterName = "t"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.t?.ToString("yyyy-MM-dd HH:mm:ss"),
                        ParameterName = "tt"
                    });

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void UpdateDatabase(StatusDTO data, string conString, string tableName)
        {
            {
                try
                {
                    using (var con = new OracleConnection(conString))
                    {
                        con.Open();

                        var cmd = new OracleCommand
                        {
                            Connection = con,
                            CommandText = string.Format(
                                "UPDATE {0} SET LONGITUDE = :a, LATITUDE = :o , AZIMUTH = :b, ACTION_STATE = :st, POSITION_UPDATE = TO_DATE(:t, 'YYYY-MM-DD HH24:MI:SS'), STATE_UPDATE = TO_DATE(:t, 'YYYY-MM-DD HH24:MI:SS') WHERE MU_NAME = '{1}'",
                                tableName, data.i)
                        };

                        // does not work
                        //cmd.Parameters.Add(new OracleParameter
                        //{
                        //    Value = data.i,
                        //    ParameterName = "i"
                        //});
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.o,
                            ParameterName = "o"
                        });
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.a,
                            ParameterName = "a"
                        });

                        // is not in table in database
                        //commmand.Parameters.Add(new OracleParameter
                        //{
                        //    Value = value.l,
                        //    ParameterName = "l"
                        //});
                        //commmand.Parameters.Add(new OracleParameter
                        //{
                        //    Value = value.s,
                        //    ParameterName = "s"
                        //});
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.b,
                            ParameterName = "b"
                        });
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = "SHUTDOWN",
                            ParameterName = "st"
                        });
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.t?.ToString("yyyy-MM-dd HH:mm:ss"),
                            ParameterName = "t"
                        });
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.t?.ToString("yyyy-MM-dd HH:mm:ss"),
                            ParameterName = "tt"
                        });

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
            }
        }

        private string GetConnectionString(string id, List<Connection> connections)
        {
            foreach (var connection in connections)
            {
                if (connection.Ids.Contains(id))
                {
                    return connection.ConnectionString;
                }
            }

            return Settings.Default.DefaultConnectionString;
        }

        private string GetTableName(string id, List<Connection> connections)
        {
            foreach (var connection in connections)
            {
                if (connection.Ids.Contains(id))
                {
                    return connection.TableName;
                }
            }

            return Settings.Default.DefaultTableName;
        }
    }
}