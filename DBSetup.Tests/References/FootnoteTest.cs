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

public class FootnoteTest
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.Footnotes.SingleAsync(f => f.FoodDescriptionId == "12538" &&
                                                                f.Footnt_Typ == 'N' &&
                                                                f.NutrientDefinitionId == "204");
        
        Assert.NotNull(footnote);
        Assert.Equal("12538", footnote.FoodDescriptionId);
        Assert.Equal("01", footnote.Footnt_No);
        Assert.Equal('N', footnote.Footnt_Typ);
        Assert.Equal("204", footnote.NutrientDefinitionId);
        Assert.Equal("Fat and fatty acids based on 25% roasted in cottonseed oil and 75% roasted in sunflower oil", footnote.Footnt_Txt);
    }

    [Fact]
    public async Task FoodDescriptionTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.Footnotes
            .Include(f => f.FoodDescription)
            .SingleAsync(f => f.FoodDescriptionId == "12538" &&
                              f.Footnt_Typ == 'N' &&
                              f.NutrientDefinitionId == "204");

        Assert.NotNull(footnote);
        Assert.NotNull(footnote.FoodDescription);
        Assert.Equal("12538", footnote.FoodDescription.FoodDescriptionId);
        Assert.Equal("Seeds, sunflower seed kernels, oil roasted, with salt added", footnote.FoodDescription.Long_Desc);
    }

    [Fact]
    public async Task NutrientDataTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.Footnotes
            .Include(f => f.NutrientData)
            .SingleAsync(f => f.FoodDescriptionId == "15066" &&
                              f.Footnt_Typ == 'N' &&
                              f.NutrientDefinitionId == "307");

        Assert.NotNull(footnote);
        var nutrientData = footnote.NutrientData;
        Assert.Single(nutrientData);
        foreach (var data in nutrientData)
        {
            Assert.NotNull(data);
            Assert.Equal("15066", data.FoodDescriptionId);
        }
    }

    [Fact]
    public async Task NutrientDefinitionTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.Footnotes
            .Include(f => f.NutrientDefinition)
            .SingleAsync(f => f.FoodDescriptionId == "12538" &&
                              f.Footnt_Typ == 'N' &&
                              f.NutrientDefinitionId == "204");

        Assert.NotNull(footnote);
        Assert.NotNull(footnote.NutrientDefinition);
        Assert.Equal("204", footnote.NutrientDefinition.NutrientDefinitionId);
        Assert.Equal("Total lipid (fat)", footnote.NutrientDefinition.NutrDesc);
    }
}