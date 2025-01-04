// Copyright 2025 Gregory Eakin
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

#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBSetup.Data;

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
    [MaxLength(1)]
    [Required]
    [Comment("Type of footnote: " +
             "D = footnote adding information to the food description;  " +
             "M = footnote adding information to measure description;  " +
             "N = footnote providing additional information on a nutrient value. " +
             "If the Footnt_typ = N, the Nutr_No will also be filled in.")]
    public string Footnt_Typ { get; set; }

    // [NotMapped]
    // public string Footnote_Type { get; set; }

    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Comment("Unique 3-digit identifier code for a nutrient to which footnote applies.")]
    public string NutrientDefinitionId { get; set; }

    [Column("Footnt_Txt")]
    [MaxLength(200)]
    [Required]
    [Comment("Footnote text.")]
    public string Footnt_Txt { get; set; }

    //-----------------------------------------------
    //Relationships
    // public NutrientDefinition NutrientDefinition { get; set; }
}