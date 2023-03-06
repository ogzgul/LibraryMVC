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
    public class LibrariesController : Controller
    {
        private readonly LibraryMVCContext _context;

        public LibrariesController(LibraryMVCContext context)
        {
            _context = context;
        }
       
        // GET: Libraries
        public async Task<IActionResult> Index()
        {
            

            var libraryMVCContext = _context.Library.Include(l => l.District).Include(m=>m.District.City);
            return View(await libraryMVCContext.ToListAsync());
        }

        public async Task<PartialViewResult> DistrictsLibraries(byte districtId)
        {
            var libraryMVCContext = _context.Library.Where(b => b.DistrictId == districtId).OrderBy(b => b.Name);
            return PartialView(await libraryMVCContext.ToListAsync());
        }
        // GET: Libraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.City.OrderBy(c=>c.Name), "PlateCode", "Name");

            return View();
        }
        

        // POST: Libraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,BookCount,Capacity,EstablishmentDate,DistrictId")] Library library)
        {
            if (ModelState.IsValid)
            {
                _context.Add(library);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name", library.DistrictId);
            return View(library);
        }

        // GET: Libraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }
            
            ViewData["CityId"] = new SelectList(_context.City,"PlateCode","Name",library.District.CityId);
            ViewData["DistrictId"] = new SelectList(_context.District.Where(d=>d.CityId==library.District.CityId), "Id", "Name",library.DistrictId);
            
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,BookCount,Capacity,EstablishmentDate,DistrictId")] Library library)
        {
            if (id != library.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(library);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryExists(library.Id))
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
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Name", library.DistrictId);
            return View(library);
        }

        // GET: Libraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Library == null)
            {
                return NotFound();
            }

            var library = await _context.Library
                .Include(l => l.District)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Library == null)
            {
                return Problem("Entity set 'LibraryMVCContext.Library'  is null.");
            }
            var library = await _context.Library.FindAsync(id);
            if (library != null)
            {
                _context.Library.Remove(library);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryExists(int id)
        {
          return _context.Library.Any(e => e.Id == id);
        }
    }
}
