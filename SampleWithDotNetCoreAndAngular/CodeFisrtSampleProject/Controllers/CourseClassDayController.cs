using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFisrtSampleProject.Data;
using CodeFisrtSampleProject.Entities;

namespace CodeFisrtSampleProject.Controllers
{
    public class CourseClassDayController : Controller
    {
        private readonly OnlineEducationDBContext _context;

        public CourseClassDayController(OnlineEducationDBContext context)
        {
            _context = context;
        }

        // GET: CourseClassDay
        public async Task<IActionResult> Index()
        {
            var onlineEducationDBContext = _context.CourseClassDays.Include(c => c.CourseClass);
            return View(await onlineEducationDBContext.ToListAsync());
        }

        // GET: CourseClassDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClassDay = await _context.CourseClassDays
                .Include(c => c.CourseClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClassDay == null)
            {
                return NotFound();
            }

            return View(courseClassDay);
        }

        // GET: CourseClassDay/Create
        public IActionResult Create()
        {
            ViewData["CourseClassId"] = new SelectList(_context.CourseClasses, "Id", "Id");
            return View();
        }

        // POST: CourseClassDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day,StartTime,FinishTime,CourseClassId")] CourseClassDay courseClassDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseClassDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseClassId"] = new SelectList(_context.CourseClasses, "Id", "Id", courseClassDay.CourseClassId);
            return View(courseClassDay);
        }

        // GET: CourseClassDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClassDay = await _context.CourseClassDays.FindAsync(id);
            if (courseClassDay == null)
            {
                return NotFound();
            }
            ViewData["CourseClassId"] = new SelectList(_context.CourseClasses, "Id", "Id", courseClassDay.CourseClassId);
            return View(courseClassDay);
        }

        // POST: CourseClassDay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day,StartTime,FinishTime,CourseClassId")] CourseClassDay courseClassDay)
        {
            if (id != courseClassDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseClassDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseClassDayExists(courseClassDay.Id))
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
            ViewData["CourseClassId"] = new SelectList(_context.CourseClasses, "Id", "Id", courseClassDay.CourseClassId);
            return View(courseClassDay);
        }

        // GET: CourseClassDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClassDay = await _context.CourseClassDays
                .Include(c => c.CourseClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClassDay == null)
            {
                return NotFound();
            }

            return View(courseClassDay);
        }

        // POST: CourseClassDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseClassDay = await _context.CourseClassDays.FindAsync(id);
            _context.CourseClassDays.Remove(courseClassDay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseClassDayExists(int id)
        {
            return _context.CourseClassDays.Any(e => e.Id == id);
        }
    }
}
