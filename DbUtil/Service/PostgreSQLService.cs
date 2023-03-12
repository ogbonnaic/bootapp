using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbUtil.Entities;
using DbUtil.IService;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace DbUtil.Service
{
    public class PostgreSQLService: IDbService
    {
        private readonly string connection_string;
        public PostgreSQLService(string connectionstring)
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
                { "user_id", username },
                { "password", password },
                { "host", host },
                { "port", port },
                { "database", database }
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
                    part2 += string.Format(":{0}", fieldVals[i]);
                }
                else
                {
                    query += fieldVals[i] + ", ";
                    part2 += string.Format(":{0}, ", fieldVals[i]);
                }
            }
            query += ") VALUES(";
            string sql= query + part2 + ") RETURNING *";



            NpgsqlConnection mc = new NpgsqlConnection(connection_string);
            //string sql = genInsertQuery(table, fields, values);
            NpgsqlCommand msc = new NpgsqlCommand(sql, mc);

            var columns = GetColumns("public", parameters.table);

            for(int i = 0; i < fieldVals.Length; i++)
            {
                var column = columns.FirstOrDefault(e => e.column_name == fieldVals[i]);

                if (column != null && column.data_type == "uuid")
                {
                    msc.Parameters.AddWithValue(string.Format(":{0}", fieldVals[i]), Guid.Parse(valData[i].ToString()));
                }
                else
                {
                    msc.Parameters.AddWithValue(string.Format(":{0}", fieldVals[i]), valData[i]);
                }
            }


            DataTable dt =null;



            mc.Open();
            //int retVal = -1;
            try
            {
                dt = new DataTable();
                var adapter = new NpgsqlDataAdapter(msc);
                adapter.Fill(dt);

                //retVal = msc.ExecuteNonQuery();

                //if (retVal > -1)
                //{
                //    dt = new DataTable();
                //    var adapter = new NpgsqlDataAdapter(msc);
                //    adapter.Fill(dt);
                //}

              
            }
            catch (NpgsqlException e)
            {
                throw new ArgumentException("PGSQL insert exception", nameof(parameters), e);
            }
            finally
            {
                mc.Close();
            }
            return dt;
        }

        public int mquery(string cmd)
        {
            NpgsqlConnection mc = new NpgsqlConnection(connection_string);
            NpgsqlCommand msc = new NpgsqlCommand(cmd, mc);
            mc.Open();
            int retVal = -1;
            try
            {
                retVal = msc.ExecuteNonQuery();
            }
            catch (NpgsqlException)
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
                    part2 += string.Format(":{0}", fieldVals[i]);
                }
                else {
                    query += fieldVals[i] + ", ";
                    part2 += string.Format(":{0}, ", fieldVals[i]);
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
            NpgsqlConnection mc = new NpgsqlConnection(connection_string);
            string clause = string.Format("WHERE {0}= :{0}", parameters.args[0]);
            string update = string.Format("SET {0} = :{0}", parameters.toAlter[0]);
            for (int i = 1; i < parameters.toAlter.Length; i++)
            {
                update += string.Format(", {0} = :{0}", parameters.toAlter[i]);
                //update += string.Format(", {0} = :{0}", parameters.toAlter[i], parameters.alValues[i]);
            }
            for (int i = 1; i < parameters.args.Length; i++)
            {
                clause += string.Format(" AND {0}= :{1}", parameters.args[i], parameters.values[i]);
            }
            string sql = string.Format("UPDATE {0} {1} {2}", parameters.table, update, clause);
            NpgsqlCommand msc = new NpgsqlCommand(sql, mc);

            for (int i = 0; i < parameters.toAlter.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format(":{0}", parameters.toAlter[i]), parameters.alValues[i]);
            }

            for (int i = 0; i < parameters.args.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format(":{0}", parameters.args[i]), parameters.values[i]);
            }
            mc.Open();
            try
            {
                result = msc.ExecuteNonQuery();
            }
            catch (Exception e) {
                throw new ArgumentException("PGSQL update exception", nameof(parameters), e);
            }
            finally { mc.Close(); }
            return result;
        }


        public int Delete(DeleteParameters parameters)
        {
            if (parameters.args.Length != parameters.values.Length)
            {
                return 0;
            }
            int result = -1;
            NpgsqlConnection mc = new NpgsqlConnection(connection_string);
            string clause = string.Format("WHERE {0}= :{0}", parameters.args[0]);
           
            for (int i = 1; i < parameters.args.Length; i++)
            {
                clause += string.Format(" AND {0}= :{0}", parameters.args[i]);
            }
            string sql = string.Format("DELETE FROM {0} {1}", parameters.table, clause);
            NpgsqlCommand msc = new NpgsqlCommand(sql, mc);

            
            for (int i = 0; i < parameters.args.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format(":{0}", parameters.args[i]), parameters.values[i]);
            }
            mc.Open();
            try
            {
                result = msc.ExecuteNonQuery();
            }
            catch (Exception e) {
                throw new ArgumentException("PGSQL delete exception", nameof(parameters), e);
            }
            finally { mc.Close(); }
            return result;
        }

       
        public DataTable SelectData(QueryParameters parameters)
        {
            NpgsqlConnection mc = new NpgsqlConnection(connection_string);

            string clause = "";
            string append = "";


            if ((parameters.args != null && parameters.values != null ) && parameters.args.Length > 0 && parameters.args.Length == parameters.values.Length)
            {
                //clause = string.Format("WHERE {0}= '{1}'", parameters.args[0], parameters.values[0]);
                //for (int i = 1; i < parameters.args.Length; i++)
                //{
                //    clause += string.Format(" AND {0}= '{1}' ", parameters.args[i], parameters.values[i]);
                //}

                clause = string.Format("WHERE {0}= :{0}", parameters.args[0]);
                for (int i = 1; i < parameters.args.Length; i++)
                {
                    clause += string.Format(" AND {0}= :{0} ", parameters.args[i]);
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
            NpgsqlCommand msc = new NpgsqlCommand(sql, mc);

            if ((parameters.args != null && parameters.values != null) && parameters.args.Length > 0 && parameters.args.Length == parameters.values.Length)
            {
                for (int i = 0; i < parameters.args.Length; i++)
                {
                    msc.Parameters.AddWithValue(string.Format(":{0}", parameters.args[i]), parameters.values[i]);
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
                var adapter = new NpgsqlDataAdapter(msc);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception e)
            {
                throw new ArgumentException("PGSQL select exception", nameof(parameters), e);
            }
            finally
            {
              
                mc.Close();
            }

            
        }

        /// <summary>
        /// Get all the tables in the database with their info
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetTables()
        {
            string sql = "select n.nspname as table_schema, c.relname as table_name, c.reltuples as rows from pg_class c join pg_namespace n on n.oid = c.relnamespace where c.relkind = 'r' and n.nspname not in ('information_schema', 'pg_catalog') order by c.reltuples desc; ";

            NpgsqlConnection mc = new NpgsqlConnection(connection_string);

            NpgsqlCommand msc = new NpgsqlCommand(sql, mc);

            return GetCollection<TableInfo>(msc);
        }

        public List<ColumnInfo> GetColumns(string table_schema,string table_name)
        {
            string sql = "SELECT column_name,is_nullable,data_type FROM information_schema.columns WHERE table_schema = :table_schema AND table_name = :table_name; ";

            NpgsqlConnection mc = new NpgsqlConnection(connection_string);

            NpgsqlCommand cmd = new(sql, mc);
            cmd.Parameters.AddWithValue(":table_schema", table_schema);
            cmd.Parameters.AddWithValue(":table_name", table_name);

            return GetCollection<ColumnInfo>(cmd);
        }


        public  List<T> GetCollection<T>(NpgsqlCommand command)
        {
            
            try
            {
                var conn = new NpgsqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new NpgsqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return DataTableUtil.ConvertDataTable<T>(dt);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("PGSQL getcollection exception", nameof(command), ex);
               
            }

        }

        /// <summary>
        /// Run a query agains the database
        /// </summary>
        /// <param name="query">The query to be run against the database</param>
        /// <returns>int showing the outcome of the query</returns>
        public int RunQuery(string query)
        {
            int result = 0;
            using (var conn= new NpgsqlConnection(connection_string))
            {
                conn.Open();
                var cmd = new NpgsqlCommand(query, conn);
                result = cmd.ExecuteNonQuery();
            }
            //using var cmd = new NpgsqlCommand(query, new NpgsqlConnection(connection_string));
            //return cmd.ExecuteNonQuery();
            return result;
        }

        public void RunQuery(List<string> queries)
        {
            try
            {
                using (var con = new NpgsqlConnection(connection_string))
                {
                    foreach (var q in queries)
                    {
                        using var cmd = new NpgsqlCommand(q, con);
                        cmd.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException("PGSQL Query exception", queries.ToString(), ex);
            }

        }
    }
}
