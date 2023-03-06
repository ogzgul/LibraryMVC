using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryMVC.Data;
using LibraryMVC.Models;

namespace LibraryMVC.Controllers
{
    public class DistrictsController : Controller
    {
        private readonly LibraryMVCContext _context;

        public DistrictsController(LibraryMVCContext context)
        {
            _context = context;
        }

        // GET: Districts
        public async Task<IActionResult> Index()
        {
            var libraryMVCContext = _context.District.Include(d => d.City).OrderBy(f=>f.City.Name).ThenBy(d=>d.Name);
            return View(await libraryMVCContext.ToListAsync());
        }
        public async Task<PartialViewResult> CityDistricts(byte cityId)
        {
            var libraryMVCContext = _context.District.Where(b => b.CityId==cityId).OrderBy(b => b.Name);
            return PartialView(await libraryMVCContext.ToListAsync());
        }

        // GET: Districts/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.District == null)
            {
                return NotFound();
            }

            var district = await _context.District
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // GET: Districts/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City, "PlateCode", "Name");
            return View();
        }

        // POST: Districts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CityId,Population")] District district)
        {
            if (ModelState.IsValid)
            {
                _context.Add(district);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.City, "PlateCode", "Name", district.CityId);
            return View(district);
        }

        // GET: Districts/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.District == null)
            {
                return NotFound();
            }

            var district = await _context.District.FindAsync(id);
            if (district == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.City, "PlateCode", "Name", district.CityId);
            return View(district);
        }

        // POST: Districts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name,CityId,Population")] District district)
        {
            if (id != district.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(district);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DistrictExists(district.Id))
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
            ViewData["CityId"] = new SelectList(_context.City, "PlateCode", "Name", district.CityId);
            return View(district);
        }

        // GET: Districts/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.District == null)
            {
                return NotFound();
            }

            var district = await _context.District
                .Include(d => d.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (district == null)
            {
                return NotFound();
            }

            return View(district);
        }

        // POST: Districts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.District == null)
            {
                return Problem("Entity set 'LibraryMVCContext.District'  is null.");
            }
            var district = await _context.District.FindAsync(id);
            if (district != null)
            {
                _context.District.Remove(district);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DistrictExists(short id)
        {
          return _context.District.Any(e => e.Id == id);
        }
    }
}
