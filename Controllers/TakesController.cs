using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamMvc.Models;
using ExamMvc.ViewModels;

namespace ExamMvc.Controllers
{
    public class TakesController : Controller
    {
        private readonly MobileContext _context;

        public TakesController(MobileContext context)
        {
            _context = context;
        }

        // GET: Takes
        public async Task<IActionResult> Index()
        {
            var mobileContext = _context.Takes.Include(t => t.Book).Include(t => t.User);
            return View(await mobileContext.ToListAsync());
        }

        // GET: Takes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Takes
                .Include(t => t.Book)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (take == null)
            {
                return NotFound();
            }

            return View(take);
        }

        // GET: Takes/Create
        public IActionResult Create(int BookId)
        {
            ViewBag.BookId=BookId; 
            return View();
        }

        // POST: Takes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,UserId,BookId")] Take take)
        {
            take.UserId = 0;
            List<Book> books = _context.Books.ToList();
            List<User> Users = _context.Users.ToList();
            
            if (ModelState.IsValid)
            {
                foreach (Book b in books)
                {
                    if (b.Id == take.BookId)
                    {  
                        b.Status = "Given";
                    }
                }
                foreach (User u in Users)
                {
                    if (u.Email == take.Email)
                    {
                       take.UserId= u.Id;
                    }
                }
                _context.Add(take);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "AuthorName", take.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", take.UserId);
            return View(take);
        }

        // GET: Takes/Edit/5
        public IActionResult Person()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Person(string Email)
        {
            List<Take>Takes= _context.Takes.ToList();

            List<Book> Bookss = new List<Book>();
            List<Book> Bookss1 = _context.Books.ToList();

            
            foreach (Take t in Takes)           
            {
                if(t.Email == Email)
                {
                    foreach(Book b in Bookss1)
                    {
                        if (t.BookId == b.Id)
                        {
                            Bookss.Add(b);
                        }
                    }
                }
            }
            BookPageModel tvm = new BookPageModel
            {
                Books = Bookss
            };
            return View(tvm);
        }
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Takes.FindAsync(id);
            if (take == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "AuthorName", take.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", take.UserId);
            return View(take);
        }

        // POST: Takes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,UserId,BookId")] Take take)
        {
            if (id != take.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(take);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakeExists(take.Id))
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
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "AuthorName", take.BookId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", take.UserId);
            return View(take);
        }

        // GET: Takes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var take = await _context.Takes
                .Include(t => t.Book)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (take == null)
            {
                return NotFound();
            }
            return View(take);
        }

        // POST: Takes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var take = await _context.Takes.FindAsync(id);
            _context.Takes.Remove(take);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakeExists(int id)
        {
            return _context.Takes.Any(e => e.Id == id);
        }
        public bool IsExistsEmailNMore3(string email)
        {
            IQueryable<User> users = _context.Users;
            IQueryable<Take> Takes = _context.Takes;
            int isThree = 0;
            foreach (Take t in Takes)
            {
                if (t.Email == email)
                {
                    isThree++;
                }
            }
            if (isThree >= 3)
            {
                return false;
            }
            foreach (User u in users)
            {
                if(u.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
