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

public class DataSourceTests
{
    [Fact]
    public async Task FindByKeyTest()
    {
        await using var context = new EfCoreContext();
        var dataSource = await context.DataSources.FindAsync(["S12"], TestContext.Current.CancellationToken);

        Assert.NotNull(dataSource);
        Assert.Equal("S12", dataSource.DataSourceId);
        Assert.Equal("Food and Drug Administration (FDA), DHHS", dataSource.Authors);
        Assert.Equal("FDA Total Diet Study", dataSource.Title);
        Assert.Equal("1997", dataSource.Year);
    }
}
