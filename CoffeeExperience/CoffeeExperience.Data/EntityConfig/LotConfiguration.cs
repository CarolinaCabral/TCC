using System.Data.Entity.ModelConfiguration;
using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Data.EntityConfig
{
    public class LotConfiguration : EntityTypeConfiguration<Lot>
    {
        public LotConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Code).IsRequired();
            Property(p => p.LotStatus).IsRequired();
            Property(p => p.Weight).IsRequired();
            Property(p => p.Vality).IsRequired();

            HasMany<Analysis>(p => p.ListAnalysis).WithRequired(v => v.Lot);
            HasMany<Product>(p => p.ListProduct).WithRequired(v => v.Lot);
            HasOptional(p => p.User).WithMany().HasForeignKey(p => p.UserId);

        }
    }
}
