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

public class SRC_CD : DbLoader<SourceCode, SrcCdDto>
{
    protected override string Filename => "SRC_CD.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SourceCode>()
            .HasIndex(sc => sc.SourceCodeDescription)
            .IsUnique();
    }

    protected override Task<SourceCode?> ParseDtoRecordAsync(DbContext context, SrcCdDto record)
    {
        var item = new SourceCode
        {
            SourceCodeId = int.Parse(record.Src_Cd),
            SourceCodeDescription = record.SrcCd_Desc,
        };

        return Task.FromResult(item)!;
    }
}
