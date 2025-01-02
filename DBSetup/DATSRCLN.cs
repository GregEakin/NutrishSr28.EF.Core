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
            var item = ParseDataSourceLink(record);
            context.Add(item);
            Console.WriteLine(item.FoodDescriptionId + " " + item.NutrientDefinitionId);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Source Code done!");
    }

    private static DataSourceLink ParseDataSourceLink(DataSourceLinkDto record)
    {
        var item = new DataSourceLink
        {
            FoodDescriptionId = record.NDB_No,
            NutrientDefinitionId = record.Nutr_No,
            DataSourceId = record.DataSrc_ID,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("DATSRCLN")]
[Comment("This file is used to link the Nutrient Data file with the Sources of Data table. It is needed to resolve the many-to-many relationship between the two tables.")]
public class DataSourceLink
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(5)]
    [Column("NDB_No")]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    public string FoodDescriptionId { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(3)]
    [Column("Nutr_No")]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    public string NutrientDefinitionId { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(6)]
    [Column("DataSrc_ID")]
    [Comment("Unique ID identifying the reference/source.")]
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
