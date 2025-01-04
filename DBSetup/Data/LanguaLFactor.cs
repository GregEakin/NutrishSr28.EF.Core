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

[Table("LANGUAL", Schema = "SR28")]
[PrimaryKey(nameof(FoodDescriptionId), nameof(LangualDescriptionId))]
[Comment("This file is a support file to the Food Description file and contains the factors from the LanguaL Thesaurus used to code a particular food.")]
public class LanguaLFactor
{
    [Key]
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Required]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Key]
    [Column("Factor_Code", TypeName = "nchar(5)")]
    [Required]
    [Comment("The LanguaL factor from the Thesaurus.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string LangualDescriptionId { get; set; }

    public FoodDescription FoodDescription { get; set; }
    public LanguaLDescription LanguaLDescription { get; set; }
}