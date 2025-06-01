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

public class NutrientDataTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData.FindAsync("12538", "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
        Assert.Equal("12538", nutrientData.FoodDescriptionId);
        Assert.Equal("421", nutrientData.NutrientDefinitionId);
        Assert.Equal(55.100m, nutrientData.Nutr_Val);
        // Assert.Equal("g", nutrientData.
    }

    [Fact]
    public async Task FoodDescriptionTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData
            .Include(nd => nd.FoodDescription)
            .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
        Assert.NotNull(nutrientData.FoodDescription);
        Assert.Equal("12538", nutrientData.FoodDescription.FoodDescriptionId);
        Assert.Equal("Seeds, sunflower seed kernels, oil roasted, with salt added", nutrientData.FoodDescription.Long_Desc);
    }

    [Fact]
    public async Task FoodDescriptionRefTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData
            .Include(nd => nd.FoodDescriptionRef)
            .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
        Assert.NotNull(nutrientData.FoodDescriptionRef);
        Assert.Equal("12036", nutrientData.FoodDescriptionRef.FoodDescriptionId);
        Assert.Equal("Seeds, sunflower seed kernels, dried", nutrientData.FoodDescriptionRef.Long_Desc);
    }

    // [Fact]
    // public async Task FootnotesTest()
    // {
    //     await using var context = new EfCoreContext();
    //     var nutrientData = await context.NutrientData
    //         .Include(nd => nd.Footnote)
    //         .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);
    //
    //     Assert.NotNull(nutrientData);
    // }

    // [Fact]
    // public async Task DataSourcesTest()
    // {
    //     await using var context = new EfCoreContext();
    //     var nutrientData = await context.NutrientData
    //         .Include(nd => nd.DataSources)
    //         .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);
    //
    //     Assert.NotNull(nutrientData);
    // }

    [Fact]
    public async Task NutrientDefinitionTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData
            .Include(nd => nd.NutrientDefinition)
            .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
        Assert.NotNull(nutrientData.NutrientDefinition);
        Assert.Equal("421", nutrientData.NutrientDefinition.NutrientDefinitionId);
        Assert.Equal("Choline, total", nutrientData.NutrientDefinition.NutrDesc);
    }

    [Fact]
    public async Task SourceCodeTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData
            .Include(nd => nd.SourceCode)
            .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
    }

    [Fact]
    public async Task DerivationCodeTest()
    {
        await using var context = new EfCoreContext();
        var nutrientData = await context.NutrientData
            .Include(nd => nd.DerivationCode)
            .SingleAsync(nd => nd.FoodDescriptionId == "12538" && nd.NutrientDefinitionId == "421", TestContext.Current.CancellationToken);

        Assert.NotNull(nutrientData);
    }
}