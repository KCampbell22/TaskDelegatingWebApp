using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Data;
using TaskDelegatingWebApp.Dtos;

namespace TaskDelegatingWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private readonly TaskDelegatingWebAppContext _context;
        private readonly IMapper _mapper;

        public WeeksController(TaskDelegatingWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET /api/weeks
        public IEnumerable<WeeksDto> GetWeeks()
        {
             



            return _context.Week.Include(e => e.Days).ThenInclude(e => e.TaskItems).ToList().Select(_mapper.Map<Week, WeeksDto>);

        }

        // Get /api/weeks/1
        [Route("/api/[controller]/{id}")]
        public WeeksDto GetWeek(int id)
        {
            var week = _context.Week.Include(e => e.Days).SingleOrDefault(c => c.Id == id);

            if (week == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return _mapper.Map<Week, WeeksDto>(week);

        }


        // POST /api/task/1
        [Route("api/[controller]/CreateTask")]
        [HttpPost]
        public WeeksDto CreateWeek(WeeksDto weekdto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());

            var week = _mapper.Map<WeeksDto, Week>(weekdto);
            _context.Week.Add(week);
            _context.SaveChanges();

            weekdto.Id = week.Id;
            return weekdto;
        }


        // PUT /api/weeks/1

        [Route("api/[controller]/UpdateWeek={id}")]
        public void UpdateWeek(int id, WeeksDto weekdto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var weekInDb = _context.Week.SingleOrDefault(c => c.Id == id);

            if (weekInDb == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }

            _mapper.Map(weekdto, weekInDb);



            _context.SaveChanges();
        }

        // DELETE /api/weeks/1
        [Route("api/[controller]/Delete={id}")]
        [HttpDelete]
        public void DetleteWeek(int id)
        {
            var weekInDb = _context.Week.SingleOrDefault(c => c.Id == id);
            if (weekInDb == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _context.Week.Remove(weekInDb);
        }
    }
}
