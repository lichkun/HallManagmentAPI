

using System.ComponentModel.DataAnnotations;

namespace BackendTZ.DataAccess.Entities
{
    public class ConferenceHallEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; } = 0;
        public decimal BaseRate { get; set; }
        public virtual ICollection<ServiceEntity> Services { get; set; }
    }
}
