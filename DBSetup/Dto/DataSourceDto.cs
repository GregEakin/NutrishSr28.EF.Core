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

public class DataSourceDto
{
    public string DataSrc_ID { get; set; }
    public string? Authors { get; set; }
    public string Title { get; set; }
    public string? Year { get; set; }
    public string? Journal { get; set; }
    public string? Vol_City { get; set; }
    public string? Issue_State { get; set; }
    public string? Start_Page { get; set; }
    public string? End_Page { get; set; }
}