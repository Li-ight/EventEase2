namespace EventEase.Models;
    public class Venue
{
    public int VenueID { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int Capacity { get; set; }

    // Navigation property
    public ICollection<Booking>? Bookings { get; set; }
}
