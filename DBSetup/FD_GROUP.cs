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

public static class FD_GROUP
{
    private static readonly string Filename = "../../../../data/FD_GROUP.txt";

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

        await foreach (var record in csv.GetRecordsAsync<FoodGroupDescriptionDto>())
        {
            var item = ParseFoodDescription(record);
            context.Add(item);
            Console.WriteLine(item.FdGrp_Cd + " " + item.FdGrp_Desc);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("FoodGroupDescription done!");
    }

    private static FoodGroupDescription ParseFoodDescription(FoodGroupDescriptionDto record)
    {
        var item = new FoodGroupDescription
        {
            FdGrp_Cd = record.FdGrp_Cd,
            FdGrp_Desc = record.FdGrp_Desc,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("FD_GROUP")]
[Comment("Contains a list of food groups used in SR28 and their descriptions.")]
public class FoodGroupDescription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(4)]
    [Column("FdGrp_Cd")]
    [Comment("4-digit code identifying a food group. Only the first 2 ndigits are currently assigned. " +
             "In the future, the last 2 digits may be used. Codes may not be consecutive.")]
    public string FdGrp_Cd { get; set; }

    [Required]
    [MaxLength(60)]
    [Column("FdGrp_Desc")]
    [Comment("Name of food group.")]
    public string FdGrp_Desc { get; set; }

    //-----------------------------------------------
    //Relationships
    public ICollection<FoodDescription> FoodDescriptions { get; set; } = [];
}

public class FoodGroupDescriptionDto
{
    public string FdGrp_Cd { get; set; }
    public string FdGrp_Desc { get; set; }
}
