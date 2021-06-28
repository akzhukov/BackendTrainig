using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebSocketsService.Hubs;
using SocketsShared.DTOs;
using DataProviders.Models;

namespace WebSocketsService.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();

            CreateMap<BaseHub, HubDto>();

            CreateMap<SimpleChart, SimpleChartAdminDto>();
            CreateMap<SimpleChart, SimpleChartOthersDto>();

        }

    }
}
