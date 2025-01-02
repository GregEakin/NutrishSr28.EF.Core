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

public class LangualDescriptionTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var langualDescription = await context.LangualDescriptions.FindAsync("A0208");

        Assert.NotNull(langualDescription);
        Assert.Equal("A0208", langualDescription.LangualDescriptionId);
        Assert.Equal("SALAD (US CFR)", langualDescription.Description);
    }

    [Fact]
    public async Task FindByValueTest()
    {
        await using var context = new EfCoreContext();
        var langualDescription = await context.LangualDescriptions
            .SingleAsync(ld => ld.Description == "SALAD (US CFR)");

        Assert.NotNull(langualDescription);
        Assert.Equal("A0208", langualDescription.LangualDescriptionId);
        Assert.Equal("SALAD (US CFR)", langualDescription.Description);
    }
}
