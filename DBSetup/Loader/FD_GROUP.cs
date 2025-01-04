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

public class FD_GROUP : DbLoader<FoodGroup, FoodGroupDto>
{
    protected override string Filename => "FD_GROUP.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodGroup>()
            .HasIndex(fg => fg.FoodGroupName)
            .IsUnique();
    }

    protected override Task<FoodGroup?> ParseDtoRecordAsync(DbContext context, FoodGroupDto record)
    {
        var item = new FoodGroup
        {
            FoodGroupId = record.FdGrp_Cd,
            FoodGroupName = record.FdGrp_Desc,
        };

        return Task.FromResult(item)!;
    }
}
