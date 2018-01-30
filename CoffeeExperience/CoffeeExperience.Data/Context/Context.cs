using System;
using System.Linq;
using System.Data.Entity;
using CoffeeExperience.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using CoffeeExperience.Data.EntityConfig;

namespace CoffeeExperience.Data.Context
{
    public class Contexto : DbContext, IDisposable 
    {
        public Contexto() : base("ConnectionStringBancoDeDados")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet <Lot> Lot { get; set; }
        public DbSet <Laboratory> Laboratory { get; set; }
        public DbSet <Analysis> Analysis { get; set; }
        public DbSet <Product> Product { get; set; }
        public DbSet <QualityResult> QualityResult { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreationDate") != null)) //fazendo tracker de alguma mudança
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreationDate").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreationDate").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Status") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Status").CurrentValue = EnmStatus.Enabled;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));
            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

           
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new LotConfiguration());
            modelBuilder.Configurations.Add(new LaboratoryConfiguration());
            modelBuilder.Configurations.Add(new AnalysisConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new QualityResultConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

}
