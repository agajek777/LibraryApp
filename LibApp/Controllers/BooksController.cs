using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using LibApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Book Book { get; set; }
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (true)
            {

            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return View("BookForm", book);
        }

        public IActionResult New()
        {
            var book = new Book();
            return View("BookForm", book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(Book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Books");
            }
            return View("BookForm", Book);
        }

        [HttpPost]
        public async Task<IActionResult> New(Book Book)
        {
            if (ModelState.IsValid)
            {
                await _db.Books.AddAsync(Book);
                _db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }
            return View("BookForm", Book);
        }


        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookInDb = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookInDb == null)
                return Json(new { success = false, message = "Error while deleting" });
            _db.Books.Remove(bookInDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}