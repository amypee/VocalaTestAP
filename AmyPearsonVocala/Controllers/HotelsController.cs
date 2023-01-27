using AmyPearsonVocala.Models;
using AmyPearsonVocala.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AmyPearsonVocala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : ControllerBase
    {
        private IBookingService _bookings;

        public HotelsController(IBookingService bookings)
        {
            _bookings = bookings;

        }

        [HttpPost()]
        [Route("/ConfirmHotelBooking")]
        public async Task<ActionResult<BookingRequest>> ConfirmHotelBooking([FromBody] BookingRequest bookingRequest)
        {
            if (bookingRequest == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Booking Request");
            }
            if (bookingRequest.Id > 0)
            {
                try
                {
                    _bookings.UpdateBooking(bookingRequest);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error updating booking: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    _bookings.CreateBooking(bookingRequest);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error creating booking: " + ex.Message);
                }
            }

            return Ok(bookingRequest);
        }

        [HttpGet()]
        [Route("/GetHotelBookings")]
        public async Task<ActionResult<IEnumerable<BookingRequest>>> GetHotelBookings()
        {
            try
            {
                var response = _bookings.GetAllBookings();

                if (response == null)
                {
                    return NotFound("No bookings found");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error retrieving bookings: " + ex.Message);
            }
        }
    }
}
