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

public static class SRC_CD
{
    private static readonly string Filename = "../../../../data/SRC_CD.txt";

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

        await foreach (var record in csv.GetRecordsAsync<SrcCdDto>())
        {
            var item = ParseSourceCode(record);
            context.Add(item);
            // Console.WriteLine(item.SourceCodeId + " " + item.SourceCodeDescription);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Source Code done!");
    }

    private static SourceCode ParseSourceCode(SrcCdDto record)
    {
        var item = new SourceCode
        {
            SourceCodeId = record.Src_Cd,
            SourceCodeDescription = record.SrcCd_Desc,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("SRC")]
[Comment("This file contains codes indicating the type of data (analytical, calculated, assumed zero, " +
         "and so on) in the Nutrient Data file. To improve the usability of the database and to provide " +
         "values for the FNDDS, NDL staff imputed nutrient values for a number of proximate components, " +
         "total dietary fiber, total sugar, and vitamin and mineral values.")]
public class SourceCode
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(2)]
    [Column("Src_Cd")]
    [Comment("A 2-digit code indicating type of data.")]
    public string SourceCodeId { get; set; }

    [Required]
    [MaxLength(60)]
    [Column("SrcCd_Desc")]
    [Comment("Description of source code that identifies the type of nutrient data.")]
    public string SourceCodeDescription { get; set; }

    //-----------------------------------------------
    //Relationships

    public ICollection<NutrientData> NutrientData { get; set; }
}

public class SrcCdDto
{
    public string Src_Cd { get; set; }
    public string SrcCd_Desc { get; set; }
}
