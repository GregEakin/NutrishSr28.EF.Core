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

public class EfCoreContext : DbContext
{
    public DbSet<FoodGroupDescription> FoodGroups { get; set; }

    public EfCoreContext()
    {
    }

    public EfCoreContext(DbContextOptions<EfCoreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodDescription>()
            .Property(b => b.Refuse)
            .HasPrecision(2, 0);

        modelBuilder.Entity<FoodDescription>()
            .Property(b => b.N_Factor)
            .HasPrecision(4, 2);
        modelBuilder.Entity<FoodDescription>()
            .Property(b => b.Pro_Factor)
            .HasPrecision(4, 2);
        modelBuilder.Entity<FoodDescription>()
            .Property(b => b.Fat_Factor)
            .HasPrecision(4, 2);
        modelBuilder.Entity<FoodDescription>()
            .Property(b => b.CHO_Factor)
            .HasPrecision(4, 2);
    }

    private const string ConnectionString = "Host=vim3.lab.eakin.wtf;Database=SR28;Username=docker;Password=secret";
    // => options.UseSqlite($"Data Source={DbPath}");
    // => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbCon;");

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options
            .UseNpgsql(ConnectionString)
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging();
}
