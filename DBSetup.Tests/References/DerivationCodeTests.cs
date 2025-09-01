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

public class DerivationCodeTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var derivationCode = await context.DerivationCodes.FindAsync(["RC"], TestContext.Current.CancellationToken);

        Assert.NotNull(derivationCode);
        Assert.Equal("RC", derivationCode.DerivationCodeId);
        Assert.Equal("Recipe; Cookbook", derivationCode.DerivationCodeDescription);
    }

    [Fact]
    public async Task NutrientDataTest()
    {
        await using var context = new EfCoreContext();
        var derivationCode = await context.DerivationCodes
            .Include(dc => dc.NutrientData)
            .SingleAsync(dc => dc.DerivationCodeId == "RC", TestContext.Current.CancellationToken);

        Assert.NotNull(derivationCode);
        var nutrientData = derivationCode.NutrientData;
        Assert.Equal(2358, nutrientData.Count);
        foreach (var data in nutrientData)
            Assert.Equal(derivationCode, data.DerivationCode);
    }
}