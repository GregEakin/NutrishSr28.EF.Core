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

public class FoodDescriptionTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions.FindAsync("01119", TestContext.Current.CancellationToken);

        Assert.NotNull(foodDescription);
        Assert.Equal("01119", foodDescription.FoodDescriptionId);
        Assert.Equal("YOGURT,VANILLA,LOFAT,11 GRAMS PROT PER 8 OZ", foodDescription.Shrt_Desc);
        Assert.Equal("Yogurt, vanilla, low fat, 11 grams protein per 8 ounce", foodDescription.Long_Desc);
    }

    [Fact]
    public async Task FoodGroupTest()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.FoodGroup)
            .ThenInclude(fg => fg.FoodDescriptions)
            .SingleAsync(fd => fd.FoodDescriptionId == "01119", TestContext.Current.CancellationToken);
        
        Assert.NotNull(foodDescription);
        var foodGroup = foodDescription.FoodGroup;
        Assert.Equal("0100", foodGroup.FoodGroupId);
        Assert.Equal("Dairy and Egg Products", foodGroup.FoodGroupName);
        Assert.True(foodGroup.FoodDescriptions.Contains(foodDescription));
    }

    // Links to the LanguaL Factor file by the NDB_No field
    [Fact]
    public async Task LanguageTest()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.LanguaLFactors)
            .ThenInclude(lf => lf.LanguaLDescription)
            .SingleAsync(fd => fd.FoodDescriptionId == "02002", TestContext.Current.CancellationToken);
        
        Assert.NotNull(foodDescription);
        var langualFactors = foodDescription.LanguaLFactors;
        Assert.Equal(13, langualFactors.Count);
        foreach (var langualFactor in langualFactors) 
            Assert.Equal(foodDescription, langualFactor.FoodDescription);
    }

    [Fact]
    public async Task FootnoteTest()
    {
        await using var context = new EfCoreContext();
        var foodDescription = await context.FoodDescriptions
            .Include(fd => fd.Footnotes)
            .SingleAsync(fd => fd.FoodDescriptionId == "12120", TestContext.Current.CancellationToken);

        Assert.NotNull(foodDescription);

        Assert.Equal("Nuts, hazelnuts or filberts", foodDescription.Long_Desc);

        Assert.NotNull(foodDescription.Footnotes);
        Assert.Equal(2, foodDescription.Footnotes.Count);

        foreach (var footnote in foodDescription.Footnotes)
            Assert.Equal(foodDescription, footnote.FoodDescription);

        var f1 = foodDescription.Footnotes.Single(fd => fd.Footnt_No == "01");
        Assert.Equal("Unroasted", f1.Footnt_Txt);

        var f2 = foodDescription.Footnotes.Single(fd => fd.Footnt_No == "02");
        Assert.Equal("Other phytosterols = 12.0 mg/100g; these include delta 5-avenasterol (2.6), campestanol (3.0), sitostanol (3.9) and other minor phytosterols (2.5 mg).", 
            f2.Footnt_Txt);
    }
}
