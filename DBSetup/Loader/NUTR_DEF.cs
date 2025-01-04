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

public class NUTR_DEF : DbLoader<NutrientDefinition, NutrientDefinitionDto>
{
    protected override string Filename => "NUTR_DEF.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => new { nd.NutrDesc, nd.Units })
            .IsUnique();
        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => new { nd.TagName, nd.Units })
            .HasFilter("\"Tagname\" IS NOT NULL")
            .IsUnique();
        modelBuilder.Entity<NutrientDefinition>()
            .HasIndex(nd => nd.SR_Order)
            .IsUnique();
    }

    protected override Task<NutrientDefinition?> ParseDtoRecordAsync(DbContext context, NutrientDefinitionDto record)
    {
        var item = new NutrientDefinition
        {
            NutrientDefinitionId = record.Nutr_No,
            Units = record.Units,
            TagName = record.Tagname,
            NutrDesc = record.NutrDesc,
            Num_Dec = record.Num_Dec[0],
            SR_Order = int.Parse(record.SR_Order),
        };

        return Task.FromResult(item)!;
    }
}
