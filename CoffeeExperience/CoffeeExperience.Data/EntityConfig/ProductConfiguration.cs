using System.Data.Entity.ModelConfiguration;
using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Data.EntityConfig
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired();
            
            Property(p => p.Weight).IsRequired();
           

            HasRequired(p => p.Lot).WithMany().HasForeignKey(p => p.LotId);


        }

    }
}
