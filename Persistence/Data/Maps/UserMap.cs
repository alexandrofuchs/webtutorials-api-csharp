using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTutorialsApp.Domain.Entities;

namespace WebTutorialsApp.Persistence.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            #region TABLENAME
            builder.ToTable("USER");
            #endregion TABLENAME

            #region ATTRIBUTES
            builder.HasKey(c => c.Id);
            builder.Property(c => c.IsAdmin).HasDefaultValue(false);
            builder.Property(c => c.CreatedAt).IsRequired(true);
            builder.Property(c => c.UpdatedAt).IsRequired(true);
            builder.Property(c => c.FirstName).HasMaxLength(30).IsRequired(true);
            builder.Property(c => c.LastName).HasMaxLength(40).IsRequired(true);
            builder.Property(c => c.Email).HasMaxLength(100).IsRequired(true);            
            builder.Property(c => c.Password).HasMaxLength(128).IsRequired(true);
            #endregion ATTRIBUTES

            #region UNIQUE KEYS

            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName("UK_EMAIL");

            #endregion UNIQUE KEYS
        }
    }    
}
