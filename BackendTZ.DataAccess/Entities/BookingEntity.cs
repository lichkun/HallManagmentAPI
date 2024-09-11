using System.ComponentModel.DataAnnotations;

namespace BackendTZ.DataAccess.Entities
{
    public class BookingEntity
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid ConferenceHallId { get; set; }
        public virtual ConferenceHallEntity ConferenceHall { get; set; }
        public virtual ICollection<ServiceEntity> Services { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
