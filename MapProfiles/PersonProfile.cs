using AutoMapper;
using TaskDelegatingWebApp.Dtos;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.MapProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonDto>().ForMember(m => m.TaskItems, o => o.MapFrom(e => e.TaskItems.Select(e => e.PersonId)));
            CreateMap<PersonDto, Person>();
        }
    }
}
