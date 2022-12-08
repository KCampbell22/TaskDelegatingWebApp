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
    public class DaysController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public DaysController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: Days
        public async Task<IActionResult> Index()
        {
            var taskDelegatingWebAppContext = _context.Day.Include(d => d.Week).Include(e => e.TaskItems);
            return View(await taskDelegatingWebAppContext.ToListAsync());
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Day == null)
            {
                return NotFound();
            }

            var day = await _context.Day
                .Include(d => d.Week)
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
            ViewData["WeekId"] = new SelectList(_context.Week, "Id", "Id");
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DayId,DayName,WeekId")] Day day)
        {
            if (ModelState.IsValid)
            {
                _context.Add(day);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WeekId"] = new SelectList(_context.Week, "Id", "Id", day.WeekId);
            return View(day);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Day == null)
            {
                return NotFound();
            }

            var day = await _context.Day.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }
            ViewData["WeekId"] = new SelectList(_context.Week, "Id", "Id", day.WeekId);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DayId,DayName,WeekId")] Day day)
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
            ViewData["WeekId"] = new SelectList(_context.Week, "Id", "Id", day.WeekId);
            return View(day);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Day == null)
            {
                return NotFound();
            }

            var day = await _context.Day
                .Include(d => d.Week)
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
            if (_context.Day == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.Day'  is null.");
            }
            var day = await _context.Day.FindAsync(id);
            if (day != null)
            {
                _context.Day.Remove(day);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayExists(int id)
        {
          return _context.Day.Any(e => e.DayId == id);
        }

        
    }
}
