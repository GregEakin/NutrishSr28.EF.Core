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

public class FOOD_DES : DbLoader<FoodDescription, FoodDescriptionDto>
{
    protected override string Filename => "FOOD_DES.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodDescription>()
            .HasIndex(fd => fd.Long_Desc)
            .IsUnique();
        modelBuilder.Entity<FoodDescription>()
            .HasIndex(fd => fd.Shrt_Desc);
    }

    protected override async Task<FoodDescription?> ParseDtoRecordAsync(DbContext context, FoodDescriptionDto record)
    {
        var foodGroup = await context.FindAsync<FoodGroup>(record.FdGrp_Cd);
        if (foodGroup == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(FoodDescription), nameof(FoodGroup), record.FdGrp_Cd);
            throw new Exception($"FoodGroup {record.FdGrp_Cd} not found!");
        }

        // Console.WriteLine("FoodDescription {0} {1} {2} --> {3}", record.NDB_No, record.Long_Desc, record.Shrt_Desc, foodGroup.FoodGroupName);

        var item = new FoodDescription
        {
            FoodDescriptionId = record.NDB_No,
            FoodGroup = foodGroup,
            Long_Desc = record.Long_Desc,
            Shrt_Desc = record.Shrt_Desc,
            ComName = record.ComName,
            ManufacName = record.ManufacName,
            Survey = record.Survey?[0],
            Ref_desc = record.Ref_desc,
            Refuse = record.Refuse,
            SciName = record.SciName,
            N_Factor = record.N_Factor,
            Pro_Factor = record.Pro_Factor,
            Fat_Factor = record.Fat_Factor,
            CHO_Factor = record.CHO_Factor,
        };

        // foodGroup.FoodDescriptions.Add(item);
        await context.SaveChangesAsync();
        return item;
    }
}
