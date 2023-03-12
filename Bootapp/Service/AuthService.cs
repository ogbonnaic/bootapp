using System;
using System.Collections.Generic;
using System.Linq;
using Bootapp.Data.Entities;
using Bootapp.Data.Model;
using Bootapp.IServices;
using Bootapp.Util;
using DbUtil;
using DbUtil.Entities;

namespace Bootapp.Service
{
    public class AuthService : IAuthService
    {
       

        public User Authenticate(string username, string password,app_project project)
        {
            ProcessQuery _processQuery = new(project);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;


            var parameters = new QueryParameters
            {
                args = new string[] { "username" },
                values = new string[] { username },
                table = project.code + "_users"
            };

            var dt = _processQuery.Select(parameters);

            if (dt == null)
                return null;


            var user = DataTableUtil.ConvertDataTable<User>(dt).FirstOrDefault();
            //var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.password_hash, user.password_salt))
                return null;

            // authentication successful
            return user;
        }

        public IEnumerable<User> GetAll(app_project project)
        {
            ProcessQuery _processQuery = new ProcessQuery(project);

            var parameters = new QueryParameters
            {
                table = project.code + "_users"
            };

            var dt = _processQuery.Select(parameters);

            if (dt == null)
                return null;


            return DataTableUtil.ConvertDataTable<User>(dt);
        }

        public User GetById(Guid id,app_project project)
        {
            ProcessQuery _processQuery = new ProcessQuery(project);

            var parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new string[] { id.ToString() },
                table = project.code + "_users"
            };

            var dt = _processQuery.Select(parameters);

           return DataTableUtil.ConvertDataTable<User>(dt).FirstOrDefault();
        }

        public User Create(User user, string password,app_project project)
        {
            ProcessQuery _processQuery = new ProcessQuery(project);
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            var parameters = new QueryParameters
            {
                args = new string[] { "username"},
                values = new string[] { user.username },
                table = project.code + "_users"
            };

            var dt = _processQuery.Select(parameters);


           


            if (DataTableUtil.ConvertDataTable<User>(dt).Any(x => x.username == user.username))
                throw new Exception("Username \"" + user.username + "\" is already taken");

            user.id = Guid.NewGuid();

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.password_hash = passwordHash;
            user.password_salt = passwordSalt;

            var insert_parameter = new PostParameters
            {
                fields = new string[] { "id", "username", "first_name", "last_name", "password_hash", "password_salt" },
                values = new object[] { user.id, user.username, user.first_name, user.last_name, user.password_hash, user.password_salt },
                table = project.code + "_users",
                
            };

            _processQuery.Insert(insert_parameter);


            return user;
        }

        public void Update(User userParam, app_project project, string password = null)
        {
            var process_query = new ProcessQuery(project);
            var parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new object[] { userParam.id },
                table = project.code + "_users"
            };

            var dt = process_query.Select(parameters);

            var user= DataTableUtil.ConvertDataTable<User>(dt).FirstOrDefault();
            

            if (user == null)
                throw new Exception("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(userParam.username) && userParam.username != user.username)
            {
                // throw error if the new username is already taken
                if (DataTableUtil.ConvertDataTable<User>(
                    process_query.Select(new QueryParameters {
                        table= project.code + "_users",
                        args = new string[] { "username" },
                        values = new object[] { userParam.username }
                    })).Any())
                {
                    throw new Exception("Username " + userParam.username + " is already taken");
                }
               

                user.username = userParam.username;
            }

            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.first_name))
                user.first_name = userParam.first_name;

            if (!string.IsNullOrWhiteSpace(userParam.last_name))
                user.last_name = userParam.last_name;

            // update password if provided
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.password_hash = passwordHash;
                user.password_hash = passwordSalt;
            }

            var put_parameter = new PutParameters
            {
                toAlter = new string[] { "username", "first_name", "last_name", "password_hash", "password_salt" },
                alValues = new object[] { user.username, user.first_name, user.last_name, user.password_hash, user.password_salt },
                args = new string[] { "id" },
                values = new object[] { user.id },
                table = project.code + "_users"
            };


            process_query.Update(put_parameter);
        }

        public void Delete(Guid id,app_project project)
        {
            ProcessQuery _processQuery = new ProcessQuery(project);

            var parameters = new DeleteParameters
            {
                args = new string[] { "id" },
                values = new string[] { id.ToString() },
                table = project.code + "_users"
            };
            _processQuery.Delete(parameters);


        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
