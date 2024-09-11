using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BackendTZ.DataAccess.Configurations
{
    public class ConferenceHallConfiguration : IEntityTypeConfiguration<ConferenceHallEntity>
    {
        public void Configure(EntityTypeBuilder<ConferenceHallEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .HasMaxLength(ConferenceHall.MAX_NAME_LENGTH)
                .IsRequired();
            builder.Property(x => x.Capacity)
                .IsRequired();
            builder.Property(x => x.BaseRate)
                .IsRequired();

            builder.HasCheckConstraint("CK_ConferenceHall_Capacity", $"[Capacity]<= {ConferenceHall.MAX_HALL_CAPACITY}");
        }
    }
}
