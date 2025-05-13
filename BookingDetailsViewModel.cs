namespace EventEase.ViewModels
{
    public class BookingDetailsViewModel
    {
        public int BookingId { get; set; }
        public string ClientName { get; set; }

        public int EventId { get; set; }
        public string EventName { get; set; }

        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenueLocation { get; set; }
    }
}
