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
    public class TaskAssignmentsController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public TaskAssignmentsController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: TaskAssignments
        public async Task<IActionResult> Index()
        {
            var AssignedTasks = _context.TaskAssignments.Include(t => t.Day).Include(t => t.Person).Include(t => t.TaskItem);
            return View(await AssignedTasks.ToListAsync());
        }

        // GET: TaskAssignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskAssignments == null)
            {
                return NotFound();
            }

            var taskAssignment = await _context.TaskAssignments
                .Include(t => t.Day)
                .Include(t => t.Person)
                .Include(t => t.TaskItem)
                .FirstOrDefaultAsync(m => m.TaskItemId == id);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            return View(taskAssignment);
        }

        // GET: TaskAssignments/Create
        public IActionResult Create()
        {
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId");
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
            ViewData["TaskItemId"] = new SelectList(_context.TaskItems, "TaskItemId", "TaskItemId");
            return View();
        }

        // POST: TaskAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,TaskItemId,DayId")] TaskAssignment taskAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskAssignment.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", taskAssignment.PersonId);
            ViewData["TaskItemId"] = new SelectList(_context.TaskItems, "TaskItemId", "TaskItemId", taskAssignment.TaskItemId);
            return View(taskAssignment);
        }

        // GET: TaskAssignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskAssignments == null)
            {
                return NotFound();
            }

            var taskAssignment = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignment == null)
            {
                return NotFound();
            }
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskAssignment.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", taskAssignment.PersonId);
            ViewData["TaskItemId"] = new SelectList(_context.TaskItems, "TaskItemId", "TaskItemId", taskAssignment.TaskItemId);
            return View(taskAssignment);
        }

        // POST: TaskAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,TaskItemId,DayId")] TaskAssignment taskAssignment)
        {
            if (id != taskAssignment.TaskItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskAssignmentExists(taskAssignment.TaskItemId))
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
            ViewData["DayId"] = new SelectList(_context.Days, "DayId", "DayId", taskAssignment.DayId);
            ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId", taskAssignment.PersonId);
            ViewData["TaskItemId"] = new SelectList(_context.TaskItems, "TaskItemId", "TaskItemId", taskAssignment.TaskItemId);
            return View(taskAssignment);
        }

        // GET: TaskAssignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskAssignments == null)
            {
                return NotFound();
            }

            var taskAssignment = await _context.TaskAssignments
                .Include(t => t.Day)
                .Include(t => t.Person)
                .Include(t => t.TaskItem)
                .FirstOrDefaultAsync(m => m.TaskItemId == id);
            if (taskAssignment == null)
            {
                return NotFound();
            }

            return View(taskAssignment);
        }

        // POST: TaskAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskAssignments == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.TaskAssignments'  is null.");
            }
            var taskAssignment = await _context.TaskAssignments.FindAsync(id);
            if (taskAssignment != null)
            {
                _context.TaskAssignments.Remove(taskAssignment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskAssignmentExists(int id)
        {
          return (_context.TaskAssignments?.Any(e => e.TaskItemId == id)).GetValueOrDefault();
        }
    }
}
