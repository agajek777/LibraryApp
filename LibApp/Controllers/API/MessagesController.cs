using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibApp.Data;
using LibApp.Models.Dto;
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
        private readonly IMapper _mapper;

        public MessagesController(ApplicationDbContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<JsonResult> GetMessages()
        {
            var msgs = await _db.Messages.OrderBy(m => m.Sent).Include(m => m.AppUser).Select(m => _mapper.Map<MessageDto>(m)).ToListAsync();
            return Json(new { data = msgs });
        }
    }
}