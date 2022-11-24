using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Data;
using TaskDelegatingWebApp.Models;

namespace TaskDelegatingWebApp.Controllers
{
    public class WeeksController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public WeeksController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Day> DbDays = _context.Days.ToList();
            List<Person> DbPerson = _context.People.ToList();
            List<TaskItem> DbTaskItems = _context.TaskItems.ToList();

            Week tempWeek = new Week();

            foreach( Day day in DbDays)
            {
                tempWeek.DaysOfTheWeek.AddLast(day);
            }

            tempWeek.ListOfPeople = DbPerson;
            tempWeek.TaskQueue = DbTaskItems;

            ViewData["Days"] = tempWeek.DaysOfTheWeek;
            ViewData["People"] = tempWeek.ListOfPeople;
            ViewData["Tasks"] = tempWeek.TaskQueue;



            return View(tempWeek);
        }
    }
}