using System;
using System.Collections.Generic;
using Bootapp.Data.Model;

namespace Bootapp.Util
{
    public class AuthUtil
    {
        public void CreateDb(app_project project)
        {
            List<string> queries;

            string query = "";

            switch (project.datasource.id)
            {
                //postgresql
                case 1:
                    //     queries = new List<string> {
                    //        string.Format("CREATE TABLE IF NOT EXISTS {0}_roles( id uuid, role_name text NOT NULL, PRIMARY KEY(id) );",project.code) ,
                    //string.Format("CREATE TABLE IF NOT EXISTS {0}_users( id uuid,first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15), email VARCHAR(100),password_hash bytea, password_salt bytea, PRIMARY KEY(id) );",project.code) ,
                    //string.Format("CREATE TABLE IF NOT EXISTS {0}_user_roles(user_id uuid, role_id uuid,  PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references users(id),  constraint fk_role foreign key(role_id) references roles(id) ); ",project.code)
                    //     };

                    query = string.Format(
                       "CREATE TABLE IF NOT EXISTS {0}_roles( id uuid, role_name text NOT NULL, PRIMARY KEY(id) ); " +
                       "CREATE TABLE IF NOT EXISTS {0}_users( id uuid,first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15),username text NOT NULL, email VARCHAR(100),password_hash bytea, password_salt bytea, PRIMARY KEY(id) ); " +
                       "CREATE TABLE IF NOT EXISTS {0}_user_roles(user_id uuid, role_id uuid,  PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references {0}_users(id),  constraint fk_role foreign key(role_id) references {0}_roles(id) ); ", project.code);
                    
                    break;

                //mysql
                case 2:

                    //    queries = new List<string> {
                    //        string.Format("CREATE TABLE IF NOT EXISTS {0}_roles(id varchar(36),role_name varchar(100) NOT NULL, constraint Pk_role PRIMARY KEY(id) );",project.code) ,
                    //string.Format("CREATE TABLE IF NOT EXISTS users( id varchar(36), first_name varchar(100) NOT NULL, last_name varchar(100) NOT NULL, phone_number VARCHAR(15), email VARCHAR(100),  password_hash varbinary(255), password_salt varbinary(255),  constraint pk_users PRIMARY KEY(id)  );",project.code) ,
                    //string.Format("CREATE TABLE IF NOT EXISTS user_roles(     user_id varchar(36),   role_id varchar(36),   constraint Pk_user_role PRIMARY KEY(user_id, role_id),    constraint fk_user foreign key(user_id) references users(id),    constraint fk_role foreign key(role_id) references roles(id) );", project.code)
                    //    };
                    query = 
                        string.Format("" +
                        "CREATE TABLE IF NOT EXISTS {0}_roles(id varchar(36),role_name varchar(100) NOT NULL, constraint Pk_role PRIMARY KEY(id) ); " +
                        "CREATE TABLE IF NOT EXISTS {0}_users( id varchar(36), first_name varchar(100) NOT NULL, last_name varchar(100) NOT NULL,email VARCHAR(100) NOT NULL, phone_number VARCHAR(15), username VARCHAR(100) NOT NULL,  password_hash varbinary(255), password_salt varbinary(255),  constraint pk_users PRIMARY KEY(id)  ); " +
                        "CREATE TABLE IF NOT EXISTS {0}_user_roles(     user_id varchar(36),   role_id varchar(36),   constraint Pk_user_role PRIMARY KEY(user_id, role_id),    constraint fk_user foreign key(user_id) references {0}_users(id),    constraint fk_role foreign key(role_id) references {0}_roles(id) );", project.code)
                    ;
                    break;

                //mssql
                case 3:
                    //queries = new List<string> {

                    //   string.Format("IF OBJECT_ID (N'[dbo].[{0}_roles]) IS NULL BEGIN CREATE TABLE [dbo].[{0}_roles] (id uniqueidentifier, role_name text NOT NULL, PRIMARY KEY(id)) END;",project.code),
                    //   string.Format("IF OBJECT_ID (N'[dbo].[{0}_users]) IS NULL BEGIN CREATE TABLE {0}_users(id uniqueidentifier, first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15), email VARCHAR(100), password_hash varbinary(255), password_salt varbinary(255), PRIMARY KEY(id)) END;",project.code),

                    //   string.Format("IF OBJECT_ID (N'[dbo].[{0}_user_roles]) IS NULL BEGIN CREATE TABLE {0}_user_roles(user_id uniqueidentifier, role_id uniqueidentifier, PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references users(id), constraint fk_role foreign key(role_id) references roles(id)) END;",project.code)
                    //};

                    query=

                       string.Format("" +
                       "CREATE TABLE {0}_roles (id uniqueidentifier, role_name text NOT NULL, PRIMARY KEY(id)) ; " +
                       "CREATE TABLE {0}_users(id uniqueidentifier, first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15), email VARCHAR(100), username VARCHAR(100) NOT NULL, password_hash varbinary(255), password_salt varbinary(255), PRIMARY KEY(id)) ; " +
                       "CREATE TABLE {0}_user_roles(user_id uniqueidentifier, role_id uniqueidentifier, PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references {0}_users(id), constraint fk_role foreign key(role_id) references {0}_roles(id)) ;", project.code)
                    ;

                    break;

                default:
                    queries = new List<string>();
                    break;

            }
           // var pg_sql = new List<string> {"CREATE TABLE roles( id uuid, role_name text NOT NULL, PRIMARY KEY(id) );" ,
           //     "CREATE TABLE users( id uuid,first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15), email VARCHAR(100),password_hash bytea, password_salt bytea, PRIMARY KEY(id) );" ,
           //     "CREATE TABLE user_roles(user_id uuid, role_id uuid,  PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references users(id),  constraint fk_role foreign key(role_id) references roles(id) ); " };

           // var my_sql = new List<string> { "CREATE TABLE roles(id varchar(36),role_name varchar(100) NOT NULL, constraint Pk_role PRIMARY KEY(id) );" ,
           //     " CREATE TABLE users( id varchar(36), first_name varchar(100) NOT NULL, last_name varchar(100) NOT NULL, phone_number VARCHAR(15), email VARCHAR(100),  password_hash varbinary(255), password_salt varbinary(255),  constraint pk_users PRIMARY KEY(id)  );" ,
           //     "   CREATE TABLE user_roles(     user_id varchar(36),   role_id varchar(36),   constraint Pk_user_role PRIMARY KEY(user_id, role_id),    constraint fk_user foreign key(user_id) references users(id),    constraint fk_role foreign key(role_id) references roles(id)    ); " };

           // var sql_server = new List<string> { "CREATE TABLE roles(id uniqueidentifier, role_name text NOT NULL, PRIMARY KEY(id));",
           //" CREATE TABLE users(id uniqueidentifier, first_name text NOT NULL, last_name text NOT NULL, phone_number VARCHAR(15), email VARCHAR(100), password_hash varbinary(255), password_salt varbinary(255), PRIMARY KEY(id));",

           //" CREATE TABLE user_roles(user_id uniqueidentifier, role_id uniqueidentifier, PRIMARY KEY(user_id, role_id), constraint fk_user foreign key(user_id) references users(id), constraint fk_role foreign key(role_id) references roles(id));"
           // };

            var process_query = new ProcessQuery(project);

            process_query.RunQuery(query);

        }
    }
}
