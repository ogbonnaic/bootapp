using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bootapp.Util;
using DbUtil.IService;
using DbUtil.Service;
using Newtonsoft.Json.Linq;

namespace Bootapp.Data.ViewModel
{
    public class Project
    {
        private IDbService _service;
        private string _connection_string;
        private JObject _con_object;
        
        public Guid id { set; get; }
        [Required]
        public string name { set; get; }
        [Required]
        [MaxLength(10)]
        public string code { set; get; }
        public string created_by { set; get; }
        [Required]
        [Column(TypeName = "json")]
        public string connection_string
        {
            set {
                _connection_string = value;
                //postgresql
                if (datasourceid == 1)
                {
                    _con_object = JObject.Parse(_connection_string);
                    port = JsonUtil.GetString(_con_object, "port");
                    host = JsonUtil.GetString(_con_object, "host");
                    user_id = JsonUtil.GetString(_con_object, "user_id");
                    password = JsonUtil.GetString(_con_object, "password");
                }
                //mysql
                else if (datasourceid == 2)
                {
                    _con_object = JObject.Parse(_connection_string);
                    port = JsonUtil.GetString(_con_object, "Port");
                    host = JsonUtil.GetString(_con_object, "Server");
                    user_id = JsonUtil.GetString(_con_object, "Uid");
                    password = JsonUtil.GetString(_con_object, "Pwd");
                }
                //sql server
                else if (datasourceid == 3)
                {
                    _con_object = JObject.Parse(_connection_string);
                    port = JsonUtil.GetString(_con_object, "port");
                    host = JsonUtil.GetString(_con_object, "server");
                    user_id = JsonUtil.GetString(_con_object, "user_id");
                    password = JsonUtil.GetString(_con_object, "password");
                }

            }
            get
            {
                //postgres
                if (datasourceid == 1)
                {
                    _service = new PostgreSQLService("");
                }
                //mysql
                else if (datasourceid ==2)
                {
                    _service = new MySQLService("");
                }
                //sql server
                else if (datasourceid == 3)
                {
                    _service = new SqlServerService("");
                }
                return _service.GenerateConnectionJson(host, database, user_id, password, port);
            } }
    
        public DateTime created_at { set; get; }
        public string updated_by { set; get; }
        public DateTime? updated_at { set; get; }
        public int active { set; get; }
        public string host { set; get; }
        public string port { set; get; }
        public string user_id { set; get; }
        public string password { set; get; }
        public string database { set; get; }
        public int datasourceid { set; get; }



    }
}
