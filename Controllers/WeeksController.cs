﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
        public async Task<IActionResult> Index(int? id, int? dayId, int? taskId)
        {
            var vm = new WeekViewModel();
            vm.Week = await _context.Week.Include(w => w.Days)
                .ThenInclude(d => d.People)
                .ThenInclude(p => p.TaskItems)
                .ToListAsync();

            if (id != null)
            {
                ViewBag.WeekID = id.Value; // Set the ViewBag.WeekID property
                vm.Days = vm.Week.Where(w => w.Id == id.Value)
                    .Single().Days;
            }

            if (dayId != null)
            {
                ViewBag.DayId = dayId.Value;
                vm.Tasks = vm.Days.Where(d => d.DayId == dayId.Value)
                    .Single().TaskItems;
            }

            // Populate the Model.Days property with the days in the current week
            

            return View(vm);
        }









        // GET: Weeks/Details/5
        public async Task<IActionResult> Details(int? id)
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
                _context.Add(week);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
    }
}
