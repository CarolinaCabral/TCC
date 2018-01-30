using System.Data.Entity.ModelConfiguration;
using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Data.EntityConfig
{
    public class AnalysisConfiguration : EntityTypeConfiguration<Analysis>
    {
        public AnalysisConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Code).IsRequired();
            Property(p => p.Type).IsRequired();

            HasMany<QualityResult>(p => p.ListQualityResult).WithRequired(v => v.Analysis);
            HasRequired(p => p.Lot).WithMany().HasForeignKey(p => p.LotId);
            HasRequired(p => p.Laboratory).WithMany().HasForeignKey(p => p.LaboratoryId);
        }
    }
}
