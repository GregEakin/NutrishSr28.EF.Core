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

public class LANGDESC : DbLoader<LanguaLDescription, LangualDescriptionDto>
{
    protected override string Filename => "LANGDESC.txt";

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override Task<LanguaLDescription?> ParseDtoRecordAsync(DbContext context, LangualDescriptionDto record)
    {
        var item = new LanguaLDescription
        {
            LangualDescriptionId = record.Factor_Code,
            Description = record.Description,
        };

        return Task.FromResult(item)!;
    }
}
