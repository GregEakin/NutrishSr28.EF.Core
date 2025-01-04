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

public class WEIGHT : DbLoader<Weight, WeightDto>
{
    protected override string Filename => "WEIGHT.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Weight>()
            .HasIndex(w => w.Msre_Desc);
    }

    protected override async Task<Weight?> ParseDtoRecordAsync(DbContext context, WeightDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(Weight), nameof(FoodDescription), record.NDB_No);
            throw new Exception($"{nameof(Weight)} {nameof(FoodDescription)} {record.NDB_No} not found!");
        }

        var item = new Weight
        {
            FoodDescription = foodDescription,
            Seq = int.Parse(record.Seq),
            Amount = (float)record.Amount,
            Msre_Desc = record.Msre_Desc,
            Gm_Wgt = (float)record.Gm_Wgt,
            Num_Data_Pts = record.Num_Data_Pts.HasValue ? (int?)record.Num_Data_Pts.Value : null,
            Std_Dev = (float?)record.Std_Dev,
        };

        return item;
    }
}
