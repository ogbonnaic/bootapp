using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Serilog;

namespace Bootapp.Util
{
    public class DbOperations
    {
        public static DataSet GetDataSet(NpgsqlCommand command, string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return null;
            }

        }
        public static DataTable GetDataTable(NpgsqlCommand command,string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection();
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return dt;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return null;
            }

        }
        public static JArray GetJArray(NpgsqlCommand command, string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);


                var json_string = JsonConvert.SerializeObject(dt);

                return JArray.Parse(json_string);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return null;
            }

        }
        public static List<T> GetCollection<T>(NpgsqlCommand command, string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return ConvertDataTable<T>(dt);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return null;
            }

        }
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            if (dt == null) return null;

            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static void ExecuteNonQuery(NpgsqlCommand command, string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                conn.Open();
                command.Connection = conn;
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
            }
        }


        public static object ExecuteScaler(NpgsqlCommand command, string connection_string)
        {
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                conn.Open();
                command.Connection = conn;
                var result =  command.ExecuteScalar();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                return null;
            }
        }


        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (dr[column.ColumnName] != System.DBNull.Value)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }

}

