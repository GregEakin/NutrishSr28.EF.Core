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

public class NutrientDataDto
{
    public string NDB_No { get; set; }
    public string Nutr_No { get; set; }
    public decimal Nutr_Val { get; set; }
    public decimal Num_Data_Pts { get; set; }
    public decimal? Std_Error { get; set; }
    public string Src_Cd { get; set; }
    public string? Deriv_Cd { get; set; }
    public string? Ref_NDB_No { get; set; }
    public string? Add_Nutr_Mark { get; set; }
    public decimal? Num_Studies { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    public decimal? DF { get; set; }
    public decimal? Low_EB { get; set; }
    public decimal? Up_EB { get; set; }
    public string? Stat_cmt { get; set; }
    public string? AddMod_Date { get; set; }
    public string? CC { get; set; }
}