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
    public class CourseCategoryController : Controller
    {
        private readonly OnlineEducationDBContext _context;

        public CourseCategoryController(OnlineEducationDBContext context)
        {
            _context = context;
        }

        // GET: CourseCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseCategories.ToListAsync());
        }

        // GET: CourseCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories
                .FirstOrDefaultAsync(m => m.CourseCategoryId == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // GET: CourseCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCategoryId,Title")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseCategory);
        }

        // GET: CourseCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories.FindAsync(id);
            if (courseCategory == null)
            {
                return NotFound();
            }
            return View(courseCategory);
        }

        // POST: CourseCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseCategoryId,Title")] CourseCategory courseCategory)
        {
            if (id != courseCategory.CourseCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseCategoryExists(courseCategory.CourseCategoryId))
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
            return View(courseCategory);
        }

        // GET: CourseCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseCategory = await _context.CourseCategories
                .FirstOrDefaultAsync(m => m.CourseCategoryId == id);
            if (courseCategory == null)
            {
                return NotFound();
            }

            return View(courseCategory);
        }

        // POST: CourseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseCategory = await _context.CourseCategories.FindAsync(id);
            _context.CourseCategories.Remove(courseCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseCategoryExists(int id)
        {
            return _context.CourseCategories.Any(e => e.CourseCategoryId == id);
        }
    }
}
