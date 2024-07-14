using Congratulator.Domain.Birthday;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Congratulator.DataAccess.Configurations
{
    public class BirthdayConfiguration : IEntityTypeConfiguration<Birthday>
    {
        public void Configure(EntityTypeBuilder<Birthday> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(b => b.Date)
                .IsRequired();
        }
    }
}
