using AutoMapper;
using TaskDelegatingWebApp.Dtos;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.MapProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>().ForMember(m => m.TaskItems, o => o.MapFrom(e => e.TaskItems));

            CreateMap<TaskItem, TaskItemDto>().ForMember(dto =>
                dto.PersonId, opt => opt.MapFrom(src => src.Person));
            CreateMap<TaskItemDto, TaskItem>();

            CreateMap<PersonDto, Person>();


        }
    }
}
