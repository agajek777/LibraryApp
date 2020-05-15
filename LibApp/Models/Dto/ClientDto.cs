using System.ComponentModel.DataAnnotations;

namespace LibApp.Models.Dto
{
    internal class ClientDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [StringLength(11, ErrorMessage = "Pesel number is incorrect. Please retype the Pesel.", MinimumLength = 11)]
        public string Pesel { get; set; }
    }
}