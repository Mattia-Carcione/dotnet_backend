namespace LibraryModel.Model
{
    public class Booking
    {
        public int BookingID { get; set; }
        public required string User { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public DateTime? DeliveryDate { get; set; }
        public required Book Book { get; set; }
    }
}
