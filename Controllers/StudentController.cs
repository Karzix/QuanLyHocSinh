using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index(string? Name, string? Mssv, string? LopHoc)
        {
            var applicationDbContext = _context.Students.Include(s => s.LopHoc).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                applicationDbContext = applicationDbContext.Where(m => m.Name.Contains(Name));
                ViewBag.Name = Name;
            }
            if (!string.IsNullOrEmpty(Mssv))
            {
                applicationDbContext = applicationDbContext.Where(m => m.Mssv==Mssv);
                ViewBag.Mssv = Mssv;
            }
            if (!string.IsNullOrEmpty(LopHoc))
            {
                applicationDbContext = applicationDbContext.Where(m => m.LopHoc.MaLopHoc == LopHoc);
                ViewBag.LopHoc = LopHoc;
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.LopHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            ViewData["LopHocId"] = new SelectList(_context.LopHocs, "Id", "Name");
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Mssv,Name,LopHocId")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = Guid.NewGuid();
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LopHocId"] = new SelectList(_context.LopHocs, "Id", "Name", student.LopHocId);
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["LopHocId"] = new SelectList(_context.LopHocs, "Id", "Name", student.LopHocId);
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Mssv,Name,LopHocId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["LopHocId"] = new SelectList(_context.LopHocs, "Id", "Name", student.LopHocId);
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.LopHoc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(Guid id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
