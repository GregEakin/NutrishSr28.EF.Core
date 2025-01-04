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

public class NUT_DATA : DbLoader<NutrientData, NutrientDataDto>
{
    protected override string Filename => "NUT_DATA.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
    }

    protected override async Task<NutrientData?> ParseDtoRecordAsync(DbContext context, NutrientDataDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(FoodDescription), record.NDB_No);
            return null;
        }

        var foodDescriptionRef = record.Ref_NDB_No == null ? null : await context.FindAsync<FoodDescription>(record.Ref_NDB_No);

        var nutrientDefinition = await context.FindAsync<NutrientDefinition>(record.Nutr_No);
        if (nutrientDefinition == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(NutrientDefinition), record.Nutr_No);
            return null;
        }

        var sourceCode = await context.FindAsync<SourceCode>(int.Parse(record.Src_Cd));
        if (sourceCode == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(SourceCode), record.Src_Cd);
            return null;
        }

        var derivationCode = record.Deriv_Cd == null ? null : await context.FindAsync<DerivationCode>(record.Deriv_Cd);

        var item = new NutrientData
        {
            FoodDescription = foodDescription,
            NutrientDefinition = nutrientDefinition,
            Nutr_Val = record.Nutr_Val,
            Num_Data_Pts = record.Num_Data_Pts,
            Std_Error = record.Std_Error,
            SourceCode = sourceCode,
            DerivationCode = derivationCode,
            FoodDescriptionRef = foodDescriptionRef,
            Add_Nutr_Mark = record.Add_Nutr_Mark,
            Num_Studies = record.Num_Studies,
            Min = record.Min,
            Max = record.Max,
            DF = record.DF,
            Low_EB = record.Low_EB,
            Up_EB = record.Up_EB,
            Stat_cmt = record.Stat_cmt,
            AddMod_Date = record.AddMod_Date,
            CC = record.CC,
        };

        return item;
    }
}
