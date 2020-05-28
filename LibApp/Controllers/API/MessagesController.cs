using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MessagesController(ApplicationDbContext db)
        {
            this._db = db;
        }
        [HttpGet]
        public async Task<JsonResult> GetMessagesAsync()
        {
            var msgs = await _db.Messages.OrderBy(m => m.Sent).Include(m => m.AppUser).ToListAsync();
            return Json(new { data = msgs });
        }
    }
}