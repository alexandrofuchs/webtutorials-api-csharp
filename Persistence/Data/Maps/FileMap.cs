using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Persistence.Data.Maps
{
    public class FileMap : IEntityTypeConfiguration<PostFile>
    {
        public void Configure(EntityTypeBuilder<PostFile> builder)
        {
            #region TABLENAME
            builder.ToTable("POSTFILE");
            #endregion TABLENAME

            #region ATTRIBUTES
            #region PRIMARY KEY
            builder.HasKey(c => c.Id);
            #endregion PRIMARY KEY
            builder.Property(c => c.CreatedAt).IsRequired(true);
            builder.Property(c => c.UpdatedAt).IsRequired(true);
            #endregion ATTRIBUTES
        }
    }
}

