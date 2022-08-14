using System.ComponentModel.DataAnnotations;

namespace BookLibServ.Models.EntityModel
{
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public string Author { get; set; }
        //ref
        public TagModel Tag { get; set; }
        public int TagId { get; set; }
    }
}
