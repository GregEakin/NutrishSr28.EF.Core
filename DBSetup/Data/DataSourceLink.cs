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

[Table("DATSRCLN", Schema = "SR28")]
[PrimaryKey(nameof(FoodDescriptionId), nameof(NutrientDefinitionId), nameof(DataSourceId))]
[Comment("This file is used to link the Nutrient Data file with the Sources of Data table. It is needed to resolve the many-to-many relationship between the two tables.")]
public class DataSourceLink
{
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Required]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item. " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Required]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string NutrientDefinitionId { get; set; }

    [Column("DataSrc_ID")]
    [MaxLength(6)]
    [Required]
    [Comment("Unique ID identifying the reference/source.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string DataSourceId { get; set; }

    //-----------------------------------------------
    //Relationships
    public FoodDescription FoodDescription { get; set; }
    public NutrientDefinition NutrientDefinition { get; set; }
    public DataSource DataSource { get; set; }
}