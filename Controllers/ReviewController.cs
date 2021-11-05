using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospitals.Data;
using Hospitals.Models;
using CustomClasses;

namespace Hospitals.Controllers
{
    public class ReviewController : Controller
    {
        private readonly HospitalDataContext _context;
        private DateTime dateTime = new DateTime();

        private string[] _salaries;

        private int[] _ratings;

        private string[] _departments;

        public ReviewController(HospitalDataContext context)
        {
            _context = context;

            _salaries = ItemsForLists.GetSalaries();
            _ratings = ItemsForLists.GetRatings();
            _departments = ItemsForLists.GetDepartments();
            dateTime = DateTime.Now;
        }

        // GET: Review
        public async Task<IActionResult> Index(string searchString, string filterApplied, int? pageNumber)
        {
         // IEnumerable<Review> reviews = await _context.Reviews.Include(r => r.Hospital).AsNoTracking().ToListAsync();
          var reviews = from s in _context.Reviews.Include(r => r.Hospital) select s;

          if (searchString != null)
            {
                pageNumber = 1;
            }

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                reviews = reviews.Where(p => p.Speciality.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrWhiteSpace(filterApplied))
            {
                reviews = reviews.Where(p => p.Department.ToUpper() == filterApplied.ToUpper());

            }

            ViewBag.filters = new SelectList(_departments);

            ViewBag.filterApplied = filterApplied;

            int pageSize = 5;

            return View(await PaginatedList<Review>.CreateAsync(reviews, pageNumber ?? 1, pageSize));
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
            ViewBag.ratings = new SelectList(_ratings);

            ViewBag.departments = new SelectList(_departments);
            ViewBag.salaries = new SelectList(_salaries);
            //ViewData["HospitalID"] = new SelectList(_context.Hospitals, "HospitalID", "HospitalID");
            return View();
        }

        // POST: Review/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReviewID,UserName,Department,Salary,Speciality,Date,Agency,EmploymentType,ReviewText,OwnerId,HospitalID,Rating")] Review review)
        {
            review.Date = dateTime;

            if (ModelState.IsValid)
            {
                _context.Add(review);

                Hospital hospital = await _context.Hospitals.FirstAsync(p => p.HospitalID == review.HospitalID);
                hospital.RatingTotal += review.Rating;
                hospital.ReviewsCount++;
                hospital.Rating = Math.Round((double)hospital.RatingTotal / hospital.ReviewsCount, 2);

                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Hospital", new { id = review.HospitalID});
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

            ViewData["oldRating"] = review.Rating;
            ViewData["HospitalID"] = review.HospitalID;
            ViewBag.ratings = new SelectList(_ratings);

            ViewBag.departments = new SelectList(_departments);
            ViewBag.salaries = new SelectList(_salaries);
            return View(review);
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ReviewID,UserName,Department,Salary,Date,Rating,Speciality,Agency,ReviewText,OwnerId,HospitalID")] Review review, int oldRating)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);

                    var hospital = await _context.Hospitals.FirstAsync(p => p.HospitalID == review.HospitalID);
                    hospital.RatingTotal -= oldRating;
                    hospital.RatingTotal += review.Rating;
                    hospital.Rating = Math.Round((double)hospital.RatingTotal / hospital.ReviewsCount, 2);

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
                return RedirectToAction("Details", "Hospital", new { id = review.HospitalID});
            }

            ViewBag.ratings = new SelectList(_ratings);
            ViewBag.departments = new SelectList(_departments);
            ViewBag.salaries = new SelectList(_salaries);
            ViewData["HospitalID"] = review.HospitalID;

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

            var hospital = await _context.Hospitals.FirstAsync(p => p.HospitalID == review.HospitalID);
            hospital.RatingTotal -= review.Rating;
            hospital.ReviewsCount--;
            hospital.Rating = Math.Round((double)hospital.RatingTotal / hospital.ReviewsCount, 2);

            _context.Update(hospital);
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
