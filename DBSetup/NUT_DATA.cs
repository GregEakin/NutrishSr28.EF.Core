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

public static class NUT_DATA
{
    private static readonly string Filename = "../../../../data/NUT_DATA.txt";

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

        await foreach (var record in csv.GetRecordsAsync<NutrientDataDto>())
        {
            var item = await ParseDtoRecordAsync(context, record);
            if (item == null)
                continue;

            context.Add(item);
            // Console.WriteLine(item.FoodDescriptionId + " " + item.NutrientDefinitionId);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Nutrient Data done!");
    }

    private static async Task<NutrientData?> ParseDtoRecordAsync(DbContext context, NutrientDataDto record)
    {
        var foodDescription = await context.FindAsync<FoodDescription>(record.NDB_No);
        if (foodDescription == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(FoodDescription), record.NDB_No);
            return null;
        }

        var foodDescriptionRef = record.Ref_NDB_No == null ? null : await context.FindAsync<FoodDescription>(record.Ref_NDB_No);

        var nutrientDefinition = await context.FindAsync<NutrientDefinition>(record.Nutr_No);
        if (nutrientDefinition == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(NutrientDefinition), record.Nutr_No);
            return null;
        }

;        var sourceCode = await context.FindAsync<SourceCode>(int.Parse(record.Src_Cd));
        if (sourceCode == null)
        {
            Console.WriteLine("Can't find {0} {1} {2}", nameof(NutrientData), nameof(SourceCode), record.Src_Cd);
            return null;
        }

        var derivationCode = record.Deriv_Cd == null ? null : await context.FindAsync<DerivationCode>(record.Deriv_Cd);

        var item = new NutrientData
        {
            FoodDescription = foodDescription,
            NutrientDefinition = nutrientDefinition,
            Nutr_Val = record.Nutr_Val,
            Num_Data_Pts = record.Num_Data_Pts,
            Std_Error = record.Std_Error,
            SourceCode = sourceCode,
            DerivationCode = derivationCode,
            FoodDescriptionRef = foodDescriptionRef,
            Add_Nutr_Mark = record.Add_Nutr_Mark,
            Num_Studies = record.Num_Studies,
            Min = record.Min,
            Max = record.Max,
            DF = record.DF,
            Low_EB = record.Low_EB,
            Up_EB = record.Up_EB,
            Stat_cmt = record.Stat_cmt,
            AddMod_Date = record.AddMod_Date,
            CC = record.CC,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("NUT_DATA", Schema = "SR28")]
[PrimaryKey(nameof(FoodDescriptionId), nameof(NutrientDefinitionId))]
[Comment("This file contains the nutrient values and information about the values, including expanded statistical information.")]
public class NutrientData
{
    [Key]
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Required]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item.  " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Key]
    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Required]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string NutrientDefinitionId { get; set; }

    [Column("Nutr_Val")]
    [Precision(10, 3)]
    [Required]
    [Comment("Amount in 100 grams, edible portion.")] 
    public Decimal Nutr_Val { get; set; }

    [Column("Num_Data_Pts")]
    [Precision(5, 0)]
    [Required]
    [Comment("Number of data points is the number of analyses used to calculate the nutrient value. " +
             "If the number of data points is 0, the value was calculated or imputed.")]
    public Decimal Num_Data_Pts { get; set; }
    
    [Column("Std_Error")]
    [Precision(8, 3)]
    [Comment("Standard error of the mean. Null if cannot be calculated. The standard error is also " +
             "not given if the number of data points is less than three.")]
    public Decimal? Std_Error { get; set; }
    
    [Column("Src_Cd")]
    [Required]
    [Comment("Code indicating type of data.")]
    public int SourceCodeId { get; set; }
    
    [Column("Deriv_Cd", TypeName = "nchar(4)")]
    [Comment("Data Derivation Code giving specific information on how the value is determined. " +
             "This field is populated only for items added or updated starting with SR14. " +
             "This field may not be populated if older records were used in the calculation of the mean value.")]
    public string? DerivationCodeId { get; set; }
    
    [Column("Ref_NDB_No")]
    [MaxLength(5)]
    [Comment("NDB number of the item used to calculate a missing value. Populated only for items added or updated starting with SR14.")]
    public string? FoodDescriptionRefId { get; set; }
    
    [Column("Add_Nutr_Mark")]
    [MaxLength(1)]
    [Comment("Indicates a vitamin or mineral added for fortification or enrichment. " +
             "This field is populated for ready-to eat breakfast cereals and many brand-name hot cereals in food group 08.")]
    public string? Add_Nutr_Mark { get; set; }
    
    [Column("Num_Studies")]
    [Precision(2, 0)]
    [Comment("Number of studies.")]
    public Decimal? Num_Studies { get; set; }
    
    [Column("Min")]
    [Precision(10, 3)]
    [Comment("Minimum value.")]
    public Decimal? Min { get; set; }
    
    [Column("Max")]
    [Precision(10, 3)]
    [Comment("Maximum value.")]
    public Decimal? Max { get; set; }
    
    [Column("DF")]
    [Precision(4, 0)]
    [Comment("Degrees of freedom.")]
    public Decimal? DF { get; set; }
    
    [Column("Low_EB")]
    [Precision(10, 3)]
    [Comment("Lower 95% error bound.")]
    public Decimal? Low_EB { get; set; }
    
    [Column("Up_EB")]
    [Precision(10, 3)]
    [Comment("Upper 95% error bound.")]
    public Decimal? Up_EB { get; set; }
    
    [Column("Stat_cmt")]
    [MaxLength(10)]
    [Comment("Statistical comments.")]
    public string? Stat_cmt { get; set; }
    
    [Column("AddMod_Date")]
    [MaxLength(10)]
    [Comment("Indicates when a value was either added to the database or last modified.")]
    public string? AddMod_Date { get; set; }
    
    [Column("CC")]
    [MaxLength(1)]
    [Comment("Confidence Code indicating data quality, based on evaluation of sample plan, " +
             "sample handling, analytical method, analytical quality control, and number of " +
             "samples analysed. Not included in this release, but is planned for future releases.")]
    public string? CC { get; set; }

    //-----------------------------------------------
    //Relationships
    public FoodDescription FoodDescription { get; set; }
    public FoodDescription? FoodDescriptionRef { get; set; }
    public FootnoteN Footnote { get; set; }
    // public ICollection<DataSource> DataSources { get; set; } = [];
    public NutrientDefinition NutrientDefinition { get; set; }
    public SourceCode SourceCode { get; set; }
    public DerivationCode DerivationCode { get; set; }
}

public class NutrientDataDto
{
    public string NDB_No { get; set; }
    public string Nutr_No { get; set; }
    public Decimal Nutr_Val { get; set; }
    public Decimal Num_Data_Pts { get; set; }
    public Decimal? Std_Error { get; set; }
    public string Src_Cd { get; set; }
    public string? Deriv_Cd { get; set; }
    public string? Ref_NDB_No { get; set; }
    public string? Add_Nutr_Mark { get; set; }
    public Decimal? Num_Studies { get; set; }
    public Decimal? Min { get; set; }
    public Decimal? Max { get; set; }
    public Decimal? DF { get; set; }
    public Decimal? Low_EB { get; set; }
    public Decimal? Up_EB { get; set; }
    public string? Stat_cmt { get; set; }
    public string? AddMod_Date { get; set; }
    public string? CC { get; set; }
}
