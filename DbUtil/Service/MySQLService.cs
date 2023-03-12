using System;
using System.Collections.Generic;
using System.Data;
using DbUtil.Entities;
using DbUtil.IService;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace DbUtil.Service
{
    public class MySQLService: IDbService
    {
        private readonly string connection_string;
        public MySQLService(string connectionstring)
        {
            connection_string = connectionstring;
        }

        /// <summary>
        /// Generate the connection string from the datasource object
        /// </summary>
        /// <param name="datasource">The datasource object</param>
        /// <returns></returns>
        public string GenerateConnectionJson(string host, string database, string username, string password,string port)
        {
            var job = new JObject
            {
                { "Uid", username },
                { "Pwd", password },
                { "Server", host },
                { "Port", port },
                { "Database", database }
            };

            return job.ToString();
            //return string.Format("Server={0};Database={1};User Id={2};Password = {3}; ", host, database, username, password);
        }

        // <summary>
        /// Inserts fields and values to a specified table
        /// </summary>
        /// <param name="table">The table to insert values to</param>
        /// <param name="fields">The fields of the table to insert to</param>
        /// <param name="values">The values to insert</param>
        /// <returns>Returns null if the an error occurs during execution or the values after insert</returns>
        public DataTable InsertData(PostParameters parameters)
        {
            string[] fieldVals = parameters.fields;
            object[] valData = parameters.values;
            if (fieldVals.Length != valData.Length) { return null; }
            string query = string.Format("INSERT INTO {0} (", parameters.table);
            string part2 = "";
            for (int i = 0; i < fieldVals.Length; i++)
            {
                if (i == (fieldVals.Length - 1))
                {
                    query += fieldVals[i];
                    part2 += string.Format("@{0}", fieldVals[i]);
                }
                else
                {
                    query += fieldVals[i] + ", ";
                    part2 += string.Format("@{0}, ", fieldVals[i]);
                }
            }
            query += ") VALUES (";
            string sql = query + part2 + ");  ";// + string.Join(',',parameters.fields) + ";";



            MySqlConnection mc = new MySqlConnection(connection_string);
            //string sql = genInsertQuery(table, fields, values);
            MySqlCommand msc = new MySqlCommand(sql, mc);

            for(int i = 0; i < fieldVals.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", fieldVals[i]), valData[i]);
            }


            DataTable dt =null;



            mc.Open();
            //int retVal = -1;
            try
            {
                dt = new DataTable();
                var adapter = new MySqlDataAdapter(msc);
                adapter.Fill(dt);


                //retVal = msc.ExecuteNonQuery();

                //if (retVal > -1)
                //{
                //    dt = new DataTable();
                //    var adapter = new MySqlDataAdapter(msc);
                //    adapter.Fill(dt);
                //}


            }
            catch (MySqlException e)
            {
                throw new ArgumentException("MySQL insert exception", nameof(parameters), e);
            }
            finally
            {
                mc.Close();
            }
            return dt;
        }

        public int mquery(string cmd)
        {
            MySqlConnection mc = new MySqlConnection(connection_string);
            MySqlCommand msc = new MySqlCommand(cmd, mc);
            mc.Open();
            int retVal = -1;
            try
            {
                retVal = msc.ExecuteNonQuery();
            }
            catch (MySqlException)
            { }
            finally
            {
                mc.Close();
            }
            return retVal;
        }

        public  string genInsertQuery(string table, object fields, object values)
        {
            string[] fieldVals = fields as string[];
            string[] valData = values as string[];
            if (fieldVals.Length != valData.Length) { return "Number of fields not equal to values."; }
            string query = string.Format("INSERT INTO {0} (", table);
            string part2 = "";
            for (int i = 0; i < fieldVals.Length; i++)
            {
                if (i == (fieldVals.Length - 1)) {
                    query += fieldVals[i];
                    part2 += string.Format("@{0}", fieldVals[i]);
                }
                else {
                    query += fieldVals[i] + ", ";
                    part2 += string.Format("@{0}, ", fieldVals[i]);
                }
            }
            query += ") VALUES(";
            return query + part2 + ")";
        }

        /// <summary>
        /// Runs an sql update query on the specified table with the specified parameters
        /// </summary>
        /// <param name="table">The table to perform the query on</param>
        /// <param name="toAlter">The field value(s) to alter</param>
        /// <param name="alValue">the new values of the specified field(s)</param>
        /// <param name="args">paramters for where clause</param>
        /// <param name="values">values for where clause</param>
        /// <param name="append">append any other desired value for the query, eg. ORDER BY</param>
        /// <returns></returns>
        public int Update(PutParameters parameters)
        {
            int result = -1;
            MySqlConnection mc = new MySqlConnection(connection_string);
            string clause = string.Format("WHERE {0}= @{0}", parameters.args[0]);
            string update = string.Format("SET {0} = @{0}", parameters.toAlter[0]);
            for (int i = 1; i < parameters.toAlter.Length; i++)
            {
                update += string.Format(", {0} = @{0}", parameters.toAlter[i], parameters.alValues[i]);
            }
            for (int i = 1; i < parameters.args.Length; i++)
            {
                clause += string.Format(" AND {0}= @{1}", parameters.args[i], parameters.values[i]);
            }
            string sql = string.Format("UPDATE {0} {1} {2}", parameters.table, update, clause);
            MySqlCommand msc = new MySqlCommand(sql, mc);

            for (int i = 0; i < parameters.toAlter.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", parameters.toAlter[i]), parameters.alValues[i]);
            }

            for (int i = 0; i < parameters.args.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", parameters.args[i]), parameters.values[i]);
            }
            mc.Open();
            try
            {
                result = msc.ExecuteNonQuery();
            }
            catch (Exception e) {
                throw new ArgumentException("MySQL update exception", nameof(parameters), e);
            }
            finally { mc.Close(); }
            return result;
        }


        public int Delete(DeleteParameters parameters)
        {
            int result = -1;
            MySqlConnection mc = new MySqlConnection(connection_string);
            string clause = string.Format("WHERE {0}= @{0}", parameters.args[0]);
           
            for (int i = 1; i < parameters.args.Length; i++)
            {
                clause += string.Format(" AND {0}= @{0}", parameters.args[i]);
            }
            string sql = string.Format("DELETE FROM {0} {1}", parameters.table, clause);
            MySqlCommand msc = new MySqlCommand(sql, mc);

            
            for (int i = 0; i < parameters.args.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", parameters.args[i]), parameters.values[i]);
            }
            mc.Open();
            try
            {
                result = msc.ExecuteNonQuery();
            }
            catch (Exception e) {
                throw new ArgumentException("MySQL delete exception", nameof(parameters), e);
            }
            finally { mc.Close(); }
            return result;
        }


       
        public DataTable SelectData(QueryParameters parameters)
        {
            MySqlConnection mc = new MySqlConnection(connection_string);

            string clause = "";
            string append = "";


            if ((parameters.args != null && parameters.values != null ) && parameters.args.Length > 0 && parameters.args.Length == parameters.values.Length)
            {
                //clause = string.Format("WHERE {0}= '{1}'", parameters.args[0], parameters.values[0]);
                //for (int i = 1; i < parameters.args.Length; i++)
                //{
                //    clause += string.Format(" AND {0}= '{1}' ", parameters.args[i], parameters.values[i]);
                //}

                clause = string.Format("WHERE {0}= @{0}", parameters.args[0]);
                for (int i = 1; i < parameters.args.Length; i++)
                {
                    clause += string.Format(" AND {0}= @{0} ", parameters.args[i]);
                }

            }

            string fieldvalues = " * ";

            if (parameters.fields != null && parameters.fields.Length > 0)
            {
                fieldvalues = string.Join(',', parameters.fields);
            }

            //check if there are order by parameters
            if (parameters.orderBy != null || parameters.orderByDesc != null)
            {
                append = " ORDER BY ";

                if (parameters.orderBy != null)
                {
                    append += string.Join(',', parameters.orderBy);
                }
             

                if (parameters.orderByDesc != null)
                {
                    string[] orderByDesc = new string[parameters.orderByDesc.Length];
                    for(var i = 0; i < parameters.orderByDesc.Length; i++)
                    {
                         orderByDesc[i]= parameters.orderByDesc[i] + " DESC";
                    }

                    if (parameters.orderBy == null)
                    {
                        append += string.Join(',', orderByDesc);
                    }
                    else
                    {
                        append +=" ,"+ string.Join(',', orderByDesc);
                    }
                   
                }
            }

            //check if there is pagination
            if (parameters.PageNumber > 0)
            {
                append += " LIMIT " + parameters.PageSize + " OFFSET " + ((parameters.PageNumber - 1) * parameters.PageSize);
            }


            string sql = string.Format("SELECT {0} FROM {1} {2}", fieldvalues, parameters.table, clause) + append;
            MySqlCommand msc = new MySqlCommand(sql, mc);

            if ((parameters.args != null && parameters.values != null) && parameters.args.Length > 0 && parameters.args.Length == parameters.values.Length)
            {
                for (int i = 0; i < parameters.args.Length; i++)
                {
                    msc.Parameters.AddWithValue(string.Format("@{0}", parameters.args[i]), parameters.values[i]);
                }
            }


            try
            {
                mc.Open();
            }
            catch
            {
                return null;
            }
            var dt = new DataTable();


            try
            {
                var adapter = new MySqlDataAdapter(msc);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MySQL select exception", nameof(parameters), ex);
            }
            finally
            {
              
                mc.Close();
            }

        }

        public List<TableInfo> GetTables()
        {
            string sql = "SELECT table_schema as 'table_schema', table_name as 'table_name', table_rows as 'rows' FROM INFORMATION_SCHEMA.TABLES; ";

            MySqlConnection mc = new MySqlConnection(connection_string);

            MySqlCommand msc = new MySqlCommand(sql, mc);

            return GetCollection<TableInfo>(msc);
        }

        public List<ColumnInfo> GetColumns(string table_schema,string table_name)
        {
            string sql = "SELECT column_name as 'column_name',is_nullable as 'is_nullable',data_type as 'data_type' FROM information_schema.columns WHERE table_schema = @table_schema AND table_name = @table_name; ";

            MySqlConnection mc = new MySqlConnection(connection_string);

            MySqlCommand cmd = new(sql, mc);
            cmd.Parameters.AddWithValue("@table_schema", table_schema);
            cmd.Parameters.AddWithValue("@table_name", table_name);

            return GetCollection<ColumnInfo>(cmd);
        }


        public  List<T> GetCollection<T>(MySqlCommand command)
        {
            
            try
            {
                var conn = new MySqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new MySqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return DataTableUtil.ConvertDataTable<T>(dt);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MySQL getcollection exception", nameof(command), ex);
            }

        }

        /// <summary>
        /// Run a query agains the database
        /// </summary>
        /// <param name="query">The query to be run against the database</param>
        /// <returns>int showing the outcome of the query</returns>
        public int RunQuery(string query)
        {
            var conn = new MySqlConnection(connection_string);
            try
            {
                var cmd = new MySqlCommand(query,conn);
                conn.Open();

                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MSSQL Query exception", query, ex);
            }
            finally
            {
                conn.Close();
            }

        }

        public void RunQuery(List<string> queries)
        {
            try
            {
                using (var con= new MySqlConnection(connection_string))
                {
                    foreach (var q in queries)
                    {
                        using var cmd = new MySqlCommand(q, con);
                        cmd.ExecuteNonQuery();
                    }
                  
                }
              
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MSSQL Query exception", queries.ToString(), ex);
            }

        }
    }
}
