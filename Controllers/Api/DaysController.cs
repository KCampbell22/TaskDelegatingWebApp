using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.Dtos;
using TaskDelegatingWebApp.Data;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace TaskDelegatingWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {

        private readonly TaskDelegatingWebAppContext _context;
        private readonly IMapper _mapper;

        public DaysController(TaskDelegatingWebAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DaysDto> GetWeeks()
        {




            return _context.Day.Include(e => e.Week)
                .Include(e => e.TaskItems.Select(c => c.Day))
                .Include(e => e.People)
                .ToList()
                .Select(_mapper.Map<Day, DaysDto>);

        }

        // Get /api/Day/1
        [Route("/api/[controller]/{id}")]
        public DaysDto GetWeek(int id)
        {
            var day = _context.Day.Include(e => e.Week).Include(e => e.TaskItems).Include(e => e.People).SingleOrDefault(c => c.DayId == id);

            if (day == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }
            return _mapper.Map<Day, DaysDto>(day);

        }


        // POST /api/days/1
        [Route("api/[controller]/CreateDay")]
        [HttpPost]
        public DaysDto CreateWeek(DaysDto daydto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var day = _mapper.Map<DaysDto, Day>(daydto);
            _context.Day.Add(day);
            _context.SaveChanges();
            daydto.DayId = day.DayId;
            return daydto;
        }


        // PUT /api/days/1

        [Route("api/[controller]/UpdateWeek/{id}")]
        public void UpdateWeek(int id, DaysDto dayDto)
        {
            if (!ModelState.IsValid)

                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var dayInDb = _context.Day.SingleOrDefault(c => c.DayId == id);

            if (dayInDb == null)
            {
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());
            }

            _mapper.Map(dayDto, dayInDb);
            
            _context.SaveChanges();
        }

        // DELETE /api/days/1
        [Route("api/[controller]/Delete/{id}")]
        [HttpDelete]
        public void DeleteWeek(int id)
        {
            var dayindb = _context.Day.SingleOrDefault(c => c.DayId == id);
            if (dayindb == null)
                throw new Exception(HttpStatusCode.NotFound.ToString());

            _context.Day.Remove(dayindb);
        }

    }
}
