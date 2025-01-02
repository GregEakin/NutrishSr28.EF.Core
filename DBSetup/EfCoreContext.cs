// Copyright 2024 Gregory Eakin
// 
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
// 
//       http://www.apache.org/licenses/LICENSE-2.0
// 
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using Microsoft.EntityFrameworkCore;

namespace DBSetup;

public class EfCoreContext : DbContext
{
    public DbSet<FoodGroup> FoodGroups { get; set; }
    public DbSet<FoodDescription> FoodDescriptions { get; set; }
    public DbSet<LanguaLDescription> LanguaLDescriptions { get; set; }
    public DbSet<SourceCode> SourceCodes { get; set; }
    public DbSet<DerivationCode> DerivationCodes { get; set; }
    public DbSet<DataSource> DataSources { get; set; }
    public DbSet<NutrientDefinition> NutrientDefinitions { get; set; }
    public DbSet<Weight> Weights { get; set; }
    public DbSet<Footnote> Footnotes { get; set; }
    public DbSet<NutrientData> NutrientData { get; set; }

    public EfCoreContext()
    {
    }

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Configurations.Add(new MyEntityConfiguration())
        // var entity = new ETC();
        // modelBuilder.ApplyConfiguration(entity);

        modelBuilder.Entity<LanguaLFactor>()
            .HasKey(lf => new { lf.FoodDescriptionId, lf.LangualDescriptionId });

        modelBuilder.Entity<Weight>()
            .HasKey(w => new { w.FoodDescriptionId , w.Seq });

        modelBuilder.Entity<NutrientData>()
            .HasKey(nd => new { nd.FoodDescriptionId, nd.NutrientDefinitionId });

        modelBuilder.Entity<DataSourceLink>()
            .HasKey(dsl => new { dsl.FoodDescriptionId, dsl.NutrientDefinitionId, dsl.DataSourceId });

        base.OnModelCreating(modelBuilder);
    }

    // private const string ConnectionString = "Host=vim3.lab.eakin.wtf;Database=SR28;Username=docker;Password=secret";
    // => options.UseSqlite($"Data Source={DbPath}");
    private const string ConnectionString = @"Server=(localdb)\SR28;Database=Nutrish;";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseSqlServer(ConnectionString);
            // .LogTo(Console.WriteLine)
            // .EnableSensitiveDataLogging();
}

// public class ETC : IEntityTypeConfiguration<FoodDescription>
// {
//     public void Configure(EntityTypeBuilder<FoodDescription> builder)
//     {
//         throw new NotImplementedException();
//     }
// }
