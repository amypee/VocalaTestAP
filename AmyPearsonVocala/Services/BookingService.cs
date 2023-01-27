using AmyPearsonVocala.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmyPearsonVocala.Services
{
    public interface IBookingService
    {
        public IEnumerable<BookingRequest> GetAllBookings();
        public void UpdateBooking(BookingRequest booking);
        public void CreateBooking(BookingRequest booking);

    }
    public class BookingService : IBookingService
    {
        private readonly HotelManagementContext _context;
        public BookingService(HotelManagementContext context)
        {
            _context = context;
        }
        public IEnumerable<BookingRequest> GetAllBookings()
        {
           
                var list = from Bookings in _context.Bookings
                           select Bookings;

                var bookings = list.ToList();
                return bookings;
           
        }
        public void UpdateBooking(BookingRequest booking)
        {
            var existingBooking = _context.Bookings.FirstOrDefault(x => x.Id == booking.Id);

            if (existingBooking == null)
            {
                throw new Exception("Booking not found.");
            }
            else
            {
                existingBooking.HotelName = booking.HotelName;
                existingBooking.BookingDate = booking.BookingDate;
                existingBooking.Cost = booking.Cost;
                existingBooking.Status = booking.Status;

                _context.SaveChanges(); 
            }
        }
        public void CreateBooking(BookingRequest booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }
    }

}
