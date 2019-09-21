using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using projectmanager.DAL;
using AutoMapper;

namespace projectmanager.Service.Repository
{
    public class EntityMapper<TSource, TDestination> where TSource : class where TDestination : class
    {
        public EntityMapper()
        {
            Mapper.CreateMap<Models.Projects,Projects>();
            Mapper.CreateMap<Projects, Models.Projects>();
        }
        public TDestination Translate(TSource obj)
        {
            return Mapper.Map<TDestination>(obj);
        }
    }
}