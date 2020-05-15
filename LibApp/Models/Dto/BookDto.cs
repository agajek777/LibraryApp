using System.ComponentModel.DataAnnotations;

namespace LibApp.Models.Dto
{
    internal class BookDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        [Range(0, 20)]
        [Required]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
    }
}