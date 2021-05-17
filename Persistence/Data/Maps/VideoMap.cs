using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Persistence.Data.Maps
{
    public class VideoMap : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            #region TABLENAME
            builder.ToTable("VIDEO");
            #endregion TABLENAME

            #region ATTRIBUTES
            #region PRIMARY KEY
            builder.HasKey(c => c.Id);
            #endregion PRIMARY KEY
            builder.Property(c => c.CreatedAt).IsRequired(true);
            builder.Property(c => c.FilePath).IsRequired(true);   
            builder.Property(c => c.StoragedFileName).IsRequired(true);   
            builder.Property(c => c.UpdatedAt).IsRequired(true);       
            #endregion ATTRIBUTES

            #region UNIQUE KEYS
            //builder.HasIndex(c => c.Description).IsUnique().HasDatabaseName("UK_DESCRIPTION");
            #endregion UNIQUE KEYS
        }
    }
}

