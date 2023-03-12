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
    public class AuthRoleService : IAuthRoleService
    {
        string role_table = "_roles";
        string user_role_table = "_user_roles";
        string user_table = "_users";

        /// <summary>
        /// Add user to an existing role
        /// </summary>
        /// <param name="userRole">Role Id and the User Id to be linked</param>
        /// <param name="project">The project for the transaction</param>
        public void AddUserToRole(UserRole userRole, app_project project)
        {
            var process = new ProcessQuery(project);

            //check if the user is already in the database
            var parameters = new QueryParameters
            {
                args = new string[] { "user_id","role_id" },
                values = new object[] { userRole.user_id,userRole.role_id },
                table = project.code + user_role_table
            };

            var dt = process.Select(parameters);


            if (DataTableUtil.ConvertDataTable<UserRole>(dt).Any())
                throw new Exception("The user already belongs to the role");

            //add the user if they do not exist
            var insert_parameter = new PostParameters
            {
                fields = new string[] { "user_id", "role_id" },
                values = new object[] { userRole.user_id, userRole.role_id },
                table = project.code + user_role_table,

            };

            process.Insert(insert_parameter);
        }

        /// <summary>
        /// Remove the user from an existing role
        /// </summary>
        /// <param name="userRole">Role Id and the User Id to be unlinked</param>
        /// <param name="project">The project for the transaction</param>
        public void RemoveUserFromRole(UserRole userRole, app_project project)
        {
            var parameters = new DeleteParameters
            {
                args = new string[] { "user_id","role_id" },
                values = new string[] { userRole.user_id.ToString(),userRole.role_id.ToString() },
                table = project.code + "_roles"
            };
            new ProcessQuery(project).Delete(parameters);


        }

        /// <summary>
        /// Check if a user is in a role
        /// </summary>
        /// <param name="role_name"></param>
        /// <param name="user_id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool UserInRole(string role_name,Guid user_id,app_project project)
        {
            var process = new ProcessQuery(project);
            //get the role using the name of the role
            var parameters = new QueryParameters
            {
                args = new string[] { "role_name" },
                values = new string[] { role_name },
                table = project.code + role_table
            };

            var dt = process.Select(parameters);

            var role = DataTableUtil.ConvertDataTable<Role>(dt).FirstOrDefault();

            if (role == null)
            {
                throw new Exception("This role does not exist");
            }

            //check if the user exists

             parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new object[] { user_id },
                table = project.code + user_table
            };

             dt = process.Select(parameters);

            if (DataTableUtil.ConvertDataTable<User>(dt).FirstOrDefault() != null)
            {
                throw new Exception("The user does not exist");
            }

            parameters = new QueryParameters
            {
                args = new string[] { "user_id","role_id" },
                values = new object[] { user_id,role.id },
                table = project.code + user_role_table
            };

            if (process.Select(parameters) != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get all the roles that a user belongs to
        /// </summary>
        /// <param name="user_id"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetUserRoles(Guid user_id, app_project project)
        {
            var process = new ProcessQuery(project);

            var parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new object[] { user_id },
                table = project.code + user_table
            };

            var dt = process.Select(parameters);

            if (DataTableUtil.ConvertDataTable<User>(dt).FirstOrDefault() != null)
            {
                throw new Exception("The user does not exist");
            }

            //get all the roles assigned to the users
            parameters = new QueryParameters
            {
                args = new string[] { "user_id"},
                values = new object[] { user_id },
                table = project.code + user_role_table
            };

            dt = process.Select(parameters);

            var user_roles = DataTableUtil.ConvertDataTable<UserRole>(dt);

            parameters = new QueryParameters
            {
                table = project.code + "_roles"
            };

            dt = process.Select(parameters);

            if (dt == null)
                return null;


            var roles = DataTableUtil.ConvertDataTable<Role>(dt);

            return roles.Where(e => user_roles.Select(x => x.role_id).Contains(e.id));
        }


        /// <summary>
        /// Get all the roles for the project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public IEnumerable<Role> GetAll(app_project project)
        {
            var parameters = new QueryParameters
            {
                table = project.code + "_roles"
            };

            var dt = new ProcessQuery(project).Select(parameters);

            if (dt == null)
                return null;


            return DataTableUtil.ConvertDataTable<Role>(dt);
        }

        public Role GetById(Guid id,app_project project)
        {
            var parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new string[] { id.ToString() },
                table = project.code + "_roles"
            };

            var dt = new ProcessQuery(project).Select(parameters);

           return DataTableUtil.ConvertDataTable<Role>(dt).FirstOrDefault();
        }

        public Role Create(string role,app_project project)
        {
            ProcessQuery _processQuery = new ProcessQuery(project);
            // validation
            if (string.IsNullOrWhiteSpace(role))
                throw new Exception("Role is required");

            var parameters = new QueryParameters
            {
                args = new string[] { "role_name"},
                values = new string[] { role },
                table = project.code + "_roles"
            };

            var dt = _processQuery.Select(parameters);


            if (DataTableUtil.ConvertDataTable<Role>(dt).Any(x => x.role_name == role))
                throw new Exception("The role \"" + role + "\" is already created in the database");


            var insert_parameter = new PostParameters
            {
                fields = new string[] { "id", "role_name" },
                values = new object[] { Guid.NewGuid(), role },
                table = project.code + "_roles",
                
            };

            dt = _processQuery.Insert(insert_parameter);


            return DataTableUtil.ConvertDataTable<Role>(dt).FirstOrDefault();
        }

        public void Update(Role roleParam, app_project project)
        {
            var process_query = new ProcessQuery(project);
            var parameters = new QueryParameters
            {
                args = new string[] { "id" },
                values = new object[] { roleParam.id },
                table = project.code + "_roles"
            };

            var dt = process_query.Select(parameters);

            var role= DataTableUtil.ConvertDataTable<Role>(dt).FirstOrDefault();
            

            if (role == null)
                throw new Exception("User not found");

            // update role name if it has changed
            if (!string.IsNullOrWhiteSpace(roleParam.role_name) && roleParam.role_name != role.role_name)
            {
                // throw error if the role name already exists in the databas
                if (DataTableUtil.ConvertDataTable<User>(
                    process_query.Select(new QueryParameters {
                        table= project.code + "_roles",
                        args = new string[] { "role_name" },
                        values = new object[] { roleParam.role_name }
                    })).Any())
                {
                    throw new Exception("The role " + roleParam.role_name + " is created in the database");
                }
               

                role.role_name = roleParam.role_name;
            }

        
            var put_parameter = new PutParameters
            {
                toAlter = new string[] { "role_name" },
                alValues = new object[] { role.role_name },
                args = new string[] { "id" },
                values = new object[] { role.id },
                table = project.code + "_roles"
            };


            process_query.Update(put_parameter);
        }

        /// <summary>
        /// Delete a role from the system
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <param name="project">The project for the transaction</param>
        public void Delete(Guid id,app_project project)
        {
            var parameters = new DeleteParameters
            {
                args = new string[] { "id" },
                values = new string[] { id.ToString() },
                table = project.code + "_roles"
            };
            new ProcessQuery(project).Delete(parameters);


        }


    }
}
