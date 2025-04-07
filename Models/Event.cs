namespace EventEase.Models; 
public class Event
{
    public int EventID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    // Navigation property
    public ICollection<Booking>? Bookings { get; set; }
}
