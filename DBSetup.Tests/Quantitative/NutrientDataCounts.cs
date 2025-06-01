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

using Microsoft.EntityFrameworkCore;

namespace DBSetup.Tests.Quantitative;

public class NutrientDataCounts
{
    [Fact]
    public async Task FullCountTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.NutrientData.CountAsync();

        Assert.Equal(679045, count);
    }

    [Fact]
    public async Task FoodGroupDescriptionsCounts()
    {
        var expectedValues = new[]
        {
            ("10:0", 6478),
            ("12:0", 6664),
            ("13:0", 270),
            ("14:0", 7006),
            ("14:1", 2755),
            ("15:0", 2399),
            ("15:1", 2101),
            ("16:0", 7213),
            ("16:1 c", 1373),
            ("16:1 t", 1243),
            ("16:1 undifferentiated", 6999),
            ("17:0", 2785),
            ("17:1", 2485),
            ("18:0", 7202),
            // some 150 more lines
        };

        await using var context = new EfCoreContext();
        var foodGroupCounts = await context.NutrientDefinitions
            .Include(nd => nd.NutrientData)
            .OrderBy(nd => nd.NutrDesc)
            .Select(nd => new Tuple<string, int>(nd.NutrDesc, nd.NutrientData.Count).ToValueTuple())
            .Take(14)
            .ToArrayAsync();

        Assert.Equal(expectedValues, foodGroupCounts);
    }
}