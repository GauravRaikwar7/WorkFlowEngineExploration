﻿using AutoMapper;
using System;

namespace WF.Sample.MsSql
{
    internal static class Mappings
    {
        public static IMapper Mapper { get { return _mapper.Value; } }

        private static Lazy<IMapper> _mapper = new Lazy<IMapper>(GetMapper);

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<StructDivision, Business.Model.StructDivision>();
                cfg.CreateMap<Employee, Business.Model.Employee>();
                cfg.CreateMap<Role, Business.Model.Role>();
                cfg.CreateMap<EmployeeRole, Business.Model.EmployeeRole>();

                cfg.CreateMap<Document, Business.Model.Document>();
                cfg.CreateMap<TravelRequest, Business.Model.TravelRequest>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        }
    }
}
