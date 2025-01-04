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

public class DATA_SRC : DbLoader<DataSource, DataSourceDto>
{
    protected override string Filename => "DATA_SRC.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Authors);
        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Title);
        modelBuilder.Entity<DataSource>()
            .HasIndex(lld => lld.Journal);
    }

    protected override Task<DataSource?> ParseDtoRecordAsync(DbContext context, DataSourceDto record)
    {
        var item = new DataSource
        {
            DataSourceId = record.DataSrc_ID,
            Authors = record.Authors,
            Title = record.Title,
            Year = record.Year,
            Journal = record.Journal,
            Vol_City = record.Vol_City,
            Issue_State = record.Issue_State,
            Start_Page = record.Start_Page,
            End_Page = record.End_Page,
        };

        return Task.FromResult(item)!;
    }
}
