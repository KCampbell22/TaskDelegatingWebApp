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
            CreateMap<Day, DaysDto>().ForMember(member => member.People, o => o.MapFrom(src => src.People)).ForMember(member => member.TaskItems, o => o.MapFrom(src => src.TaskItems)).ForMember(member => member.Week, o => o.MapFrom(src => src.Week)).ReverseMap();
            

                
        }
    }
}

