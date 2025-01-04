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
    public async Task FoodDescriptionTest()
    {
        await using var context = new EfCoreContext();
        var footnoteDs = context.FootnoteDs
            .Include(fd => fd.FoodDescription)
            .AsQueryable()
            .Where(fd => fd.FoodDescriptionId == "12120");

        var f1 = await footnoteDs.SingleAsync(f => f.Footnt_No == "01");
        Assert.Equal("Unroasted", f1.Footnt_Txt);
        Assert.Equal("Nuts, hazelnuts or filberts", f1.FoodDescription.Long_Desc);

        var f2 = await footnoteDs.SingleAsync(f => f.Footnt_No == "02");
        Assert.Equal("Other phytosterols = 12.0 mg/100g; these include delta 5-avenasterol (2.6), campestanol (3.0), sitostanol (3.9) and other minor phytosterols (2.5 mg).", f2.Footnt_Txt);
        Assert.Equal("Nuts, hazelnuts or filberts", f2.FoodDescription.Long_Desc);
    }

    [Fact]
    public async Task FootnoteDCountTest()
    {
        await using var context = new EfCoreContext();
        var footnoteDs = context.FootnoteDs;
        var count = await footnoteDs.CountAsync();
        Assert.Equal(261, count);
    }

    [Fact]
    public async Task NutrientDataTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.FootnoteNs
            .Include(fn => fn.NutrientData)
            .SingleAsync(fn => fn.FoodDescriptionId == "12538" && fn.NutrientDefinitionId == "204");

        Assert.Equal("Fat and fatty acids based on 25% roasted in cottonseed oil and 75% roasted in sunflower oil", footnote.Footnt_Txt);
        Assert.Null(footnote.NutrientData);
    }

    [Fact]
    public async Task FootnoteNCountTest()
    {
        await using var context = new EfCoreContext();
        var footnoteNs = context.FootnoteNs;
        var count = await footnoteNs.CountAsync();
        Assert.Equal(273, count);
    }

    [Fact]
    public async Task NutrientDefinitionTest()
    {
        await using var context = new EfCoreContext();
        var footnote = await context.FootnoteMs
            .SingleAsync(fm => fm.FoodDescriptionId == "14384");

        Assert.Equal("One fl oz = 29.57 g.", footnote.Footnt_Txt);
    }

    [Fact]
    public async Task FootnoteMCountTest()
    {
        await using var context = new EfCoreContext();
        var footnoteMs = context.FootnoteMs;
        var count = await footnoteMs.CountAsync();
        Assert.Equal(18, count);
    }
}