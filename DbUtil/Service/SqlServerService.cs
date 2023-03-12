using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbUtil;
using DbUtil.Entities;
using DbUtil.IService;
using Newtonsoft.Json.Linq;

namespace DbUtil.Service
{
    public class SqlServerService: IDbService
    {
        private readonly string connection_string;
        public SqlServerService(string connectionstring)
        {
            connection_string = connectionstring;
        }

        /// <summary>
        /// Generate the connection string from the datasource object
        /// </summary>
        /// <param name="datasource">The datasource object</param>
        /// <returns></returns>
        public string GenerateConnectionJson(string host, string database, string username, string password, string port)
        {
            var job = new JObject
            {
                { "user_id", username },
                { "password", password },
                { "server", host },
                { "port", port },
                { "database", database }
            };

            return job.ToString();
            //return string.Format("Server={0};Database={1};User Id={2};Password = {3}; ", host, database, username, password);
        }

        /// <summary>
        /// Generate the connection string from the datasource object
        /// </summary>
        /// <param name="datasource">The datasource object</param>
        /// <returns></returns>
        public string GenerateConnectionString(string host,string database,string username,string password)
        {
            //return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;user id={2};password={3};MultipleActiveResultSets=True",datasource.host,datasource.database,datasource.username,datasource.password);

            return string.Format("Server={0};Database={1};User Id={2};Password = {3}; ", host, database, username, password);
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
            query += ") OUTPUT INSERTED.* VALUES(";
            string sql = query + part2 + ")";



            SqlConnection mc = new SqlConnection(connection_string);
            //string sql = genInsertQuery(table, fields, values);
            SqlCommand msc = new SqlCommand(sql, mc);

            for (int i = 0; i < fieldVals.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", fieldVals[i]), valData[i]);
            }


            DataTable dt = null;



            mc.Open();
            //int retVal = -1;
            try
            {
                dt = new DataTable();
                var adapter = new SqlDataAdapter(msc);
                adapter.Fill(dt);
               

                //if (retVal > -1)
                //{
                //    dt = new DataTable();
                //    var adapter = new SqlDataAdapter(msc);
                //    adapter.Fill(dt);
                //}


            }
            catch (SqlException ex)
            {
                throw new ArgumentException("SQL insert exception", nameof(parameters), ex);
            }
            finally
            {
                mc.Close();
            }
            return dt;
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
            SqlConnection mc = new SqlConnection(connection_string);
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
            SqlCommand msc = new SqlCommand(sql, mc);

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
            catch (Exception ex) {
                throw new ArgumentException("MSSQL update exception", nameof(parameters), ex);
            }
            finally { mc.Close(); }
            return result;
        }


        public int Delete(DeleteParameters parameters)
        {
            int result = -1;
            SqlConnection mc = new SqlConnection(connection_string);
            string clause = string.Format("WHERE {0}= @{0}", parameters.args[0]);

            for (int i = 1; i < parameters.args.Length; i++)
            {
                clause += string.Format(" AND {0}= @{0}", parameters.args[i]);
            }
            string sql = string.Format("DELETE FROM {0} {1}", parameters.table, clause);
            SqlCommand msc = new SqlCommand(sql, mc);


            for (int i = 0; i < parameters.args.Length; i++)
            {
                msc.Parameters.AddWithValue(string.Format("@{0}", parameters.args[i]), parameters.values[i]);
            }
            mc.Open();
            try
            {
                result = msc.ExecuteNonQuery();
            }
            catch (Exception ex) {
                throw new ArgumentException("MSSQL delete exception", nameof(parameters), ex);
            }
            finally { mc.Close(); }
            return result;
        }

        public DataTable SelectData(QueryParameters parameters)
        {
            SqlConnection mc = new SqlConnection(connection_string);

            string clause = "";
            string append = "";


            if ((parameters.args != null && parameters.values != null) && parameters.args.Length > 0 && parameters.args.Length == parameters.values.Length)
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
                    for (var i = 0; i < parameters.orderByDesc.Length; i++)
                    {
                        orderByDesc[i] = parameters.orderByDesc[i] + " DESC";
                    }

                    if (parameters.orderBy == null)
                    {
                        append += string.Join(',', orderByDesc);
                    }
                    else
                    {
                        append += " ," + string.Join(',', orderByDesc);
                    }

                }
            }

