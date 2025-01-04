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

public class DATSRCLN : DbLoader<DataSourceLink, DataSourceLinkDto>
{
    protected override string Filename => "DATSRCLN.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {}

    protected override async Task<DataSourceLink?> ParseDtoRecordAsync(DbContext context, DataSourceLinkDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(DataSourceLink), nameof(FoodDescription), record.NDB_No);
            foodDescription = new FoodDescription
            {
                FoodDescriptionId = record.NDB_No,
                FoodGroupId = "3600",
                Shrt_Desc = $"Deleted {record.NDB_No}",
                Long_Desc = $"Deleted {record.NDB_No}",
            };
        }

        var nutrientDefinition = await context.FindAsync<NutrientDefinition>(record.Nutr_No);
        if (nutrientDefinition == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(DataSourceLink), nameof(NutrientDefinition), record.Nutr_No);
            throw new Exception($"NutrientDefinition {record.Nutr_No} not found!");
        }

        var dataSource = await context.FindAsync<DataSource>(record.DataSrc_ID);
        if (dataSource == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(DataSourceLink), nameof(DataSource), record.DataSrc_ID);
            dataSource = new DataSource
            {
                DataSourceId = record.DataSrc_ID,
                Title = $"Deleted {record.DataSrc_ID}",
            };
        }

        var item = new DataSourceLink
        {
            FoodDescription = foodDescription,
            NutrientDefinition = nutrientDefinition,
            DataSource = dataSource,
        };

        return item;
    }
}
