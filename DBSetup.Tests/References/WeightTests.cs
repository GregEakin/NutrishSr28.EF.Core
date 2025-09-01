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

public class WeightTests()
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var weight = await context.Weights.FindAsync(["01001", 1], TestContext.Current.CancellationToken);
     
        Assert.NotNull(weight);
        Assert.Equal("01001", weight.FoodDescriptionId);
        Assert.Equal(1, weight.Seq);
        Assert.Equal(1.0f, weight.Amount);
        Assert.Equal("pat (1\" sq, 1/3\" high)", weight.Msre_Desc);
        Assert.Equal(5.0f, weight.Gm_Wgt);
    }

    [Fact]
    public async Task FoodDescriptionTest()
    {
        await using var context = new EfCoreContext();
        var weight = await context.Weights
            .Include(w => w.FoodDescription)
            .SingleAsync(w => w.FoodDescriptionId == "01001" && w.Seq == 1, TestContext.Current.CancellationToken);

        Assert.NotNull(weight);
        Assert.NotNull(weight.FoodDescription);
        Assert.Equal("01001", weight.FoodDescription.FoodDescriptionId);
        Assert.Equal("Butter, salted", weight.FoodDescription.Long_Desc);
    }
}