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
    public class TaskItemsController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public TaskItemsController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: TaskItems
        public async Task<IActionResult> Index()
        {
            var taskDelegatingWebAppContext = _context.TaskItems.Include(t => t.Day).Include(t => t.Person);
            return View(await taskDelegatingWebAppContext.ToListAsync());
        }

        // GET: TaskItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskItems == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .Include(t => t.Day)
                .Include(t => t.Person)
                .FirstOrDefaultAsync(m => m.DayId == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // GET: TaskItems/Create
        public IActionResult Create()
        {
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId");
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email");
            return View();
        }

        // POST: TaskItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskItemId,TaskName,TaskDescription,TimeOfDay,DayId,PersonId")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskItem.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Email", taskItem.PersonId);
            return View(taskItem);
        }

        // GET: TaskItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskItems == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskItem.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Name", taskItem.PersonId);
            ViewData["TimeOfDay"] = new SelectList(_context.TaskItems, "TimeOfDay", "TimeOfDay", taskItem.TimeOfDay);
            return View(taskItem);
        }

        // POST: TaskItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskItemId,TaskName,TaskDescription,TimeOfDay,DayId,PersonId")] TaskItem taskItem)
        {
            if (id != taskItem.DayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.DayId))
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
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskItem.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "Name", taskItem.PersonId);
            ViewData["TimeOfDay"] = new SelectList(_context.TaskItems, "TimeOfDay", "TimeOfDay", taskItem.TimeOfDay);

            return View(taskItem);
        }

        // GET: TaskItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskItems == null)
            {
                return NotFound();
            }

            var taskItem = await _context.TaskItems
                .Include(t => t.Day)
                .Include(t => t.Person)
                .FirstOrDefaultAsync(m => m.DayId == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // POST: TaskItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskItems == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.TaskItems'  is null.");
            }
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskItemExists(int id)
        {
          return (_context.TaskItems?.Any(e => e.DayId == id)).GetValueOrDefault();
        }
    }
}
