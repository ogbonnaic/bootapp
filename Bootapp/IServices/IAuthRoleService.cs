using System;
using System.Collections.Generic;
using Bootapp.Data.Entities;
using Bootapp.Data.Model;

namespace Bootapp.IServices
{
    public interface IAuthRoleService
    {
        void AddUserToRole(UserRole userRole, app_project project);
        void RemoveUserFromRole(UserRole userRole, app_project project);
        bool UserInRole(string role_name, Guid user_id, app_project project);

        IEnumerable<Role> GetAll(app_project project);
        Role GetById(Guid id, app_project project);
        Role Create(string role, app_project project);
        void Update(Role role, app_project project);
        void Delete(Guid id, app_project project);

        IEnumerable<Role> GetUserRoles(Guid user_id, app_project project);
    }
}
