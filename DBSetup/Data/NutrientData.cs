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
using Microsoft.EntityFrameworkCore;

namespace DBSetup.Data;

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
    public decimal Nutr_Val { get; set; }

    [Column("Num_Data_Pts")]
    [Precision(5, 0)]
    [Required]
    [Comment("Number of data points is the number of analyses used to calculate the nutrient value. " +
             "If the number of data points is 0, the value was calculated or imputed.")]
    public decimal Num_Data_Pts { get; set; }

    [Column("Std_Error")]
    [Precision(8, 3)]
    [Comment("Standard error of the mean. Null if cannot be calculated. The standard error is also " +
             "not given if the number of data points is less than three.")]
    public decimal? Std_Error { get; set; }

    [Column("Src_Cd")]
    [Required]
    [Comment("Code indicating type of data.")]
    public int SourceCodeId { get; set; }

    [Column("Deriv_Cd")]
    [MaxLength(4)]
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
    public decimal? Num_Studies { get; set; }

    [Column("Min")]
    [Precision(10, 3)]
    [Comment("Minimum value.")]
    public decimal? Min { get; set; }

    [Column("Max")]
    [Precision(10, 3)]
    [Comment("Maximum value.")]
    public decimal? Max { get; set; }

    [Column("DF")]
    [Precision(4, 0)]
    [Comment("Degrees of freedom.")]
    public decimal? DF { get; set; }

    [Column("Low_EB")]
    [Precision(10, 3)]
    [Comment("Lower 95% error bound.")]
    public decimal? Low_EB { get; set; }

    [Column("Up_EB")]
    [Precision(10, 3)]
    [Comment("Upper 95% error bound.")]
    public decimal? Up_EB { get; set; }

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
    // public FootnoteN Footnote { get; set; }
    // public ICollection<DataSource> DataSources { get; set; } = [];
    public NutrientDefinition NutrientDefinition { get; set; }
    public SourceCode SourceCode { get; set; }
    public DerivationCode DerivationCode { get; set; }
}