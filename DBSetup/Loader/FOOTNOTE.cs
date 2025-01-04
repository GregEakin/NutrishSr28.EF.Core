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

using DBSetup.Data;
using DBSetup.Dto;
using Microsoft.EntityFrameworkCore;

namespace DBSetup.Loader;

public class FOOTNOTE : DbLoader<Footnote, FootnoteDto>
{
    protected override string Filename => "FOOTNOTE.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Footnote>()
            .HasIndex(f => f.Footnt_Txt);
        modelBuilder.Entity<Footnote>()
            .HasDiscriminator(f => f.Footnt_Typ)
            .HasValue<FootnoteD>("D")
            .HasValue<FootnoteM>("M")
            .HasValue<FootnoteN>("N");
        modelBuilder.Entity<FootnoteD>()
            .HasIndex(f => new { f.FoodDescriptionId, f.Footnt_No })
            .HasFilter("\"Footnt_Typ\" = 'D'")
            .IsUnique();
        modelBuilder.Entity<FootnoteM>()
            .HasIndex(f => f.FoodDescriptionId)
            .HasFilter("\"Footnt_Typ\" = 'M'")
            .IsUnique();
        modelBuilder.Entity<FootnoteN>()
            .HasIndex(f => new { f.FoodDescriptionId, f.NutrientDefinitionId })
            .HasFilter("\"Footnt_Typ\" = 'N'")
            .IsUnique();

        // modelBuilder.Entity<FootnoteD>()
        //     .HasOne(f => f.FoodDescription)
        //     .WithMany(fd => fd.Footnotes)
        //     .HasForeignKey(f => f.FoodDescriptionId)
        //     .OnDelete(DeleteBehavior.Restrict);

        // modelBuilder.Entity<FootnoteN>()
        //     .HasOne(f => f.NutrientData)
        //     .WithOne(fd => fd.Footnote)
        //     .HasForeignKey<FootnoteN>(f => new { f.FoodDescriptionId, f.NutrientDefinitionId })
        //     .OnDelete(DeleteBehavior.Restrict);
    }

    protected override Task<Footnote?> ParseDtoRecordAsync(DbContext context, FootnoteDto record)
    {
        // var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        // if (foodDescription == null)
        // {
        //     Console.WriteLine("Can't find {0} {1} {2}", nameof(Footnote), nameof(FoodDescription), record.NDB_No);
        //     return null;
        // }
        //
        // var nutrientDefinition = await context.FindAsync<NutrientDefinition>(record.Nutr_No);
        // if (record.Nutr_No != null && nutrientDefinition == null)
        // {
        //     Console.WriteLine("Can't find {0} {1} {2}", nameof(Footnote), nameof(NutrientDefinition), record.Nutr_No);
        //     return null;
        // }

        var item = new Footnote
        {
            FoodDescriptionId = record.NDB_No,
            Footnt_No = record.Footnt_No,
            Footnt_Typ = record.Footnt_Typ,
            NutrientDefinitionId = record.Nutr_No,
            Footnt_Txt = record.Footnt_Txt,
        };

        return Task.FromResult(item)!;
    }
}
