using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace LibApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Message> Messages { get; set; }
    }
}