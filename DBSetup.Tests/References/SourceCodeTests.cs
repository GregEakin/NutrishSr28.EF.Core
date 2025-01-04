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

using Microsoft.EntityFrameworkCore;

namespace DBSetup.Tests.References;

public class SourceCodeTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var sourceCode = await context.SourceCodes.FindAsync(11);

        Assert.NotNull(sourceCode);
        Assert.Equal(11, sourceCode.SourceCodeId);
        Assert.Equal("Aggregated data involving comb. of codes other then 1,12 or6", sourceCode.SourceCodeDescription);
    }

    [Fact]
    public async Task FindNutrientDataTest()
    {
        await using var context = new EfCoreContext();
        var sourceCode = await context.SourceCodes
            .Include(sc => sc.NutrientData)
            .SingleAsync(sc => sc.SourceCodeId == 11);
        
        Assert.NotNull(sourceCode);
        var nutrientData = sourceCode.NutrientData;
        Assert.Equal(822, nutrientData.Count);
        foreach (var data in nutrientData) 
            Assert.Equal(sourceCode, data.SourceCode);
    }
}