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

public class FoodGroupCounts
{
    [Fact]
    public async Task FullCountTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.FoodGroups.CountAsync(TestContext.Current.CancellationToken);

        Assert.Equal(25, count);
    }

    [Fact]
    public async Task FoodGroupDescriptionsCounts()
    {
        var expectedValues = new[]
        {
            ("American Indian/Alaska Native Foods", 165),
            ("Baby Foods", 367),
            ("Baked Products", 879),
            ("Beef Products", 961),
            ("Beverages", 371),
            ("Breakfast Cereals", 356),
            ("Cereal Grains and Pasta", 181),
            ("Dairy and Egg Products", 283),
            ("Fast Foods", 363),
            ("Fats and Oils", 220),
            ("Finfish and Shellfish Products", 265),
            ("Fruits and Fruit Juices", 360),
            ("Lamb, Veal, and Game Products", 464),
            ("Legumes and Legume Products", 381),
            ("Meals, Entrees, and Side Dishes", 125),
            ("Nut and Seed Products", 137),
            ("Pork Products", 341),
            ("Poultry Products", 389),
            ("Restaurant Foods", 110),
            ("Sausages and Luncheon Meats", 170),
            ("Snacks", 177),
            ("Soups, Sauces, and Gravies", 465),
            ("Spices and Herbs", 64),
            ("Sweets", 360),
            ("Vegetables and Vegetable Products", 836),
        };

        await using var context = new EfCoreContext();
        var foodGroupCounts = await context.FoodGroups
            .Include(fg => fg.FoodDescriptions)
            .OrderBy(fg => fg.FoodGroupName)
            .Select(fg => new Tuple<string, int>(fg.FoodGroupName, fg.FoodDescriptions.Count).ToValueTuple())
            .ToArrayAsync(TestContext.Current.CancellationToken);

        Assert.Equal(expectedValues, foodGroupCounts);
    }
}