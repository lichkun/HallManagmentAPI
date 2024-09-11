using BackendTZ.Application.Services;
using BackendTZ.Contracts.Requests;
using BackendTZ.Contracts.Response;
using BackendTZ.Core.Abstractions;
using BackendTZ.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendTZ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IService<Booking> _bookingService;
        private readonly IService<ConferenceHall> _hallservice;
        private readonly IService<Service> _srvservice;

        public BookingController(IService<Booking> bookingService, IService<ConferenceHall> hallservice, IService<Service> srvservice)
        {
            _bookingService = bookingService;
            _hallservice = hallservice;
            _srvservice = srvservice;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingResponse>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAll();
            var response = bookings.Select(b => new BookingResponse(
                b.Id, b.StartTime, b.EndTime, b.ConferenceHallId, b.ServicesIds, b.TotalPrice));
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingResponse>> GetByIDBooking(Guid id)
        {
            var booking = await _bookingService.GetById(id);
            var response = new BookingResponse(booking.Id, booking.StartTime,booking.EndTime, booking.ConferenceHallId, booking.ServicesIds, booking.TotalPrice);
            return Ok(response);
        }
        //[HttpPost]
        //public async Task<ActionResult<Guid>> CreateBooking([FromBody] BookingRequest request)
        //{
        //    var (booking, error) = Booking.Create(
        //        Guid.NewGuid(),
        //        request.StartTime,
        //        request.EndTime,
        //        request.ConferenceHallId,
        //        request.ServicesIds,
        //        request.TotalPrice
        //    );

        //    if (!string.IsNullOrEmpty(error)) return BadRequest(error);

        //    return Ok(await _bookingService.Create(booking));
        //}

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateBooking(Guid id, [FromBody] BookingRequest request)
        {
            var room = await _hallservice.GetById(request.ConferenceHallId);
            var availableServices = await _srvservice.GetAll();
            var selectedServices = availableServices.Where(s => request.ServicesIds.Contains(s.Id)).ToList();
            var totalPrice = CalculatePrice(room.BaseRate, request.StartTime, request.EndTime, selectedServices);

            var updateBooking = Booking.Create(
                id,
                request.StartTime,
                request.EndTime,
                request.ConferenceHallId,
                request.ServicesIds,
                totalPrice
            ).hall;

            return Ok(await _bookingService.Update(updateBooking));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteBooking(Guid id)
        {
            return Ok(await _bookingService.Delete(id));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> BookRoom( [FromBody] BookingRequest bookingRequest)
        {
            var room = await _hallservice.GetById(bookingRequest.ConferenceHallId);
            if (room == null) return BadRequest("Conference hall not found");

            var availableServices = await _srvservice.GetAll();

            var selectedServices = availableServices.Where(s => bookingRequest.ServicesIds.Contains(s.Id)).ToList();

            var additionalServicesPrice = selectedServices.Sum(s => s.Price);

            var totalPrice = CalculatePrice(room.BaseRate, bookingRequest.StartTime, bookingRequest.EndTime, selectedServices) ;

            var (booking, error) = Booking.Create(
                 Guid.NewGuid(),
                 bookingRequest.StartTime,
                 bookingRequest.EndTime,
                 bookingRequest.ConferenceHallId,
                 bookingRequest.ServicesIds,
                 totalPrice
             );
            if (!string.IsNullOrEmpty(error)) return BadRequest(error);

            await _bookingService.Create(booking);

            return Ok(booking.Id);
        }
        [HttpGet("availablerooms")]
        public async Task<IActionResult> GetAvailableRooms(DateTime startDate, DateTime endDate, int capacity)
        {
            var halls = await _hallservice.GetAll();
            var bookings = await _bookingService.GetAll();
            var availableRooms =  halls
                .Where(r => r.Capacity >= capacity && ! bookings.Any(b => b.ConferenceHallId == r.Id && b.StartTime <= endDate && b.EndTime >= startDate))
                .ToList();
            if (!availableRooms.Any())
            {
                return BadRequest("The conference hall is not available for the selected time.");
            }
            return Ok(availableRooms);
        }
        private decimal CalculatePrice(decimal baseRate, DateTime startTime, DateTime endTime, List<Service> selectedServices)
        {
            var duration = (endTime - startTime).TotalHours;
            decimal totalPrice = baseRate * (decimal)duration;

            if (startTime.Hour >= 18 && startTime.Hour < 23)
            {
                totalPrice *= 0.8m;
            }
            else if (startTime.Hour >= 6 && startTime.Hour < 9)
            {
                totalPrice *= 0.9m;
            }
            else if (startTime.Hour >= 12 && startTime.Hour < 14)
            {
                totalPrice *= 1.15m;
            }
            var additionalServicesPrice = selectedServices.Sum(s => s.Price);
            totalPrice += additionalServicesPrice;

            return totalPrice;
        }
    }
}
