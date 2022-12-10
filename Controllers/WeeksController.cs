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
    public class WeeksController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;


        public WeeksController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: Weeks
        public async Task<IActionResult> Index()
        {
              return View(await _context.Week.ToListAsync());
        }

        // GET: Weeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var week = await _context.Week
                .Include(w => w.Days)
                    .ThenInclude(d => d.TaskItems)
                        .ThenInclude(t => t.AssignedPerson)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (week == null)
            {
                return NotFound();
            }

            var people = await _context.Person
                .Where(p => p.Days.Any(d => d.WeekId == id))
                .ToListAsync();

            var taskItems = week.Days.SelectMany(d => d.TaskItems).ToList();
            var viewModel = new WeekVIewModel(week, taskItems, people);

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: temp/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeekName")] Week week)
        {
            if (ModelState.IsValid)
            {

                _context.Add(week);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Days", new { id = week.Id });
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeekName")] Week week)
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
    }
}
