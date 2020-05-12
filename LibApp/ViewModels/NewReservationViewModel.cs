using LibApp.Models;
using LibApp.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.ViewModels
{
    public class NewReservationViewModel
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Target Date")]
        //[TargetDate]
        public DateTime TargetDate { get; set; }
        public List<Book> Books { get; set; }
        public List<Client> Clients { get; set; }
        public SelectList SelectList { get; set; }
        public static DateTime Now { get; set; }

        public NewReservationViewModel()
        {
            Now = DateTime.Now;
        }
    }
}
