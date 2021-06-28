using AutoMapper;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Factory, FactoryDto>();
            CreateMap<FactoryCreateUpdateDto, Factory>();
            CreateMap<Factory, FactoryWithUnitsDto>();

            CreateMap<Unit, UnitDto>();
            CreateMap<Unit, UnitWithTanksDto>();
            CreateMap<Unit, UnitCreateUpdateDto>();

            CreateMap<Tank, TankDto>();
            CreateMap<TankCreateUpdateDto, Tank>();

            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();

            CreateMap<Event, EventDto>();
            CreateMap<EventDto, Event>();

        }

    }
}
