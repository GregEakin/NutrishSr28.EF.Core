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

public static class FOOD_DES
{
    public static readonly string Filename = "../../../../data/FOOD_DES.txt";

    public static async Task ParseFileAsync(DbContext context)
    {
        using var reader = new StreamReader(Filename);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "^",
            HasHeaderRecord = false,
            BadDataFound = null,
            MissingFieldFound = null,
            // CultureInfo = { CultureInfo.InvariantCulture }
        };

        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<FoodDescriptionCsv>();
        foreach (var record in records)
        {
            var item = await ParseFoodDescriptionAsync(context, record);
            // Console.WriteLine(item.NDB_No + " " + item.FoodGroupCode + " " + item.Shrt_Desc);
        }
    }

    private static async Task<FoodDescription> ParseFoodDescriptionAsync(DbContext context, FoodDescriptionCsv record)
    {
        var foodGroup = await context.FindAsync<FoodGroupDescription>(record.FdGrp_Cd.Substring(1, record.FdGrp_Cd.Length - 2));
        if (foodGroup == null)
        {
            Console.WriteLine($"FoodGroup {record.FdGrp_Cd} not found!");
            return new FoodDescription();
        }

        var item = new FoodDescription
        {
            NDB_No = record.NDB_No.Substring(1, record.NDB_No.Length - 2),
            FoodGroupCode = foodGroup,
            Long_Desc = record.Long_Desc.Substring(1, record.Long_Desc.Length - 2),
            Shrt_Desc = record.Shrt_Desc.Substring(1, record.Shrt_Desc.Length - 2),
            ComName = record.ComName?.Substring(1, record.ComName.Length - 2),
            ManufacName = record.ManufacName?.Substring(1, record.ManufacName.Length - 2),
            Survey = record.Survey[1],
            Ref_desc = record.Ref_desc?.Substring(1, record.Ref_desc.Length - 2),
            Refuse = record.Refuse,
            SciName = record.SciName?.Substring(1, record.SciName.Length - 2),
            N_Factor = record.N_Factor,
            Pro_Factor = record.Pro_Factor,
            Fat_Factor = record.Fat_Factor,
            CHO_Factor = record.CHO_Factor,
        };

        foodGroup.FoodDescriptions.Add(item);
        await context.SaveChangesAsync();
        return item;
    }
}

#nullable disable

[Table("FOOD_DES")]
[Comment("This file contains long and short descriptions and food group designators for all food items, " +
         "along with common names, manufacturer name, scientific name, percentage and description of refuse, and " +
         "factors used for calculating protein and kilocalories, if applicable. Items used in the " +
         "FNDDS are also identified by value of 'Y' in the Survey field. ")]
public class FoodDescription
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [MaxLength(5)]
    [Column("NDB_No")]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item.  If this field is defined as numeric, the leading zero will be lost.")]
    public string NDB_No { get; set; }

    [ForeignKey("FdGrp_Cd")]
    [Required]
    [Comment("4-digit code indicating food group to which a food item belongs.")]
    public FoodGroupDescription FoodGroupCode { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("Long_Desc")]
    [Comment("200-character description of food item.")]
    public string Long_Desc { get; set; }

    [Required]
    [MaxLength(60)]
    [Column("Shrt_Desc")]
    [Comment("60-character abbreviated description of food item. " +
             "Generated from the 200-character description using " +
             "abbreviations in Appendix A. If short description is " +
             "longer than 60 characters, additional abbreviations " +
             "are made. ")]
    public string Shrt_Desc { get; set; }

    [Comment("Other names commonly used to describe a food, " +
             "including local or regional names for various foods, " +
             "for example, 'soda' or 'pop' for 'carbonated beverages.'")]
    [MaxLength(100)]
    public string? ComName { get; set; }

    [Comment("Indicates the company that manufactured the product, when appropriate.")]
    [MaxLength(65)]
    public string? ManufacName{ get; set; }

    [Comment("Indicates if the food item is used in the USDA Food " +
             "and Nutrient Database for Dietary Studies (FNDDS) " +
             "and thus has a complete nutrient profile for the 65 " +
             "FNDDS nutrients.")]
    public char? Survey { get; set; }

    [Comment("Description of inedible parts of a food item (refuse), such as seeds or bone.")]
    [MaxLength(135)]
    public string? Ref_desc { get; set; }

    [Comment("Percentage of refuse.")]
    public decimal? Refuse { get; set; }

    [Comment("Scientific name of the food item. Given for the least " +
             "processed form of the food (usually raw), if applicable.")]
    [MaxLength(65)]
    public string? SciName { get; set; }

    [Comment("Factor for converting nitrogen to protein.")]
    public decimal? N_Factor { get; set; }

    [Comment("Factor for calculating calories from protein.")]
    public decimal? Pro_Factor { get; set; }

    [Comment("Factor for calculating calories from fat.")]
    public decimal? Fat_Factor { get; set; }

    [Comment("Factor for calculating calories from carbohydrate.")]
    public decimal? CHO_Factor { get; set; }
}

public class FoodDescriptionCsv
{
    public string NDB_No { get; set; }
    public string FdGrp_Cd { get; set; }
    public string Long_Desc { get; set; }
    public string Shrt_Desc { get; set; }
    public string ComName { get; set; }
    public string ManufacName { get; set; }
    public string Survey { get; set; }
    public string Ref_desc { get; set; }
    public int? Refuse { get; set; }
    public string SciName { get; set; }
    public decimal? N_Factor { get; set; }
    public decimal? Pro_Factor { get; set; }
    public decimal? Fat_Factor { get; set; }
    public decimal? CHO_Factor { get; set; }
}