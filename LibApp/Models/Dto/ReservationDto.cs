using System;
using System.ComponentModel.DataAnnotations;

namespace LibApp.Models.Dto
{
    internal class ReservationDto
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