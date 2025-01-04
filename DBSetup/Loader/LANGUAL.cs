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

// LanguaL stands for "Langua aLimentaria" or "language of food".
// See http://www.langual.org

internal class LANGUAL : DbLoader<LanguaLFactor, LangualFactorDto>
{
    protected override string Filename => "LANGUAL.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LanguaLDescription>()
            .HasIndex(lld => lld.Description)
            .IsUnique();
    }

    protected override async Task<LanguaLFactor?> ParseDtoRecordAsync(DbContext context, LangualFactorDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(LanguaLFactor), nameof(FoodDescription), record.NDB_No);
            throw new Exception($"{nameof(LanguaLFactor)} {nameof(FoodDescription)} {record.NDB_No} not found!");
        }

        var langualFactorsDescription = await context.FindAsync<LanguaLDescription>(record.Factor_Code);
        if (langualFactorsDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(LanguaLFactor), nameof(LanguaLDescription), record.Factor_Code);
            throw new Exception($"{nameof(LanguaLFactor)} {nameof(LanguaLDescription)} {record.Factor_Code} not found!");
        }

        var item = new LanguaLFactor
        {
            FoodDescriptionId = foodDescription.FoodDescriptionId,
            LangualDescriptionId = langualFactorsDescription.LangualDescriptionId,
        };

        return item;
    }
}
