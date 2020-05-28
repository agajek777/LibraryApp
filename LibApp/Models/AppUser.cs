using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace LibApp.Models
{
    public class AppUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Message> Messages { get; set; }
    }
}