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

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false);

var nutrishDb = postgres.AddDatabase("nutrishdb");

// Add pgAdmin container and link to Postgres
var pgadmin = builder.AddContainer("pgadmin", "dpage/pgadmin4")
    .WithEnvironment("PGADMIN_DEFAULT_EMAIL", "admin@localhost.com")
    .WithEnvironment("PGADMIN_DEFAULT_PASSWORD", "admin")
    .WithReference(postgres) // This links pgAdmin to the Postgres server
    .WithHttpEndpoint(port: 5050, targetPort: 80, name: "pgadmin"); // Expose pgAdmin web UI

// var setup = builder.AddProject<Projects.DBSetup>("dbsetup")
//     .WithReference(nutrishDb)
//     // .WithExternalHttpEndpoints()
//     .WithVolume("sr28src", "../../sr28asc:/app/sr28asc", isReadOnly: true);

// var setup = builder.AddProject<Projects.DBSetup>("dbsetup")
//     .WithReference(nutrishDb)
//     .WithProjectVolume("../../sr28asc", "/app/sr28asc", isReadOnly: true);

var setup = builder.AddProject<Projects.DBSetup>("dbsetup")
    .WithReference(nutrishDb);

builder.Build().Run();
