using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospitals.Data;
using Hospitals.Models;

namespace Hospitals.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HospitalDataContext _context;
        private List<string> filteri = new List<string>();

        public ReviewController(HospitalDataContext context)
        {
            _context = context;

            foreach(var item in _context.Reviews)
            {
                filteri.Add(item.Department);
            }
        }

        // GET: Review
        public async Task<IActionResult> Index(string searchString, string filterApplied)
        {
          IEnumerable<Review> hospitalDataContext = await _context.Reviews.Include(r => r.Hospital).AsNoTracking().ToListAsync();

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                hospitalDataContext = hospitalDataContext.Where(p => p.Speciality.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrWhiteSpace(filterApplied))
            {
                hospitalDataContext = hospitalDataContext.Where(p => p.Department.ToUpper() == filterApplied.ToUpper());

            }

            ViewBag.filters = new SelectList(filteri);

            return View(hospitalDataContext);
        }

        // GET: Review/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Hospital).Include(p => p.Comments).AsNoTracking()
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Review/Create
        public IActionResult Create(int HospitalID)
        {
            ViewData["HospitalID"] = HospitalID;
            ViewData["Proba"] = 1;
            //ViewData["HospitalID"] = new SelectList(_context.Hospitals, "HospitalID", "HospitalID");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewID,UserName,Department,Salary,Speciality,Date,LinkToText,OwnerId,HospitalID,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HospitalID"] = review.HospitalID;
            //ViewData["HospitalID"] = new SelectList(_context.Hospitals, "HospitalID", "HospitalID", review.HospitalID);
            return View(review);
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "HospitalID", "HospitalID", review.HospitalID);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewID,UserName,Department,Salary,Date,LinkToText,OwnerId,HospitalID")] Review review)
        {
            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.ReviewID))
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
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "HospitalID", "HospitalID", review.HospitalID);
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Hospital)
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }
    }
}
