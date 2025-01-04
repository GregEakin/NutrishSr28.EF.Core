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

namespace DBSetup.Dto;

#nullable disable
#pragma warning disable CS8632

public class FoodDescriptionDto
{
    public string NDB_No { get; set; }
    public string FdGrp_Cd { get; set; }
    public string Long_Desc { get; set; }
    public string Shrt_Desc { get; set; }
    public string? ComName { get; set; }
    public string? ManufacName { get; set; }
    public string? Survey { get; set; }
    public string? Ref_desc { get; set; }
    public int? Refuse { get; set; }
    public string? SciName { get; set; }
    public decimal? N_Factor { get; set; }
    public decimal? Pro_Factor { get; set; }
    public decimal? Fat_Factor { get; set; }
    public decimal? CHO_Factor { get; set; }
}