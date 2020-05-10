using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public DateTime TargetDate { get; set; }
    }
}
