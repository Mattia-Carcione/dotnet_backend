namespace LibraryModel.Model
{
    public class Book
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public int? NumberOfPages { get; set; }
        public DateTime? PublishingDate { get; set; }
        public required int TotalCopies { get; set; }
        public required int TotalCopiesLeft { get; set; }
        public required Author Author { get; set; }
        public int AuthorID { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Booking>? Bookings { get; set; }
        public required Publisher Publisher { get; set; }
        public int PublisherID { get; set;}
    }
}
