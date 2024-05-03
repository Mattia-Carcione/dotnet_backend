namespace LibraryModel.Model
{
    public class Category
    {
        public int CategoryID { get; set; }
        public required string Genre { get; set; }
        public string? Description { get; set; }
        public List<Book>? Books { get; set; }
    }
}
