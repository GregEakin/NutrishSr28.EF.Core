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

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DBSetup;

public static class DATSRCLN
{
    private static readonly string Filename = "../../../../data/DATSRCLN.txt";

    public static async Task ParseFileAsync(DbContext context)
    {
        using var reader = new StreamReader(Filename);

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

        await foreach (var record in csv.GetRecordsAsync<DataSourceLinkDto>())
        {
            var item = await ParseDtoRecordAsync(context, record);
            if (item == null)
                continue;

            context.Add(item);
            // Console.WriteLine(item.FoodDescriptionId + " " + item.NutrientDefinitionId);
            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Source Code done!");
    }

    private static async Task<DataSourceLink?> ParseDtoRecordAsync(DbContext context, DataSourceLinkDto record)
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

#nullable disable
#pragma warning disable CS8632

[Table("DATSRCLN", Schema = "SR28")]
[PrimaryKey(nameof(FoodDescriptionId), nameof(NutrientDefinitionId), nameof(DataSourceId))]
[Comment("This file is used to link the Nutrient Data file with the Sources of Data table. It is needed to resolve the many-to-many relationship between the two tables.")]
public class DataSourceLink
{
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Required]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Required]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string NutrientDefinitionId { get; set; }

    [Column("DataSrc_ID")]
    [MaxLength(6)]
    [Required]
    [Comment("Unique ID identifying the reference/source.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string DataSourceId { get; set; }

    //-----------------------------------------------
    //Relationships
    public FoodDescription FoodDescription { get; set; }
    public NutrientDefinition NutrientDefinition { get; set; }
    public DataSource DataSource { get; set; }
}

public class DataSourceLinkDto
{
    public string NDB_No { get; set; }
    public string Nutr_No { get; set; }
    public string DataSrc_ID { get; set; }
}