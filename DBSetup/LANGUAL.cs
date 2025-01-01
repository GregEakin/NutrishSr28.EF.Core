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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DBSetup;

// LanguaL stands for "Langua aLimentaria" or "language of food".
// See http://www.langual.org

internal class LANGUAL
{
    private static readonly string Filename = "../../../../data/LANGUAL.txt";

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

        await foreach (var record in csv.GetRecordsAsync<LangualFactorDto>())
        {
            var item = await ParseFoodDescriptionAsync(context, record);
            context.Add(item);
            Console.WriteLine(item.NDB_No + " " + item.Factor_Code);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("LangualFactor done!");
    }

    private static async Task<LangualFactor> ParseFoodDescriptionAsync(DbContext context, LangualFactorDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No)
                              ?? throw new Exception($"FoodDescription {record.NDB_No} not found!");

        var langualFactorsDescription = await context.FindAsync<LangualFactorsDescription>(record.FdGrp_Desc)
                                        ?? throw new Exception($"LangualFactorsDescription {record.FdGrp_Desc} not found!");

        var item = new LangualFactor
        {
            NDB_No = foodDescription.NDB_No,
            Factor_Code = langualFactorsDescription.Factor_Code,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("LANGUAL")]
[Comment("This file is a support file to the Food Description file and contains the factors from the LanguaL Thesaurus used to code a particular food.")]
public class LangualFactor
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [ForeignKey("NDB_No")]
    [Column(Order = 0)]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost.")]
    public string NDB_No { get; set; }
    
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [ForeignKey("Factor_Code")]
    [Column(Order = 1)]
    [Comment("The LanguaL factor from the Thesaurus.")]
    public string Factor_Code { get; set; }

    public FoodDescription FoodDescription { get; set; }
    public LangualFactorsDescription LangualFactorsDescription { get; set; }
}

public class LangualFactorDto
{
    public string NDB_No { get; set; }
    public string FdGrp_Desc { get; set; }
}
