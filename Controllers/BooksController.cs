using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamMvc.Models;
using ExamMvc.ViewModels;
using ExamMvc.Enums;

namespace ExamMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly MobileContext _context;

        public BooksController(MobileContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(int page =1)
        { 
            IQueryable<Book> books = _context.Books;
            books = books.OrderByDescending(x => x.DateAdded);
            int pagesize = 8;
            var count = await _context.Books.CountAsync();
            var items = await books.Skip((page-1)*pagesize).Take(pagesize).ToListAsync(); 
            var pvm= new PageViewModel(count,page,pagesize);
            var model = new BookPageModel
            {
                Books = items,
                PageViewModel = pvm
            };
            return View(model);
        }


        public  IActionResult Back(int BookId)
        {
            IQueryable<Book> Books = _context.Books;
            List<Take> Takess = _context.Takes.ToList();
            Take take = new Take();
            Book book = new Book();
            foreach (Book b in Books)
            {
                if(b.Id == BookId)
                {
                    b.Status = "In stock";
                    book = b;             
                    foreach (Take t in Takess)
                    {
                        if (t.BookId == book.Id)
                        {
                            take=t;
                        }
                    }
                   
                }
            }
            _context.Update(book);
            _context.Takes.Remove(take);
            _context.SaveChanges();
            return Redirect("https://localhost:44300/Books");  
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,AuthorName,Pic,YearOfIssue,Description,DateAdded")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.DateAdded = DateTime.Now;
                book.Status = "In stock";
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,AuthorName,Pic,YearOfIssue,Description,DateAdded")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
