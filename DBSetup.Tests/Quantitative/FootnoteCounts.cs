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

public class FootnoteCounts
{
    [Fact]
    public async Task FullCountDTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.FootnoteDs.CountAsync(TestContext.Current.CancellationToken);

        Assert.Equal(261, count);
    }
    [Fact]
    public async Task FullCountMTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.FootnoteMs.CountAsync(TestContext.Current.CancellationToken);

        Assert.Equal(18, count);
    }
    [Fact]
    public async Task FullCountNTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.FootnoteNs.CountAsync(TestContext.Current.CancellationToken);

        Assert.Equal(273, count);
    }
}