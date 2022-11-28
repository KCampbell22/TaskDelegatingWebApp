using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Text.Json;
using AutoMapper;
using TaskDelegatingWebApp.Dtos;

namespace TaskDelegatingWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleApiController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;
        private readonly IMapper _mapper;
        public PeopleApiController(TaskDelegatingWebAppContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/people
        public IEnumerable<PersonDto> GetPeople()
        {




            return _context.Person.Include(e => e.TaskItems).ToList().Select(_mapper.Map<Person, PersonDto>);

        }

        // Get /api/person/1
        [Route("/api/[controller]/{id}")]
        public PersonDto GetPerson(int id)
        {
            var person = _context.Person.Include(e => e.TaskItems).SingleOrDefault(c => c.PersonId == id);

            if (person == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return _mapper.Map<Person, PersonDto>(person);

        }


        // POST /api/person/1
        [Route("api/[controller]/CreatePerson")]
        [HttpPost]
        public Person CreatePerson(Person person)
        {
            if (!ModelState.IsValid)
            
                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            
            _context.Person.Add(person);
            _context.SaveChanges();
            return person;
        }


        // PUT /api/person/1

        [Route("api/[controller]/UpdatePerson={id}")]
        public void UpdatePerson(int id, Person person)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var personInDb = _context.Person.SingleOrDefault(c => c.PersonId == id);

            if (personInDb == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            

            personInDb.Name = person.Name;
            personInDb.Email = person.Email;
            personInDb.Saturday = person.Saturday;
            personInDb.Monday = person.Monday;
            personInDb.Tuesday= person.Tuesday;
            personInDb.Wednesday= person.Wednesday;
            personInDb.Thursday = person.Thursday;
            personInDb.Friday = person.Friday;
            personInDb.Sunday= person.Sunday;
            personInDb.TaskItems = person.TaskItems.Where(e => e.PersonId == id).ToList();
            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [Route("api/[controller]/Delete={id}")]
        [HttpDelete]
        public void DeletePerson(int id)
        {
            var personInDb = _context.Person.SingleOrDefault(c => c.PersonId == id);
            if (personInDb == null)
                throw new Exception (HttpStatusCode.NotFound.ToString());

            _context.Person.Remove(personInDb);
        }


    }
}
