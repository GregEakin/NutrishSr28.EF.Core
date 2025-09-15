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
    .WithDataVolume(isReadOnly: false)
    .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050))
    .WithPgWeb(pgWeb => pgWeb.WithHostPort(5051));

var nutrishDb = postgres.AddDatabase("nutrishdb");

// var pgadmin = builder.AddContainer("pgadmin", "dpage/pgadmin4")
//     .WithEnvironment("PGADMIN_DEFAULT_EMAIL", "admin@localhost.com")
//     .WithEnvironment("PGADMIN_DEFAULT_PASSWORD", "admin")
//     .WithReference(postgres)
//     .WithHttpEndpoint(port: 5050, targetPort: 80, name: "pgadmin");
//     // .WithVolume("../../AppHost/register_server.sh", "/docker-entrypoint-init.d/register_server.sh", isReadOnly: true)
//     // .WithVolume("/tmp/pgadmin-servers", "/pgadmin4/servers", isReadOnly: false)
//     // .WithEnvironment("PGADMIN_SERVER_JSON_FILE", "/pgadmin4/servers/servers.json")
//     // .WithCommand("/bin/bash", "-c", "/docker-entrypoint-init.d/register_server.sh && /entrypoint.sh");


var setup = builder.AddProject<Projects.DBSetup>("dbsetup")
    .WithReference(nutrishDb);

var tests = builder.AddProject<Projects.DBSetup_Tests>("tests")
    .WithReference(nutrishDb)
    .WithEnvironment("ConnectionStrings__nutrishdb", nutrishDb);

builder.Build().Run();
