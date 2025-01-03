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

public static class FOOTNOTE
{
    private static readonly string Filename = "../../../../data/FOOTNOTE.txt";

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

        await foreach (var record in csv.GetRecordsAsync<FootnoteDto>())
        {
            var item = await ParseDtoRecordAsync(context, record);
            if (item == null)
                continue;

            context.Add(item);
            // Console.WriteLine(item.FoodDescriptionId + " " + item.Footnt_No);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Footnote done!");
    }

    private static async Task<Footnote?> ParseDtoRecordAsync(DbContext context, FootnoteDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(Footnote), nameof(FoodDescription), record.NDB_No);
            return null;
        }

        var nutrientDefinition = await context.FindAsync<NutrientDefinition>(record.Nutr_No);
        if (record.Nutr_No != null && nutrientDefinition == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(Footnote), nameof(NutrientDefinition), record.Nutr_No);
            return null;
        }

        var item = new Footnote
        {
            FoodDescription = foodDescription,
            Footnt_No = record.Footnt_No,
            Footnt_Typ = record.Footnt_Typ[0],
            NutrientDefinition = nutrientDefinition,
            Footnt_Txt = record.Footnt_Txt,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("FOOTNOTE", Schema = "SR28")]
[Comment("This file contains additional information about the food item, household weight, and nutrient value.")]
public class Footnote
{
    [Key]
    public int FootnoteId { get; set; }

    [Required]
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    public string FoodDescriptionId { get; set; }

    [Column("Footnt_No")]
    [MaxLength(4)]
    [Required]
    [Comment("Sequence number. If a given footnote applies to more than one nutrient number, " +
             "the same footnote number is used. As a result, this file cannot be indexed " +
             "and there is no primary key. ")]
    public string Footnt_No { get; set; }

    [Column("Footnt_Typ")]
    [Required]
    [Comment("Type of footnote: " +
             "D = footnote adding information to the food description;  " +
             "M = footnote adding information to measure description;  " +
             "N = footnote providing additional information on a nutrient value. " +
             "If the Footnt_typ = N, the Nutr_No will also be filled in.")]
    public char Footnt_Typ { get; set; }

    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Comment("Unique 3-digit identifier code for a nutrient to which footnote applies.")]
    public string? NutrientDefinitionId { get; set; }

    [Column("Footnt_Txt")]
    [MaxLength(200)]
    [Required]
    [Comment("Footnote text.")]
    public string Footnt_Txt { get; set; }

    //-----------------------------------------------
    //Relationships
    public FoodDescription FoodDescription { get; set; }
    public NutrientData NutrientData { get; set; }
    public NutrientDefinition NutrientDefinition { get; set; }

}

public class FootnoteDto
{
    public string NDB_No { get; set; }
    public string Footnt_No { get; set; }
    public string Footnt_Typ { get; set; }
    public string? Nutr_No { get; set; }
    public string Footnt_Txt { get; set; }
}
