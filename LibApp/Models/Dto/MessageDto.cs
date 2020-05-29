using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models.Dto
{
    public class MessageDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Sent { get; set; }
        public AppUserDto AppUser { get; set; }
    }
}