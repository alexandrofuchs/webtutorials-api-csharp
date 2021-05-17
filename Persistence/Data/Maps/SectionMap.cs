using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Persistence.Data.Maps
{
    public class SectionMap : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            #region TABLENAME
            builder.ToTable("SECTION");
            #endregion TABLENAME

            #region ATTRIBUTES
            #region PRIMARY KEY
            builder.HasKey(c => c.Id);
            #endregion PRIMARY KEY
            builder.Property(c => c.CreatedAt).IsRequired(true);
            builder.Property(c => c.UpdatedAt).IsRequired(true);
            builder.Property(c => c.Description).HasMaxLength(50).IsRequired(true);
            #endregion ATTRIBUTES

            #region UNIQUE KEYS
            builder.HasIndex(c => c.Description).IsUnique().HasDatabaseName("UK_DESCRIPTION");
            #endregion UNIQUE KEYS
        }
    }
}

