using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using LibApp.Models;
using LibApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers
{
    public class ReservationsController : Controller
    {
        public ApplicationDbContext _db { get; set; }
        public ReservationsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> New(int? id)
        {
            var books = await _db.Books.ToListAsync();
            if (id != null && books.Count(b => b.Id == id) == 1)
            {
                var book = books.SingleOrDefault(b => b.Id == id);
                books.Remove(book);
                books.Insert(0, book);
            }
            var booksNAuthors = await _db.Books.Select(s => new {
                Id = s.Id,
                Description = $"{s.Name} - {s.Author}"
            }).ToListAsync();
            var clients = await _db.Clients.OrderBy(c => c.Id).ToListAsync();
            if (id == null)
            {
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books });
            }
            else if (books.Count(b => b.Id == id) == 1)
            {
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books, BookId = (int)id });
            }
            else
            {
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books });
            }
        }

        [HttpPost]
        public async Task<IActionResult> New(NewReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation()
                {
                    Book = await _db.Books.FindAsync(model.BookId),
                    Client = await _db.Clients.FindAsync(model.ClientId),
                    TargetDate = model.TargetDate
                };
                _db.Reservations.Add(reservation);
                _db.SaveChanges();
                return Redirect("/books/index");
            }

            return RedirectToAction("New", new { Id = model.BookId });
        }
    }
}