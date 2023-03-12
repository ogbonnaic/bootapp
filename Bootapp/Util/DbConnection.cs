using System;
using Bootapp.Data.Model;
using Newtonsoft.Json.Linq;

namespace Bootapp.Util
{
    public class DbConnection
    {

        public string GetConnectionString(app_project project)
        {
            string connection_string = "";

            var con_object = JObject.Parse(project.connection_string);
            switch (project.datasource.id)
            {
                //postgres
                case 1:
                   
                    connection_string= string.Format("User ID={0};Password={1};Host={2};Port={3};Database={4};", con_object["user_id"], con_object["password"], con_object["host"], con_object["port"], con_object["database"]);
                    break;
                //mysql
                case 2:
              
                    connection_string = string.Format("UiD={0};Pwd={1};Server={2};Port={3};Database={4};Pooling=true;", con_object["Uid"], con_object["Pwd"], con_object["Server"], con_object["Port"], con_object["Database"]);
                    break;
                //sqlserver
                case 3:
                    connection_string = string.Format("User ID={0};Password={1};Server={2},{3};Database={4};", con_object["user_id"], con_object["password"], con_object["server"], con_object["port"], con_object["database"]);
                    break;
                default:
                    break;
            }


            return connection_string;
        }

    }
}
