using System;
using System.Collections.Generic;
using System.Data;
using DbUtil.Entities;
using MySql.Data.MySqlClient;

namespace DbUtil.IService
{
    public interface IDbService
    {
        string GenerateConnectionJson(string host, string database, string username, string password, string port);
        DataTable InsertData(PostParameters parameters);
        int Update(PutParameters parameters);
        int Delete(DeleteParameters parameters);
        DataTable SelectData(QueryParameters parameters);
        List<TableInfo> GetTables();
        int RunQuery(string query);
        List<ColumnInfo> GetColumns(string table_schema, string table_name);
        void RunQuery(List<string> queries);
    }
}
