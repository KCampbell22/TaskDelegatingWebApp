using System;
using AutoMapper;
using TaskDelegatingWebApp.Dtos;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.MapProfiles
{
    public class TaskItemProfile : Profile
    {
        public TaskItemProfile()
        {
            CreateMap<TaskItem, TaskItemDto>().ForMember(mem => mem.Day, o => o.MapFrom(e => e.Day))
                .ForMember(e => e.Person, o => o.MapFrom(e => e.Person))
                .ReverseMap();
        }
    }
}

