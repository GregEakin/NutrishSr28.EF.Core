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

[Table("FOOD_DES", Schema = "SR28")]
[Comment("This file contains long and short descriptions and food group designators for all food items, " +
         "along with common names, manufacturer name, scientific name, percentage and description of refuse, and " +
         "factors used for calculating protein and kilocalories, if applicable. Items used in the " +
         "FNDDS are also identified by value of 'Y' in the Survey field. ")]
public class FoodDescription
{
    [Key]
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Column("FdGrp_Cd", TypeName = "nchar(4)")]
    [Required]
    [Comment("4-digit code indicating food group to which a food item belongs.")]
    public string FoodGroupId { get; set; }

    [Column("Long_Desc")]
    [MaxLength(200)]
    [Required]
    [Comment("200-character description of food item.")]
    public string Long_Desc { get; set; }

    [Column("Shrt_Desc")]
    [MaxLength(60)]
    [Required]
    [Comment("60-character abbreviated description of food item. " +
             "Generated from the 200-character description using " +
             "abbreviations in Appendix A. If short description is " +
             "longer than 60 characters, additional abbreviations " +
             "are made. ")]
    public string Shrt_Desc { get; set; }

    [MaxLength(100)]
    [Comment("Other names commonly used to describe a food, " +
             "including local or regional names for various foods, " +
             "for example, 'soda' or 'pop' for 'carbonated beverages.'")]
    public string? ComName { get; set; }

    [MaxLength(65)]
    [Comment("Indicates the company that manufactured the product, when appropriate.")]
    public string? ManufacName { get; set; }

    [Comment("Indicates if the food item is used in the USDA Food " +
             "and Nutrient Database for Dietary Studies (FNDDS) " +
             "and thus has a complete nutrient profile for the 65 " +
             "FNDDS nutrients.")]
    public char? Survey { get; set; }

    [MaxLength(135)]
    [Comment("Description of inedible parts of a food item (refuse), such as seeds or bone.")]
    public string? Ref_desc { get; set; }

    [Precision(2, 0)]
    [Comment("Percentage of refuse.")]
    public decimal? Refuse { get; set; }

    [MaxLength(65)]
    [Comment("Scientific name of the food item. Given for the least " +
             "processed form of the food (usually raw), if applicable.")]
    public string? SciName { get; set; }

    [Precision(4, 2)]
    [Comment("Factor for converting nitrogen to protein.")]
    public decimal? N_Factor { get; set; }

    [Precision(4, 2)]
    [Comment("Factor for calculating calories from protein.")]
    public decimal? Pro_Factor { get; set; }

    [Precision(4, 2)]
    [Comment("Factor for calculating calories from fat.")]
    public decimal? Fat_Factor { get; set; }

    [Precision(4, 2)]
    [Comment("Factor for calculating calories from carbohydrate.")]
    public decimal? CHO_Factor { get; set; }

    // Relationships
    public FoodGroup FoodGroup { get; set; }
    public ICollection<NutrientData> NutrientData { get; set; }
    public ICollection<Weight> Weights { get; set; } = [];
    public ICollection<FootnoteD> Footnotes { get; set; } = [];
    public ICollection<LanguaLFactor> LanguaLFactors { get; set; } = [];
    ICollection<DataSourceLink> DataSourceLinks { get; set; } = [];
}