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

public static class FD_GROUP
{
    public static readonly string Filename = "../../../../data/FD_GROUP.txt";

    public static void ParseFile(DbContext context)
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
        var records = csv.GetRecords<FoodGroupDescriptionCsv>();
        foreach (var record in records)
        {
            var item = ParseFoodDescription(record);
            context.Add(item);
            Console.WriteLine(item.FdGrp_Cd + " " + item.FdGrp_Desc);
        }

        context.SaveChanges();
    }

    private static FoodGroupDescription ParseFoodDescription(FoodGroupDescriptionCsv record)
    {
        var item = new FoodGroupDescription
        {
            FdGrp_Cd = record.FdGrp_Cd.Substring(1, record.FdGrp_Cd.Length - 2),
            FdGrp_Desc = record.FdGrp_Desc.Substring(1, record.FdGrp_Desc.Length - 2)
        };
        return item;
    }
}

#nullable disable

[Table("FD_GROUP")]
[Comment("Contains a list of food groups used in SR28 and their descriptions.")]
public class FoodGroupDescription
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(4)]
    [Column("FdGrp_Cd")]
    [Comment("4-digit code identifying a food group. Only the first 2 ndigits are currently assigned. " +
             "In the future, the last 2 digits may be used. Codes may not be consecutive.")]
    public string FdGrp_Cd { get; set; } = string.Empty;

    [Required]
    [MaxLength(60)]
    [Column("FdGrp_Desc")]
    [Comment("Name of food group.")]
    public string FdGrp_Desc { get; set; } = string.Empty;

    //-----------------------------------------------
    //Relationships
    public ICollection<FoodDescription> FoodDescriptions { get; set; } = [];
}

public class FoodGroupDescriptionCsv
{
    public string FdGrp_Cd { get; set; }
    public string FdGrp_Desc { get; set; }
}
