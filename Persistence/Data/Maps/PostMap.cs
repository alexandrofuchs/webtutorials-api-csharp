using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Persistence.Data.Maps
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            #region TABLENAME
            builder.ToTable("POST");
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

