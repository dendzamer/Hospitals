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
    public class HospitalController : Controller
    {
        private readonly HospitalDataContext _context;
        private List<string> filteri = new List<string>();

        public HospitalController(HospitalDataContext context)
        {
            _context = context;
            foreach (var item in _context.Hospitals)
            {
                filteri.Add(item.State);
            }
        }

        // GET: Hospital
        public async Task<IActionResult> Index(string searchString, string filterApplied)
        {
            IEnumerable<Hospital> hospitals = await _context.Hospitals.Include(p => p.Reviews).AsNoTracking().ToListAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                hospitals = hospitals.Where(p => p.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!String.IsNullOrEmpty(filterApplied))
            {
                hospitals = hospitals.Where(p => p.State.ToUpper() == filterApplied.ToUpper());
            }

            foreach (var item in hospitals)
            {
                item.ReviewsCount = item.Reviews.Count();
            }


            ViewBag.filters = new SelectList(filteri);
            return View(hospitals);
        }

        // GET: Hospital/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.Include(p => p.Reviews).AsNoTracking()
                .FirstOrDefaultAsync(m => m.HospitalID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospital/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hospital/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HospitalID,Name,Address,State,Zip")] Hospital hospital)
        {

            //Ovde moram da podesim da ownerId bude jednak Id-u ulogovanog korisnika
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        // GET: Hospital/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HospitalID,Name,Address,State,Zip,Rating,ReviewsCount,OwnerId")] Hospital hospital)
        {
            if (id != hospital.HospitalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.HospitalID))
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
            return View(hospital);
        }

        // GET: Hospital/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.HospitalID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.HospitalID == id);
        }
    }
}
