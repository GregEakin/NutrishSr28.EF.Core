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

[Table("SRC", Schema = "SR28")]
[Comment("This file contains codes indicating the type of data (analytical, calculated, assumed zero, " +
         "and so on) in the Nutrient Data file. To improve the usability of the database and to provide " +
         "values for the FNDDS, NDL staff imputed nutrient values for a number of proximate components, " +
         "total dietary fiber, total sugar, and vitamin and mineral values.")]
public class SourceCode
{
    [Key]
    [Column("Src_Cd")]
    [Required]
    [Comment("A 2-digit code indicating type of data.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int SourceCodeId { get; set; }

    [Column("SrcCd_Desc")]
    [MaxLength(60)]
    [Required]
    [Comment("Description of source code that identifies the type of nutrient data.")]
    public string SourceCodeDescription { get; set; }

    //-----------------------------------------------
    //Relationships

    public ICollection<NutrientData> NutrientData { get; set; }
}