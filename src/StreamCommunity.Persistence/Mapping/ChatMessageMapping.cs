using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StreamCommunity.Domain;

namespace StreamCommunity.Persistence.Mapping;

public class ChatMessageMapping : IEntityTypeConfiguration<ChatMessageTemplate>
{
    public void Configure(EntityTypeBuilder<ChatMessageTemplate> builder)
    {
        builder.ToTable("ChatMessageTemplates", "sc");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Identifier)
            .HasColumnName("Identifier")
            .HasMaxLength(50);
        builder.Property(x => x.Message)
            .HasColumnName("Message");

        builder.HasIndex(x => x.Identifier)
            .IsUnique();
    }
}