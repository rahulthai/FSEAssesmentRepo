using AutoMapper;
using projectmanager.DAL;
using projectmanager.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.MapperConfiguration
{
    public class AutoMapperWebApiProfile : Profile
    {
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a => { a.AddProfile<AutoMapperWebApiProfile>(); });
        }

        public AutoMapperWebApiProfile()
        {
            CreateMap<ProjectsModel, Projects>();
            CreateMap<Projects, ProjectsModel>();
        }
    }
}