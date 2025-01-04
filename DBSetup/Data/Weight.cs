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

[Table("WEIGHT", Schema = "SR28")]
[PrimaryKey(nameof(FoodDescriptionId), nameof(Seq))]
[Comment("This file contains codes indicating the type of data (analytical, calculated, assumed zero, " +
         "and so on) in the Nutrient Data file. To improve the usability of the database and to provide " +
         "values for the FNDDS, NDL staff imputed nutrient values for a number of proximate components, " +
         "total dietary fiber, total sugar, and vitamin and mineral values.")]
public class Weight
{
    [Key]
    [Column("NDB_No", TypeName = "nchar(5)")]
    [Required]
    [Comment("5-digit Nutrient Databank number that uniquely identifies a food item.  " +
             "If this field is defined as numeric, the leading zero will be lost.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodDescriptionId { get; set; }

    [Key]
    [Column("Seq")]
    [Required]
    [Comment("Sequence number.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Seq { get; set; }

    [Column("Amount")]
    [Required]
    [Comment("Unit modifier (for example, 1 in '1 cup').")]
    public float Amount { get; set; }

    [Column("Msre_Desc")]
    [MaxLength(84)]
    [Required]
    [Comment("Description (for example, cup, diced, and 1-inch pieces)")]
    public string Msre_Desc { get; set; }

    [Column("Gm_Wgt")]
    [Required]
    [Comment("Gram weight.")]
    public float Gm_Wgt { get; set; }

    [Column("Num_Data_Pts")]
    [Comment("Number of data points.")]
    public int? Num_Data_Pts { get; set; }

    [Column("Std_Dev")]
    [Comment("Standard deviation.")]
    public float? Std_Dev { get; set; }

    //-----------------------------------------------
    //Relationships
    public FoodDescription FoodDescription { get; set; }

    // Links to Nutrient Data file by NDB_No 
}