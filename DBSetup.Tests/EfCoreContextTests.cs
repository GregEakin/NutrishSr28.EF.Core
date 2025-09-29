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

namespace DBSetup.Tests;

public class EfCoreContextTests
{
    private EfCoreContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<EfCoreContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
        return new EfCoreContext(options);
    }

    [Fact]
    public void Database_IsNew_WhenCreated()
    {
        using var context = CreateInMemoryContext();
        // Database should exist after context creation
        Assert.True(context.Database.CanConnect());
    }

    [Fact]
    public void Database_IsEmpty_WhenNoDataLoaded()
    {
        using var context = CreateInMemoryContext();
        // All tables should be empty
        Assert.False(context.FoodGroups.Any());
        Assert.False(context.FoodDescriptions.Any());
        Assert.False(context.NutrientDefinitions.Any());
        // Add more asserts for other tables as needed
    }
}