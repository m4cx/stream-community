using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamCommunity.Domain;

namespace StreamCommunity.Persistence.Mapping
{
    internal sealed class EnlistmentDbMapping : IEntityTypeConfiguration<Enlistment>
    {
        public void Configure(EntityTypeBuilder<Enlistment> builder)
        {
            builder.ToTable("Enlistments", "tc");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasColumnName("UserName");
            builder.Property(x => x.Timestamp)
                .HasColumnName("Timestamp")
                .HasConversion<DateTime>(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}