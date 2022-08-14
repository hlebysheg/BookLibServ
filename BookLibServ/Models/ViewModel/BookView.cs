namespace BookLibServ.Models.ViewModel
{
    public class BookView
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Release { get; set; }
        public string Author { get; set; }
        //ref
        public TagView Tag { get; set; }
    }
}
