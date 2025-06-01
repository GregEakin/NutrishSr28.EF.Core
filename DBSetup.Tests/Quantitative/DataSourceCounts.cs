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

public class DataSourceCounts
{
    [Fact] 
    public async Task FullCountTest()
    {
        await using var context = new EfCoreContext();
        var count = await context.DataSources.CountAsync();

        Assert.Equal(683, count);
    }

    [Fact]
    public async Task DataSourceLinksTest()
    {
        await using var context = new EfCoreContext();
        var dataSource = await context.DataSources
            .Include(ds => ds.DataSourceLinks)
            .SingleAsync(ds => ds.DataSourceId == "S8382", TestContext.Current.CancellationToken);

        Assert.NotNull(dataSource);
        var dataSourceLinks = dataSource.DataSourceLinks;
        Assert.Equal(866, dataSourceLinks.Count);
        foreach (var link in dataSourceLinks)
            Assert.Equal(dataSource, link.DataSource);
    }
}

