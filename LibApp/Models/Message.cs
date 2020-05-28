using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Sent { get; set; }
        public AppUser AppUser { get; set; }

    }
}
