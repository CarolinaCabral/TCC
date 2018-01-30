using CoffeeExperience.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace CoffeeExperience.Data.EntityConfig
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired();
            Property(p => p.CPF).IsRequired();
            Property(p => p.Email).IsRequired();
            Property(p => p.Password).IsRequired().HasMaxLength(100);
            Property(p => p.FacebookId).IsOptional();
            Property(p => p.Token).IsOptional();
            Property(p => p.Status).IsRequired();

            HasMany<Lot>(p => p.ListLots).WithOptional(v => v.User);
            HasMany<Laboratory>(p => p.ListLaboratory).WithOptional(v => v.User);
        }
    }
}
