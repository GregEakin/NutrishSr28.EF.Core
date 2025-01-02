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

using DBSetup;

Console.WriteLine("Hello, World!");

await using var context = new EfCoreContext();

await FD_GROUP.ParseFileAsync(context);
await SRC_CD.ParseFileAsync(context);
await DERIV_CD.ParseFileAsync(context);
await LANGUAL_DESC.ParseFileAsync(context);
await DATA_SRC.ParseFileAsync(context);
await NUTR_DEF.ParseFileAsync(context);
await FOOD_DES.ParseFileAsync(context);         // Primary
await WEIGHT.ParseFileAsync(context);           // Primary
await LANGUAL.ParseFileAsync(context);
await FOOTNOTE.ParseFileAsync(context);         // Primary
await NUT_DATA.ParseFileAsync(context);         // Primary
// DataSrcLn

