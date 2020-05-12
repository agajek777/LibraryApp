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
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books, TargetDate = DateTime.Now});
            }
            else if (books.Count(b => b.Id == id) == 1)
            {
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books, BookId = (int)id, TargetDate = DateTime.Now });
            }
            else
            {
                return View("ReservationForm", new NewReservationViewModel() { SelectList = new SelectList(booksNAuthors, "Id", "Description"), Clients = clients, Books = books, TargetDate = DateTime.Now });
            }
        }

        public IActionResult Active()
        {
            return View();
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

        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            return Json(new { data = await _db.Reservations.Include(r => r.Client).Include(r => r.Book).Where(r => r.TargetDate >= DateTime.Now).ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var reservationInDb = await _db.Reservations.SingleOrDefaultAsync(r => r.Id == id);
            if (reservationInDb != null)
            {
                _db.Reservations.Remove(reservationInDb);
                _db.SaveChanges();
                return Json(new { success = true, message = "The Reservation has been terminated."});
            }
            return Json(new { success = false, message = "Error while deleting" });
        }

        #endregion
    }
}