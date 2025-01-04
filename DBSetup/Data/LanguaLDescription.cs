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

[Table("LANGDESC", Schema = "SR28")]
[Comment("This file is a support file to the LanguaL Factor file and contains the descriptions for only those factors used in coding the selected food items codes in this release of SR.")]
public class LanguaLDescription
{
    [Key]
    [Column("Factor_Code", TypeName = "nchar(5)")]
    [Required]
    [Comment("The LanguaL factor from the Thesaurus. Only those " +
             "codes used to factor the foods contained in the " +
             "LanguaL Factor file are included in this file. ")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string LangualDescriptionId { get; set; }

    [Column("Description")]
    [MaxLength(140)]
    [Required]
    [Comment("The description of the LanguaL Factor Code from the thesaurus. ")]
    public string Description { get; set; }

    public ICollection<LanguaLFactor> LanguaLFactors { get; set; } = [];
}