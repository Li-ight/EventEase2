using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;
using System.Threading.Tasks;
using System.Linq;
using EventEase.Models.ViewModels;


namespace EventEase.Controllers
{
    public class VenuesController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public VenuesController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: Venues/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If validation fails, return the view with validation errors
            return View(venue);
        }



        // GET: Venue
        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venues
                .Include(v => v.Bookings)
                .Select(v => new VenueWithBookingCount
                {
                    Venue = v,
                    BookingCount = v.Bookings.Count
                })
                .ToListAsync();

            return View(venues);
        }

        // GET: Venue/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // POST: Venue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueID,Name,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (id != venue.VenueID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

        // GET: Venue/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var venue = await _context.Venues
                .FirstOrDefaultAsync(m => m.VenueID == id);
            if (venue == null)
                return NotFound();

            return View(venue);
        }

        // POST: Venue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null)
                return NotFound();

            bool hasBookings = await _context.Bookings.AnyAsync(b => b.VenueID == id);

            if (hasBookings)
            {
                TempData["VenueDeleteError"] = "Cannot delete this venue because it has active bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venues.Any(e => e.VenueID == id);
        }

        public async Task<IActionResult> Available()
        {
            var allVenues = await _context.Venues
                .Include(v => v.Bookings)
                .ToListAsync();

            var availableVenues = allVenues
                .Where(v => v.Bookings == null || v.Bookings.Count == 0)
                .ToList();

            return View(availableVenues);
        }
    }
}
