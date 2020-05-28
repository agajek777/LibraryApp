using System;

namespace LibApp.Models.Dto
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public AppUserDto AppUser { get; set; }
    }
}