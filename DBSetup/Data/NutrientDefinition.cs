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

[Table("NUTR_DEF", Schema = "SR28")]
[Comment("This file is a support file to the Nutrient Data file. " +
         "It provides the 3-digit nutrient code, unit of measure, INFOODS tagname, and description.")]
public class NutrientDefinition
{
    [Key]
    [Column("Nutr_No", TypeName = "nchar(3)")]
    [Required]
    [Comment("Unique 3-digit identifier code for a nutrient.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string NutrientDefinitionId { get; set; }

    [Column("Units")]
    [MaxLength(7)]
    [Required]
    [Comment("Units of measure (mg, g, μg, and so on).")]
    public string Units { get; set; }

    [Column("Tagname")]
    [MaxLength(20)]
    [Comment("International Network of Food Data Systems (INFOODS) Tagnames. " +
             "A unique abbreviation for a nutrient/food component developed " +
             "by INFOODS to aid in the interchange of data.")]
    public string? TagName { get; set; }

    [Column("NutrDesc")]
    [MaxLength(60)]
    [Required]
    [Comment("Name of nutrient/food component.")]
    public string NutrDesc { get; set; }

    [Column("Num_Dec")]
    [Required]
    [Comment("Number of decimal places to which a nutrient value is rounded.")]
    public char Num_Dec { get; set; }

    [Column("SR_Order")]
    [MaxLength(6)]
    [Required]
    [Comment("Used to sort nutrient records in the same order as various reports produced from SR.")]
    public int SR_Order { get; set; }

    //-----------------------------------------------
    //Relationships
    public ICollection<DataSourceLink> DataSourceLinks { get; set; }
    public ICollection<NutrientData> NutrientData { get; set; }
}