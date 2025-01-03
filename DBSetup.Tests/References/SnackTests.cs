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

public class SnackTests
{
    [Fact]
    public async Task TestSpinach()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.NutrientData)
            .ThenInclude(nd => nd.NutrientDefinition)
            .SingleAsync(fd => fd.Long_Desc == "Spinach, raw");
        
        Assert.NotNull(foodDescription);

        var nutrientData = foodDescription.NutrientData;
        Assert.NotNull(nutrientData);

        Assert.Equal(70, nutrientData.Count(nd => nd.Nutr_Val > 0.0m));
        foreach (var data in nutrientData)
        {
            var nutrientDefinition = data.NutrientDefinition;
            Assert.NotNull(nutrientDefinition);
        }

        var iron = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Iron, Fe");
        Assert.Equal(2.710m, iron.Nutr_Val);
        Assert.Equal("mg", iron.NutrientDefinition.Units);

        var magnesium = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Magnesium, Mg");
        Assert.Equal(79.000m, magnesium.Nutr_Val);
        Assert.Equal("mg", magnesium.NutrientDefinition.Units);
    }

    [Fact]
    public async Task TestIceCream()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.NutrientData)
            .ThenInclude(nd => nd.NutrientDefinition)
            .Include(fd => fd.Weights)
            .SingleAsync(fd => fd.Long_Desc == "Ice creams, BREYERS, No Sugar Added, Butter Pecan");

        Assert.NotNull(foodDescription);

        var nutrientData = foodDescription.NutrientData;
        Assert.NotNull(nutrientData);
        Assert.Equal(16, nutrientData.Count(nd => nd.Nutr_Val > 0.0m));
        foreach (var data in nutrientData)
        {
            var nutrientDefinition = data.NutrientDefinition;
            Assert.NotNull(nutrientDefinition);
        }

        var calories = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Energy" && nd.NutrientDefinition.Units == "kcal");
        Assert.Equal(180.000m, calories.Nutr_Val);
        Assert.Equal("kcal", calories.NutrientDefinition.Units);

        var sugars = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Sugars, total");
        Assert.Equal(5.900m, sugars.Nutr_Val);
        Assert.Equal("g", sugars.NutrientDefinition.Units);

        var weights = foodDescription.Weights;
        Assert.NotNull(weights);
        var weight = weights.Single();
        Assert.Equal(1.000m, weight.Amount);
        Assert.Equal("serving 1/2 cup", weight.Msre_Desc);
    }

    [Fact]
    public async Task TestCookies()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.NutrientData)
            .ThenInclude(nd => nd.NutrientDefinition)
            .Include(fd => fd.Weights)
            .SingleAsync(fd => fd.Long_Desc == "MURRAY, SUGAR FREE, Chocolate Chip & Pecan Cookies");

        Assert.NotNull(foodDescription);

        var nutrientData = foodDescription.NutrientData;
        Assert.NotNull(nutrientData);
        Assert.Equal(26, nutrientData.Count(nd => nd.Nutr_Val > 0.0m));
        foreach (var data in nutrientData)
        {
            var nutrientDefinition = data.NutrientDefinition;
            Assert.NotNull(nutrientDefinition);
        }

        var calories = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Energy" && nd.NutrientDefinition.Units == "kcal");
        Assert.Equal(497.000m, calories.Nutr_Val);
        Assert.Equal("kcal", calories.NutrientDefinition.Units);

        var sugars = nutrientData.Single(nd => nd.NutrientDefinition.NutrDesc == "Sugars, total");
        Assert.Equal(1.500m, sugars.Nutr_Val);
        Assert.Equal("g", sugars.NutrientDefinition.Units);

        var weights = foodDescription.Weights;
        Assert.NotNull(weights);
        var weight = weights.Single();
        Assert.Equal(3.000m, weight.Amount);
        Assert.Equal("cookies", weight.Msre_Desc);
    }
}