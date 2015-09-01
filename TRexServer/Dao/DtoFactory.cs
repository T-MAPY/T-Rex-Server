using System;
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
                var b = ExistDataInDatabase(data.i);
                switch (b)
                {
                    case true:
                        UpdateDatabase(data);
                        break;
                    case false:
                        InsertToDatabase(data);
                        break;
                    case null:
                        Logger.Error("not connection to database");
                        break;
                }
            }
        }

        private bool? ExistDataInDatabase(string i)
        {
            try
            {
                using (var con = new OracleConnection(Settings.Default.ConnectionString))
                {
                    con.Open();

                    var cmd = new OracleCommand
                    {
                        Connection = con,
                        CommandText =
                            string.Format("SELECT MU_NAME FROM {0} WHERE MU_NAME = :i", Settings.Default.TableName)
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

        private void InsertToDatabase(StatusDTO data)
        {
            try
            {
                using (var con = new OracleConnection(Settings.Default.ConnectionString))
                {
                    con.Open();

                    var cmd = new OracleCommand
                    {
                        Connection = con,
                        CommandText =
                            string.Format(
                                "INSERT INTO {0} (MU_NAME, LONGITUDE, LATITUDE, AZIMUTH, POSITION_UPDATE) VALUES (:i, :o, :a, :b, TO_DATE(:t, 'YYYY-MM-DD HH24:MI:SS'))",
                                Settings.Default.TableName)
                    };

                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.i,
                        ParameterName = "i"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.a,
                        ParameterName = "a"
                    });
                    cmd.Parameters.Add(new OracleParameter
                    {
                        Value = data.o,
                        ParameterName = "o"
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
                        Value = data.t.ToString("yyyy-MM-dd HH:mm:ss"),
                        ParameterName = "t"
                    });

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void UpdateDatabase(StatusDTO data)
        {
            {
                try
                {
                    using (var con = new OracleConnection(Settings.Default.ConnectionString))
                    {
                        con.Open();

                        var cmd = new OracleCommand
                        {
                            Connection = con,
                            CommandText = string.Format(
                                "UPDATE {0} SET LONGITUDE = :o, LATITUDE = :a , AZIMUTH = :b, POSITION_UPDATE =  TO_DATE(:t, 'YYYY-MM-DD HH24:MI:SS') WHERE MU_NAME = '{1}'",
                                Settings.Default.TableName, data.i)
                        };

                        // does not work
                        //cmd.Parameters.Add(new OracleParameter
                        //{
                        //    Value = data.i,
                        //    ParameterName = "i"
                        //});
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.a,
                            ParameterName = "a"
                        });
                        cmd.Parameters.Add(new OracleParameter
                        {
                            Value = data.o,
                            ParameterName = "o"
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
                            Value = data.t.ToString("yyyy-MM-dd HH:mm:ss"),
                            ParameterName = "t"
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
    }
}