using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;
    public class Venue
{
    public int VenueID { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
    public int Capacity { get; set; }

    // Navigation property
    public ICollection<Booking>? Bookings { get; set; }
}
