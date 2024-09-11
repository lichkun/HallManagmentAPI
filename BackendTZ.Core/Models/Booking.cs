namespace BackendTZ.Core.Models
{
    public class Booking
    {
        public Booking() { }
        public Booking(Guid id, DateTime startTime, DateTime endTime, Guid conferenceHallId, List<Guid> services, decimal totalPrice)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            ConferenceHallId = conferenceHallId;
            ServicesIds = services;
            TotalPrice = totalPrice;
        }

        public Guid Id { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public Guid ConferenceHallId { get; }
        public List<Guid> ServicesIds { get; }
        public decimal TotalPrice { get;  }
        public static (Booking hall, string Error) Create(Guid id, DateTime startTime, DateTime endTime, Guid conferenceHallId, List<Guid> servicesIds, decimal TotalPrice)
        {
            var error = string.Empty;
            TimeSpan duration = endTime - startTime;
            if (startTime > endTime || duration.TotalHours <2)
            {
                error = "startTime cannot be bigger then endtime or duration of event less than 2 hours";
            }
            var booking = new Booking(id, startTime, endTime, conferenceHallId, servicesIds, TotalPrice);
            return (booking, error);
        }

    }
}
