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
    public class LibraryBooksController : Controller
    {
        private readonly LibraryMVCContext _context;

        public LibraryBooksController(LibraryMVCContext context)
        {
            _context = context;
        }

        // GET: LibraryBooks
        public async Task<IActionResult> Index()
        {
            var libraryMVCContext = _context.LibraryBooks.Include(l => l.Book).Include(l => l.Library);
            return View(await libraryMVCContext.ToListAsync());
        }


        // GET: LibraryBooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.LibraryBooks == null)
            {
                return NotFound();
            }

            var libraryBook = await _context.LibraryBooks
                .Include(l => l.Book)
                .Include(l => l.Library)
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (libraryBook == null)
            {
                return NotFound();
            }

            return View(libraryBook);
        }

        // GET: LibraryBooks/Create
        public IActionResult Create()
        {
            ViewData["ISBN"] = new SelectList(_context.Book, "ISBN", "ISBN");
            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Name");
            return View();
        }

        // POST: LibraryBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ISBN,LibraryId,Cabinet,Shelf,Amount")] LibraryBook libraryBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libraryBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ISBN"] = new SelectList(_context.Book, "ISBN", "ISBN", libraryBook.ISBN);
            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Name", libraryBook.LibraryId);
            return View(libraryBook);
        }

        // GET: LibraryBooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.LibraryBooks == null)
            {
                return NotFound();
            }

            var libraryBook = await _context.LibraryBooks.FindAsync(id);
            if (libraryBook == null)
            {
                return NotFound();
            }
            ViewData["ISBN"] = new SelectList(_context.Book, "ISBN", "ISBN", libraryBook.ISBN);
            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Name", libraryBook.LibraryId);
            return View(libraryBook);
        }

        // POST: LibraryBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ISBN,LibraryId,Cabinet,Shelf,Amount")] LibraryBook libraryBook)
        {
            if (id != libraryBook.ISBN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryBookExists(libraryBook.ISBN))
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
            ViewData["ISBN"] = new SelectList(_context.Book, "ISBN", "ISBN", libraryBook.ISBN);
            ViewData["LibraryId"] = new SelectList(_context.Library, "Id", "Name", libraryBook.LibraryId);
            return View(libraryBook);
        }

        // GET: LibraryBooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.LibraryBooks == null)
            {
                return NotFound();
            }

            var libraryBook = await _context.LibraryBooks
                .Include(l => l.Book)
                .Include(l => l.Library)
                .FirstOrDefaultAsync(m => m.ISBN == id);
            if (libraryBook == null)
            {
                return NotFound();
            }

            return View(libraryBook);
        }

        // POST: LibraryBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.LibraryBooks == null)
            {
                return Problem("Entity set 'LibraryMVCContext.LibraryBooks'  is null.");
            }
            var libraryBook = await _context.LibraryBooks.FindAsync(id);
            if (libraryBook != null)
            {
                _context.LibraryBooks.Remove(libraryBook);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryBookExists(string id)
        {
          return _context.LibraryBooks.Any(e => e.ISBN == id);
        }
    }
}
