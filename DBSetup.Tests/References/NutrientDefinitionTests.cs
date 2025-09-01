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

namespace DBSetup.Tests.References;

public class NutrientDefinitionTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var nutrientDefinition = await context.NutrientDefinitions.FindAsync(["255"], TestContext.Current.CancellationToken);
        Assert.NotNull(nutrientDefinition);
        Assert.Equal("255", nutrientDefinition.NutrientDefinitionId);
        Assert.Equal("Water", nutrientDefinition.NutrDesc);
        Assert.Equal("g", nutrientDefinition.Units);
    }

    [Fact]
    public async Task DataSourceLinksTest()
    {
        await using var context = new EfCoreContext();
        var nutrientDefinition = await context.NutrientDefinitions
            .Include(nd => nd.DataSourceLinks)
            .SingleAsync(nd => nd.NutrientDefinitionId == "255", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientDefinition);
        var links = nutrientDefinition.DataSourceLinks;
        Assert.Equal(4339, links.Count);
        foreach (var link in links)
            Assert.Equal(nutrientDefinition, link.NutrientDefinition);
    }

    [Fact]
    public async Task NutrientDataTest()
    {
        await using var context = new EfCoreContext();
        var nutrientDefinition = await context.NutrientDefinitions
            .Include(nd => nd.NutrientData)
            .SingleAsync(nd => nd.NutrientDefinitionId == "255", TestContext.Current.CancellationToken);
     
        Assert.NotNull(nutrientDefinition);
        var nutrientData = nutrientDefinition.NutrientData;
        Assert.Equal(8788, nutrientData.Count);
        foreach (var data in nutrientData)
            Assert.Equal(nutrientDefinition, data.NutrientDefinition);
    }
}
