using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskDelegatingWebApp.Data;
using TaskDelegatingWebApp.Models;
using TaskDelegatingWebApp.ViewModels;

namespace TaskDelegatingWebApp.Controllers
{
    public class DaysController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public DaysController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: Days
        public async Task<IActionResult> Index(int? id, int? personId)
        {
            var viewModel = new WeekViewModel();
            viewModel.Days = await _context.Days.Include(e => e.People).Include(e => e.TaskItems).ToListAsync();

            if(id != null)
            {
                ViewData["DayId"] = id.Value;
                Day day = viewModel.Days.Where(e => e.DayId == id.Value).Single();

                viewModel.People = day.TaskItems.Select(e => e.Person);
            }

            if (personId != null)
            {
                Person person = viewModel.People.Where(e => e.PersonId == personId.Value).Single();
                viewModel.Items = viewModel.People.Where(e => e.PersonId == personId.Value).Single().TaskItems;
                viewModel.Items = viewModel.People.SelectMany(e => e.TaskItems);
            }

            return View(viewModel);
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Days == null)
            {
                return NotFound();
            }

            var day = await _context.Days
                .FirstOrDefaultAsync(m => m.DayId == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // GET: Days/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,DayName")] Day day)
        {
            if (ModelState.IsValid)
            {
                _context.Add(day);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(day);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Days == null)
            {
                return NotFound();
            }

            var day = await _context.Days.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DayId,DayName")] Day day)
        {
            if (id != day.DayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(day);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayExists(day.DayId))
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
            return View(day);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Days == null)
            {
                return NotFound();
            }

            var day = await _context.Days
                .FirstOrDefaultAsync(m => m.DayId == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Days == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.Days'  is null.");
            }
            var day = await _context.Days.FindAsync(id);
            if (day != null)
            {
                _context.Days.Remove(day);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayExists(int id)
        {
          return (_context.Days?.Any(e => e.DayId == id)).GetValueOrDefault();
        }
    }
}
