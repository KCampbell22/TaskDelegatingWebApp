using System;
using AutoMapper;
using TaskDelegatingWebApp.Dtos;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.MapProfiles
{
    public class DayProfile : Profile
    {
        public DayProfile()
        {
            CreateMap<Day, DaysDto>().ForMember(member => member.People, o => o.MapFrom(e => e.People)).ForMember(member => member.Week, o => o.MapFrom(e => e.Week)).ReverseMap();
        }
    }
}

