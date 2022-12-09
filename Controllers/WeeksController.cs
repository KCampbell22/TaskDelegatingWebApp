using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Data;
using TaskDelegatingWebApp.Models;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskDelegatingWebApp.Controllers
{
    public class WeeksController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;
        private List<PdfFile> _pdfFiles = new List<PdfFile>();


        public WeeksController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }


        
    // GET: Weeks
    public async Task<IActionResult> Index()
        {
            var Weeks = _context.Week.Include(e => e.Days).ThenInclude(e => e.TaskItems).ToList();
              return View(await _context.Week.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetMostRecentWeek()
        {



            // Query the Week table in the database
            var Weeks = await _context.Week.Include(e => e.Days).ThenInclude(e => e.TaskItems).OrderByDescending(e => e.WeekStart).ToListAsync();
            var week = Weeks.FirstOrDefault();
            if (week == null)
                {
                    return NotFound();
                }

            // Return the view, passing the week object as a parameter
            return RedirectToAction("Details", new { id = week.Id });


        }

        [HttpGet]
        public  IActionResult AddPeople()
        {
            var people =  _context.Person.ToList();
            return View(people);
        }


        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.TaskItem.ToList();





            return View(tasks);
        }

        // GET: Weeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Week == null)
            {
                return NotFound();
            }

            // Use the Include method to eager-load the Days and TaskItems entities
            var week = await _context.Week
              .Include(w => w.Days)
              .ThenInclude(e => e.TaskItems)
              .FirstOrDefaultAsync(m => m.Id == id);
            if (week == null)
            {
                return NotFound();
            }

            // Create a list of WeekViewModel objects
           

            // Pass the list of WeekViewModel objects to the view
            return View(week );
        }


        // GET: Weeks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Weeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeekStart,WeekEnd")] Week week)
        {
            if (ModelState.IsValid)
            {
                // Create a list of days in the week
                var days = new List<Day>
        {
            new Day { DayName = "Monday" },
            new Day { DayName = "Tuesday" },
            new Day { DayName = "Wednesday" },
            new Day { DayName = "Thursday" },
            new Day { DayName = "Friday" },
                        new Day { DayName = "Saturday" },
                                    new Day { DayName = "Sunday" }


        };

                // Add the days to the week object
                week.Days = days;

                // Save the week to the database
                _context.Add(week);
                await _context.SaveChangesAsync();

                return RedirectToAction("AddPeople");
            }
            return View(week);
        }





        // GET: Weeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Week == null)
            {
                return NotFound();
            }

            var week = await _context.Week.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }
            return View(week);
        }

        // POST: Weeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeekStart,WeekEnd")] Week week)
        {
            if (id != week.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(week);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeekExists(week.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(week);
        }

        // GET: Weeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Week == null)
            {
                return NotFound();
            }

            var week = await _context.Week
                .FirstOrDefaultAsync(m => m.Id == id);
            if (week == null)
            {
                return NotFound();
            }

            return View(week);
        }

        // POST: Weeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Week == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.Week'  is null.");
            }
            var week = await _context.Week.FindAsync(id);
            if (week != null)
            {
                _context.Week.Remove(week);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeekExists(int id)
        {
          return _context.Week.Any(e => e.Id == id);
        }

        public IActionResult GetTaskOptions()
        {
            TaskItem task = new TaskItem();
            return PartialView("_taskOptionMenu.cshtml", task);
        }


       

    }

  
}
