using System;
using System.Collections.Generic;
using System.Data;
using Bootapp.Data.Model;
using DbUtil.Entities;
using DbUtil.IService;
using DbUtil.Service;

namespace Bootapp.Util
{
    public class ProcessQuery
    {
        private readonly app_project _project;
        private readonly IDbService _service;
        public ProcessQuery(app_project project)
        {
            _project = project;
            var connection_string = new DbConnection().GetConnectionString(project);

            switch (_project.datasource.id)
            {
                //postgres
                case 1:
                    _service = new PostgreSQLService(connection_string);
                    break;
                //mysql
                case 2:
                    _service = new MySQLService(connection_string);
                    break;
                //sql server
                case 3:
                    _service = new SqlServerService(connection_string);
                    break;
                default:
                    break;
            }
        }

        public DataTable Select(QueryParameters parameter)
        {
           return _service.SelectData(parameter);
         
        }

        public DataTable Insert(PostParameters parameters)
        {
            return _service.InsertData(parameters);

        }


        public int Update(PutParameters parameters)
        {
            return _service.Update(parameters);

        }

        public int Delete(DeleteParameters parameters)
        {
            return _service.Delete(parameters);

        }


        public int RunQuery(string query)
        {
            return _service.RunQuery(query);


        }


        public void RunQuery(List<string> query)
        {
             _service.RunQuery(query);

        }

        //get all the tables in a database of the project
        public List<TableInfo> GetTables()
        {

            return _service.GetTables();

        }

        /// <summary>
        /// Get all the columns in a table of the project
        /// </summary>
        /// <param name="project">the project object</param>
        /// <param name="schema">the name of the schema</param>
        /// <param name="table">name of the table</param>
        /// <returns>a list of columninfo object</returns>
        public List<ColumnInfo> GetColumns(string schema,string table)
        {
          
            return _service.GetColumns(schema, table); ;

        }
    }
}
