using System;
using Bootapp.Data.Entities;
using Bootapp.Data.Model;
using Bootapp.Data.ViewModel;
using AutoMapper;

namespace Bootapp.Helpers
{
    public class CustomMapperProfile : Profile
    {
        public CustomMapperProfile()
        {
            CreateMap<Project, app_project>();
            CreateMap<app_project,Project>();

            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateUserModel, User>();
        }
    }
}
