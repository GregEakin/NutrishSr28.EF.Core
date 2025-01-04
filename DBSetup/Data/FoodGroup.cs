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

[Table("FD_GROUP", Schema = "SR28")]
[Comment("Contains a list of food groups used in SR28 and their descriptions.")]
public class FoodGroup
{
    [Key]
    [Column("FdGrp_Cd", TypeName = "nchar(4)")]
    [Required]
    [Comment("4-digit code identifying a food group. Only the first 2 ndigits are currently assigned. " +
             "In the future, the last 2 digits may be used. Codes may not be consecutive.")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string FoodGroupId { get; set; }

    [Column("FdGrp_Desc")]
    [MaxLength(60)]
    [Required]
    [Comment("Name of food group.")]
    public string FoodGroupName { get; set; }

    //-----------------------------------------------
    //Relationships
    public ICollection<FoodDescription> FoodDescriptions { get; set; } = [];
}