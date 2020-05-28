using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Data;
using LibApp.Models;
using LibApp.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers.API
{
    //public delegate bool MyDel(Book book, int id);


    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _singInManager;

        //private MyDel myDel;
        public BooksController(ApplicationDbContext db, IMapper mapper, SignInManager<AppUser> singInManager)
        {
            _db = db;
            _mapper = mapper;
            _singInManager = singInManager;
        }
        // GET api/books/
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            return Json(new { data = await _db.Books.Select(b => _mapper.Map<BookDto>(b)).ToListAsync() });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            //myDel = (Book b, int id) => b.Id == id;
            var book = await _db.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
                return NotFound();
            else
                return Json(new { data = _mapper.Map<BookDto>(book) });
        }
        [HttpPost]
        public async Task<IActionResult> NewBook(BookDto bookDto)
        {
            if (ModelState.IsValid)
            {
                var book = _mapper.Map<Book>(bookDto);
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                return Json(new { data = _mapper.Map<BookDto>(book) });
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var bookInDb = await _db.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (bookInDb == null)
            {
                return BadRequest();
            }
            //bookDto.Id = id;
            _mapper.Map<BookDto, Book>(bookDto, bookInDb);
            await _db.SaveChangesAsync();
            return Json(new { data = _mapper.Map<BookDto>(bookInDb) });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int? id)
        {
            var bookInDb = await _db.Books.SingleOrDefaultAsync(b => b.Id == id);
            if (id == null || bookInDb == null || !_singInManager.IsSignedIn(User))
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _db.Books.Remove(bookInDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful." });
        }
    }
}