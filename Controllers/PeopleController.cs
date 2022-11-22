﻿using System;
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
    public class PeopleController : Controller
    {
        private readonly TaskDelegatingWebAppContext _context;

        public PeopleController(TaskDelegatingWebAppContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(int? id, int? taskItemId, int? dayId)
        {
            var viewModel = new PersonViewModel();
            viewModel.People = await _context.People.Include(e => e.TaskAssignments)
                .ThenInclude(e => e.TaskItem)
                .ThenInclude(e => e.Day)
                .AsNoTracking()
                .OrderBy(e => e.Name).ToListAsync();


            if (id != null)
            {
                ViewData["PersonId"] = id.Value;
                Person person = viewModel.People.Where(e => e.PersonId == id.Value).Single();
                viewModel.TaskItems = person.TaskAssignments.Select(e => e.TaskItem);

            }
            if (taskItemId != null)
            {
                ViewData["TaskItemId"] = taskItemId.Value;
                viewModel.TaskAssignments = viewModel.TaskItems.Where(e => e.TaskItemId == taskItemId.Value).Single().TaskAssignments;
                ViewData["TaskCount"] = viewModel.TaskItems.Where(e => e.TaskItemId == taskItemId.Value).ToArray().Count();

            }





            return View(viewModel);
        }


        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,Name,Email,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Name,Email,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.People == null)
            {
                return Problem("Entity set 'TaskDelegatingWebAppContext.People'  is null.");
            }
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
          return (_context.People?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
