using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeFisrtSampleProject.Data;
using CodeFisrtSampleProject.Entities;
using CodeFisrtSampleProject.ViewModel;

namespace CodeFisrtSampleProject.Controllers
{
    public class CourseClassController : Controller
    {
        private readonly OnlineEducationDBContext _context;

        public CourseClassController(OnlineEducationDBContext context)
        {
            _context = context;
        }

        // GET: CourseClass
        public async Task<IActionResult> Index()
        {
            var onlineEducationDBContext = _context.CourseClasses.Include(c => c.Course);
            return View(await onlineEducationDBContext.ToListAsync());
        }

        // GET: CourseClass/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        #region Create with MVC and JQuery
        // GET: CourseClass/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }

        // POST: CourseClass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseClassViewModel model) //[Bind("Id,Title,CourseId")] CourseClass courseClass
        {
            var courseClass = new CourseClass
            {
                Title = model.Title,
                CourseId = model.CourseId
            };
            courseClass.CourseClassDay = new List<CourseClassDay>();
            model.Days.ToList().ForEach(item =>
            {
                courseClass.CourseClassDay.Add(new CourseClassDay
                {
                    Day = (Days)item.DayId,
                    StartTime = item.StartTime,
                    FinishTime = item.FinishTime
                });
            });

            await _context.AddAsync(courseClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //if (ModelState.IsValid)
            //{
            //    _context.Add(courseClass);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", courseClass.CourseId);
            //return View(courseClass);
            // return View();
        }

        #endregion

        #region Create with AJAX
        public IActionResult CreateAjax()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title");
            ViewData["Provinces"] = new SelectList(_context.Province, "Id", "ProvinceName");

            return View();
        }

        // POST: CourseClass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAjax(CourseClassViewModel model) //[Bind("Id,Title,CourseId")] CourseClass courseClass
        {
            System.Threading.Thread.Sleep(4000);
            var courseClass = new CourseClass
            {
                Title = model.Title,
                CourseId = model.CourseId
            };
            courseClass.CourseClassDay = new List<CourseClassDay>();
            model.Days.ToList().ForEach(item =>
            {
                courseClass.CourseClassDay.Add(new CourseClassDay
                {
                    Day = (Days)item.DayId,
                    StartTime = item.StartTime,
                    FinishTime = item.FinishTime
                });
            });

            await _context.AddAsync(courseClass);
            await _context.SaveChangesAsync();

            var result = new AjaxActionResult<CourseClass>();
            result.IsSuccess = true;
            result.Message = "success...";
            return Ok(result);
        }

        #endregion
        // GET: CourseClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses.FindAsync(id);
            if (courseClass == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", courseClass.CourseId);
            return View(courseClass);
        }

        // POST: CourseClass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CourseId")] CourseClass courseClass)
        {
            if (id != courseClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseClassExists(courseClass.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Title", courseClass.CourseId);
            return View(courseClass);
        }

        // GET: CourseClass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseClass = await _context.CourseClasses
                .Include(c => c.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseClass == null)
            {
                return NotFound();
            }

            return View(courseClass);
        }

        // POST: CourseClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseClass = await _context.CourseClasses.FindAsync(id);
            _context.CourseClasses.Remove(courseClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseClassExists(int id)
        {
            return _context.CourseClasses.Any(e => e.Id == id);
        }
    }
}