            //check if there is pagination
            if (parameters.PageNumber > 0)
            {
                append += " LIMIT " + parameters.PageSize + " OFFSET " + ((parameters.PageNumber - 1) * parameters.PageSize);
            }


            string sql = string.Format("SELECT {0} FROM {1} {2}", fieldvalues, parameters.table, clause) + append;
            SqlCommand msc = new SqlCommand(sql, mc);

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
                var adapter = new SqlDataAdapter(msc);
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MSSQL select exception", nameof(parameters), ex);
            }
            finally
            {

                mc.Close();
            }

            
        }

        /// <summary>
        /// Get a list of all the databases from an SQL Server
        /// </summary>
        /// <param name="connString">The connection string of the server</param>
        /// <returns></returns>
        public List<string> GetDatabases(string connString)
        {
            var list = new List<string>();
            using (var con = new SqlConnection(connString))
            {
                con.Open();

                // Set up a command with the given query and associate
                // this with the current connection.
                using (var cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(dr[0].ToString());
                        }
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// Get a list of the columns in a database table
        /// </summary>
        /// <param name="table_schema">The name of the schema</param>
        /// <param name="table_name">The table whose columns are to be retrieved</param>
        /// <returns></returns>
        public List<ColumnInfo> GetColumns(string table_schema, string table_name)
        {
            string sql = "SELECT column_name as 'column_name',is_nullable as 'is_nullable',data_type as 'data_type' FROM information_schema.columns WHERE table_schema = @table_schema AND table_name = @table_name; ";

            SqlConnection mc = new SqlConnection(connection_string);

            SqlCommand cmd = new(sql, mc);
            cmd.Parameters.AddWithValue("@table_schema", table_schema);
            cmd.Parameters.AddWithValue("@table_name", table_name);

            return GetCollection<ColumnInfo>(cmd);
        }

        /// <summary>
        /// Get alll the tables in a database
        /// </summary>
        /// <returns></returns>
        public List<TableInfo> GetTables()
        {

            string sql = "select TABLE_SCHEMA as 'table_schema', TABLE_NAME as 'table_name', 0 as 'rows'  from INFORMATION_SCHEMA.TABLES ";

            SqlConnection mc = new(connection_string);

            SqlCommand msc = new(sql, mc);

            return GetCollection<TableInfo>(msc);
        }


        public List<T> GetCollection<T>(SqlCommand command)
        {

            try
            {
                var conn = new SqlConnection(connection_string);
                command.Connection = conn;
                var dataAdapter = new SqlDataAdapter(command);
                var dt = new DataTable();
                dataAdapter.Fill(dt);

                return DataTableUtil.ConvertDataTable<T>(dt);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("MSSQL getcollection exception", nameof(command), ex);
            }

        }

        /// <summary>
        /// Get the first 10 rows of the data in the table
        /// </summary>
        /// <param name="connString">Connectionstring</param>
        /// <param name="tableName">Name of the table</param>
        /// <returns></returns>
        public DataTable GetTablePreview(string connString, string tableName)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connString))
            {
                con.Open();
                //get the first 10 rows from the table
                using (var cmd = new SqlCommand("SELECT TOP 10 * FROM " + tableName, con))
                {

                    var adapter = new SqlDataAdapter(cmd);
                   
                    adapter.Fill(dt);
                    
                }
            }

            return dt;
        }

        /// <summary>
        /// Get all the data from a selected table
        /// </summary>
        /// <param name="connString">Connection string</param>
        /// <param name="tableName">Name of the table</param>
        /// <returns></returns>
        public DataTable GetTableData(string connString, string tableName)
        {
            var dt = new DataTable();
            using (var con = new SqlConnection(connString))
            {
                con.Open();
                //get the first 10 rows fromt the table
                using (var cmd = new SqlCommand("SELECT * FROM " + tableName, con))
                {

                    var adapter = new SqlDataAdapter(cmd);

                    adapter.Fill(dt);

                }
                con.Close();
            }

            return dt;
        }


        /// <summary>
        /// Run a query agains the database
        /// </summary>
        /// <param name="query">The query to be run against the database</param>
        /// <returns>int showing the outcome of the query</returns>
        public int RunQuery(string query)
        {
            var conn = new SqlConnection(connection_string);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(query, conn);
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
                using (var con = new SqlConnection(connection_string))
                {
                    foreach (var q in queries)
                    {
                        using var cmd = new SqlCommand(q, con);
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
