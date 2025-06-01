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

public class LanguaLDescriptionTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var languaLDescription = await context.LanguaLDescriptions.FindAsync("A0208", TestContext.Current.CancellationToken);

        Assert.NotNull(languaLDescription);
        Assert.Equal("A0208", languaLDescription.LangualDescriptionId);
        Assert.Equal("SALAD (US CFR)", languaLDescription.Description);
    }

    [Fact]
    public async Task FindByValueTest()
    {
        await using var context = new EfCoreContext();
        var languaLDescription = await context.LanguaLDescriptions
            .SingleAsync(ld => ld.Description == "SALAD (US CFR)", TestContext.Current.CancellationToken);

        Assert.NotNull(languaLDescription);
        Assert.Equal("A0208", languaLDescription.LangualDescriptionId);
        Assert.Equal("SALAD (US CFR)", languaLDescription.Description);
    }
}
