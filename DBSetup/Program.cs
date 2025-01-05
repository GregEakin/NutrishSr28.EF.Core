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

using System.Diagnostics;
using DBSetup;
using DBSetup.Loader;

Console.WriteLine("Hello, World!");
const string dir = @"../data";

var watch = Stopwatch.StartNew();

await using var context = new EfCoreContext();

await new FD_GROUP().ParseFileAsync(context, dir);
await new SRC_CD().ParseFileAsync(context, dir);
await new DERIV_CD().ParseFileAsync(context, dir);
await new LANGDESC().ParseFileAsync(context, dir);
await new DATA_SRC().ParseFileAsync(context, dir);
await new NUTR_DEF().ParseFileAsync(context, dir);
await new FOOD_DES().ParseFileAsync(context, dir);
await new WEIGHT().ParseFileAsync(context, dir);
await new LANGUAL().ParseFileAsync(context, dir);
await new FOOTNOTE().ParseFileAsync(context, dir);
await new NUT_DATA().ParseFileAsync(context, dir);
await new DATSRCLN().ParseFileAsync(context, dir);

Console.WriteLine($"Elapsed time: {watch.Elapsed}");
