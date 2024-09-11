using BackendTZ.Core.Models;
using BackendTZ.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTZ.DataAccess.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<BookingEntity>
    {
        public void Configure(EntityTypeBuilder<BookingEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartTime)
                .IsRequired();
            builder.Property(x => x.StartTime)
                .IsRequired();
            builder.Property(x => x.ConferenceHallId)
                .IsRequired();
        }
    }
}
