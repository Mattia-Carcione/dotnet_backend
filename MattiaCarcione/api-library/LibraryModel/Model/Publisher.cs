namespace LibraryModel.Model
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public required string Name { get; set; }
        public List<Book>? Books { get; set; }
    }
}
