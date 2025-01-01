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

public class FoodGroupTests
{
    [Fact]
    public async Task RecordNotFoundTest()
    {
        await using var context = new EfCoreContext();

        var foodGroup = await context.FoodGroups.FindAsync("Food");
        Assert.Null(foodGroup);
    }

    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();

        var foodGroup = await context.FoodGroups.FindAsync("0400");
        Assert.NotNull(foodGroup);
        Assert.Equal("0400", foodGroup.FdGrp_Cd);
        Assert.Equal("Fats and Oils", foodGroup.FdGrp_Desc);
    }

    [Fact]
    public async Task FindByValueTest()
    {
        await using var context = new EfCoreContext();

        var foodGroup = await context.FoodGroups
            .SingleAsync(fg => fg.FdGrp_Desc == "Fats and Oils");

        Assert.NotNull(foodGroup);
        Assert.Equal("0400", foodGroup.FdGrp_Cd);
        Assert.Equal("Fats and Oils", foodGroup.FdGrp_Desc);
    }

    [Fact]
    public async Task FoodDescriptionTest()
    {
        await using var context = new EfCoreContext();

        var foodGroup = await context.FoodGroups
            .Include(fg => fg.FoodDescriptions)
            .SingleAsync(fg => fg.FdGrp_Cd == "0400");

        Assert.Equal(220, foodGroup.FoodDescriptions.Count);
        foreach (var foodDescription in foodGroup.FoodDescriptions) 
            Assert.Equal(foodGroup, foodDescription.FoodGroupCode);
    }
}
