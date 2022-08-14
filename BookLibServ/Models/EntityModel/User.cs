using System.ComponentModel.DataAnnotations;

namespace BookLibServ.Models.EntityModel
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(4)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
