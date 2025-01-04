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

public static class NUTR_DEF
{
    private static readonly string Filename = "../../../../data/NUTR_DEF.txt";

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

        await foreach (var record in csv.GetRecordsAsync<NutrientDefinitionDto>())
        {
            var item = ParseDtoRecord(record);
            context.Add(item);
            // Console.WriteLine(item.NutrientDefinitionId + " " + item.NutrDesc);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Nutrient Definition done!");
    }

    private static NutrientDefinition ParseDtoRecord(NutrientDefinitionDto record)
    {
        var item = new NutrientDefinition
        {
            NutrientDefinitionId = record.Nutr_No,
            Units = record.Units,
            TagName = record.Tagname,
            NutrDesc = record.NutrDesc,
            Num_Dec = record.Num_Dec[0],
            SR_Order = int.Parse(record.SR_Order),
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("NUTR_DEF", Schema = "SR28")]
[Comment("This file is a support file to the Nutrient Data file. " +
         "It provides the 3-digit nutrient code, unit of measure, INFOODS tagname, and description.")]
public class NutrientDefinition
{
    [Key]
    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Required]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string NutrientDefinitionId { get; set; }

    [Column("Units")]
    [MaxLength(7)]
    [Required]
    [Comment("Units of measure (mg, g, μg, and so on).")]
    public string Units { get; set; }

    [Column("Tagname")]
    [MaxLength(20)]
    [Comment("International Network of Food Data Systems (INFOODS) Tagnames. " +
             "A unique abbreviation for a nutrient/food component developed " +
             "by INFOODS to aid in the interchange of data.")]
    public string? TagName { get; set; }

    [Column("NutrDesc")]
    [MaxLength(60)]
    [Required]
    [Comment("Name of nutrient/food component.")]
    public string NutrDesc { get; set; }

    [Column("Num_Dec")]
    [Required]
    [Comment("Number of decimal places to which a nutrient value is rounded.")]
    public char Num_Dec { get; set; }

    [Column("SR_Order")]
    [MaxLength(6)]
    [Required]
    [Comment("Used to sort nutrient records in the same order as various reports produced from SR.")]
    public int SR_Order { get; set; }

    //-----------------------------------------------
    //Relationships
    public ICollection<DataSourceLink> DataSourceLinks { get; set; }
    public ICollection<NutrientData> NutrientData { get; set; }
}

public class NutrientDefinitionDto
{
    public string Nutr_No { get; set; }
    public string Units { get; set; }
    public string? Tagname { get; set; }
    public string NutrDesc { get; set; }
    public string Num_Dec { get; set; }
    public string SR_Order { get; set; }
}
