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

internal class LANGUAL_DESC
{
    private static readonly string Filename = "../../../../data/LANGDESC.txt";

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

        await foreach (var record in csv.GetRecordsAsync<LangualFactorsDescriptionDto>())
        {
            var item = ParseFoodDescription(record);
            context.Add(item);
            Console.WriteLine(item.Factor_Code + " " + item.Description);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("LangualFactorsDescription done!");
    }

    private static LangualFactorsDescription ParseFoodDescription(LangualFactorsDescriptionDto record)
    {
        var item = new LangualFactorsDescription
        {
            Factor_Code = record.Factor_Code,
            Description = record.Description,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("LANGDESC")]
[Comment("This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.")]
public class LangualFactorsDescription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(5)]
    [Column("Factor_Code")]
    [Comment("The LanguaL factor from the Thesaurus. Only those " +
             "codes used to factor the foods contained in the " + 
             "LanguaL Factor file are included in this file. ")]
    public string Factor_Code { get; set; }

    [Required]
    [MaxLength(140)]
    [Column("Description")]
    [Comment("The description of the LanguaL Factor Code from the thesaurus. ")]
    public string Description { get; set; }
}

public class LangualFactorsDescriptionDto
{
    public string Factor_Code { get; set; }
    public string Description { get; set; }
}
