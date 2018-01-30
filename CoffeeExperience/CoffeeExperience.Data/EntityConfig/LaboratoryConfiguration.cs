using System.Data.Entity.ModelConfiguration;
using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Data.EntityConfig
{
    public class LaboratoryConfiguration : EntityTypeConfiguration<Laboratory>
    {
        public LaboratoryConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired();
            Property(p => p.CNPJ).IsRequired().HasMaxLength(18);
            Property(p => p.City).IsRequired();
            Property(p => p.Code).IsRequired();
           

            HasMany<Analysis>(p => p.ListAnalysis).WithRequired(v => v.Laboratory);
            HasOptional(p => p.User).WithMany().HasForeignKey(p => p.UserId);
        }

    }
}
