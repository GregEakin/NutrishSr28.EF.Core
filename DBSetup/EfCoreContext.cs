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

        modelBuilder.Entity<FoodGroup>()
            .HasIndex(fg => fg.FoodGroupName)
            .IsUnique();

        modelBuilder.Entity<SourceCode>()
            .HasIndex(sc => sc.SourceCodeDescription)
            .IsUnique();

        modelBuilder.Entity<DerivationCode>()
            .HasIndex(dc => dc.DerivationCodeDescription)
            .IsUnique();

        modelBuilder.Entity<LanguaLDescription>()
            .HasIndex(lld => lld.Description)
            .IsUnique();

        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Authors);
        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Title);
        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Journal);

        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => new { nd.NutrDesc, nd.Units })
            .IsUnique();
        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => new { nd.TagName, nd.Units })
            .HasFilter(@"Tagname IS NOT NULL")
            .IsUnique();
        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => nd.SR_Order)
            .IsUnique();

        modelBuilder.Entity<FoodDescription>()
            .HasIndex(fd => fd.Long_Desc)
            .IsUnique();
        modelBuilder.Entity<FoodDescription>()
            .HasIndex(fd => fd.Shrt_Desc);

        modelBuilder.Entity<Footnote>()
            .HasDiscriminator(f => f.Footnt_Typ)
            .HasValue<FootnoteD>("D")
            .HasValue<FootnoteM>("M")
            .HasValue<FootnoteN>("N");
        modelBuilder.Entity<Footnote>()
            .HasIndex(f => new { f.FoodDescriptionId, f.Footnt_No })
            .HasFilter(@"Footnt_Typ = 'D'")
            .IsUnique();
        modelBuilder.Entity<Footnote>()
            .HasIndex(f => f.FoodDescriptionId)
            .HasFilter(@"Footnt_Typ = 'M'")
            .IsUnique();
        modelBuilder.Entity<Footnote>()
            .HasIndex(f => new { f.FoodDescriptionId, f.NutrientDefinitionId })
            .HasFilter(@"Footnt_Typ = 'N'")
            .IsUnique();
        modelBuilder.Entity<Footnote>()
            .HasIndex(f => f.Footnt_Txt);
        modelBuilder.Entity<FootnoteD>()
            .HasOne(f => f.FoodDescription)
            .WithMany(fd => fd.Footnotes)
            .HasForeignKey(f => f.FoodDescriptionId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<FootnoteN>()
            .HasOne(f => f.NutrientData)
            .WithOne(fd => fd.Footnote)
            .HasForeignKey<FootnoteN>(f => new { f.FoodDescriptionId, f.NutrientDefinitionId })
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Weight>()
            .HasIndex(w => w.Msre_Desc);

        modelBuilder.Entity<NutrientData>()
            .HasOne(nd => nd.FoodDescription)
            .WithMany(fd => fd.NutrientData)
            .HasForeignKey(nd => nd.FoodDescriptionId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NutrientData>()
            .HasOne(nd => nd.FoodDescriptionRef)
            .WithMany()
            .HasForeignKey(nd => nd.FoodDescriptionRefId)
            .OnDelete(DeleteBehavior.Restrict);

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
