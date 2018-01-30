using System.Data.Entity.ModelConfiguration;
using CoffeeExperience.Domain.Entities;

namespace CoffeeExperience.Data.EntityConfig
{
    public class QualityResultConfiguration : EntityTypeConfiguration<QualityResult>
    {
        public QualityResultConfiguration()
        {
            HasKey(p => p.Id);
            Property(p => p.Type).IsRequired();
            Property(p => p.Observation).IsOptional();
            Property(p => p.QuebrouXicara).IsRequired();

            HasRequired(p => p.Analysis).WithMany().HasForeignKey(p => p.AnalysisId);
        }
    }
}
