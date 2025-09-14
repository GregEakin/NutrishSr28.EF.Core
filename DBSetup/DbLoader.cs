// Copyright 2025 Gregory Eakin
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

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBSetup;

public abstract class DbLoader<TData, TDto>
{
    public abstract void OnModelCreating(ModelBuilder modelBuilder);

    public async Task ParseFileAsync(DbContext context, string dir)
    {
        using var reader = new StreamReader(Path.Combine(dir, Filename));

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "^",
            Quote = '~',
            Escape = '$',
            HasHeaderRecord = false,
            BadDataFound = x => throw new Exception($"Bad data: <{x.RawRecord}>"),
            MissingFieldFound = x => throw new Exception($"Missing Filed: <{x.Index}>"),
        };

        using var csv = new CsvReader(reader, config);
        csv.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("");

        const int batchSize = 1000;

        await ParseDataAsync(context, batchSize, csv);
    }

    protected abstract string Filename { get; }

    private async Task ParseDataAsync(DbContext context, int batchSize, CsvReader csv)
    {
        var records = new List<TDto>(batchSize);
        await foreach (var record in csv.GetRecordsAsync<TDto>())
        {
            records.Add(record);
            if (records.Count < batchSize) continue;
            await ProcessRecordsAsync(context, records);
            records.Clear();
        }

        if (records.Count > 0)
        {
            await ProcessRecordsAsync(context, records);
            records.Clear();
        }

        Console.WriteLine("{0} done!", Filename);
    }

    private async Task ProcessRecordsAsync(DbContext context, List<TDto> records)
    {
        foreach (var record in records)
        {
            var item = await ParseDtoRecordAsync(context, record);
            if (item == null)
                continue;

            context.Add(item);
        }

        await context.SaveChangesAsync();
    }

    protected abstract Task<TData?> ParseDtoRecordAsync(DbContext context, TDto record);
}