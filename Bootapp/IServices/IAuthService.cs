using System;
using System.Collections.Generic;
using Bootapp.Data.Entities;
using Bootapp.Data.Model;

namespace Bootapp.IServices
{
    public interface IAuthService
    {
        User Authenticate(string username, string password,app_project project);
        IEnumerable<User> GetAll(app_project project);
        User GetById(Guid id, app_project project);
        User Create(User user, string password, app_project project);
        void Update(User user, app_project project, string password = null);
        void Delete(Guid id, app_project project);
    }
}
