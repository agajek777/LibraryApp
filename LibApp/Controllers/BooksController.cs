using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Data;
using LibApp.Models;
using LibApp.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public Book Book { get; set; }
        public BooksController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
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
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(book);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Books");
            }
            return View("BookForm", book);
        }
        [ValidateAntiForgeryToken]
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
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Json(new { data = await _db.Books.ToListAsync() });
        //}

        //[HttpDelete]
        //public async Task<IActionResult> DeleteBook(int id)
        //{
        //    //Func<Book, bool> func = b => b.Id == id;
        //    var bookInDb = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
        //    if (bookInDb == null)
        //        return Json(new { success = false, message = "Error while deleting" });
        //    _db.Books.Remove(bookInDb);
        //    await _db.SaveChangesAsync();
        //    return Json(new { success = true, message = "Delete successful" });
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditBook(BookDto bookDto)
        //{
        //    var bookInDb = await _db.Books.SingleOrDefaultAsync(b => b.Id == bookDto.Id);
        //    if (bookInDb == null)
        //    {
        //        return Json(new { success = false, Message = "Error while editing" });
        //    }
        //    else
        //    {
        //        bookInDb = _mapper.Map<Book>(book);
        //    }
        //}
        #endregion
    }
}