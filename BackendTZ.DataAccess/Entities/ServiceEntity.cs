using System.ComponentModel.DataAnnotations;

namespace BackendTZ.DataAccess.Entities
{
    public class ServiceEntity
    {
        public Guid Id { get; set; }
        public string Name { get;set; } = string.Empty;
        public decimal Price { get; set;}
        public virtual ICollection<ConferenceHallEntity> ConferenceHalls { get;set; }
        public virtual ICollection<BookingEntity> Bookings { get; set;}
    }
}
