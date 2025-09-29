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

using CsvHelper.Configuration;
using DBSetup.Data;
using DBSetup.Loader;
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
    public DbSet<FootnoteD> FootnoteDs { get; set; }
    public DbSet<FootnoteM> FootnoteMs { get; set; }
    public DbSet<FootnoteN> FootnoteNs { get; set; }
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

        new FD_GROUP().OnModelCreating(modelBuilder);
        new SRC_CD().OnModelCreating(modelBuilder);
        new DERIV_CD().OnModelCreating(modelBuilder);
        new LANGUAL().OnModelCreating(modelBuilder);
        new DATA_SRC().OnModelCreating(modelBuilder);
        new NUTR_DEF().OnModelCreating(modelBuilder);
        new FOOD_DES().OnModelCreating(modelBuilder);
        new WEIGHT().OnModelCreating(modelBuilder);
        new FOOTNOTE().OnModelCreating(modelBuilder);
        new NUT_DATA().OnModelCreating(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var environmentVariable = Environment.GetEnvironmentVariable("ConnectionStrings__nutrishdb");
        if (!string.IsNullOrWhiteSpace(environmentVariable))
            options.UseNpgsql(environmentVariable);
    }
}

// public class ETC : IEntityTypeConfiguration<FoodDescription>
// {
//     public void Configure(EntityTypeBuilder<FoodDescription> builder)
//     {
//         throw new NotImplementedException();
//     }
// }
