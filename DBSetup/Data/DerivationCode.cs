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

[Table("DERIVCD", Schema = "SR28")]
[Comment("This file provides information on how the nutrient values were determined. " +
         "The file contains the derivation codes and their descriptions.")]
public class DerivationCode
{
    [Key]
    [Column("Deriv_Cd")]
    [MaxLength(4)]
    [Comment("Derivation Code.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string DerivationCodeId { get; set; }

    [Column("Deriv_Desc")]
    [MaxLength(120)]
    [Required]
    [Comment("Description of derivation code giving specific information on how the value was determined.")]
    public string DerivationCodeDescription { get; set; }

    //-----------------------------------------------
    //Relationships
    public ICollection<NutrientData> NutrientData { get; set; }
}