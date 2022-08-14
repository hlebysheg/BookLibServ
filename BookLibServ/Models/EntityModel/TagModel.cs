using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibServ.Models.EntityModel
{
    [Table("ganre")]
    public class TagModel
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public List<BookModel> Books { get; set; }
    }
}
