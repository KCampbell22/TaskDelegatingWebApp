using System;
using AutoMapper;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Dtos;
namespace TaskDelegatingWebApp.MapProfiles
{
    public class WeekProfile : Profile
    {
        public WeekProfile()
        {
            CreateMap<Week, WeeksDto>().ForMember(e => e.Days, o => o.MapFrom(e => e.Days)).ReverseMap();
        }
    }
}

