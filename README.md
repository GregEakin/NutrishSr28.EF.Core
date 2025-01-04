# NutrishSr28.EF.Core
:fire: **Gregory Eakin**

This is an experiment in configuring an existing [USDA Nutrition Database](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr11-sr28/) in [NHibernate](https://nhibernate.info/).

## Steps to setup SQL Local DB:
1. Unzip the [Full Version of the SR28 ASCII file format](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr11-sr28/) into the data folder.
1. Unzip the patch file (May 2016) into the data2 folder.
	Overwrite the DATASRCLN.txt and sr28_doc.pdf
```
SqllocalDB i
SqllocalDB create "SR28" -s
sqlcmd -S "(localdb)\SR28" -Q "CREATE DATABASE Nutrish"
cd DBSetup
dotnet ef database update
dotnet run
cd ../DBSetup.Tests
dotnet test
```

## Database:
[![USDA Nutrition Database](SR28lib/Nutrish%20SR28.jpg "USDA Nutrition Database")](https://www.ars.usda.gov/northeast-area/beltsville-md-bhnrc/beltsville-human-nutrition-research-center/methods-and-application-of-food-composition-laboratory/mafcl-site-pages/sr17-sr28/)
US Department of Agriculture, Agricultural Research Service. 2016. Nutrient Data Laboratory. USDA National Nutrient Database for Standard Reference, Release 28 (Slightly revised). Version Current: May 2016. [http://www.ars.usda.gov/nea/bhnrc/mafcl](http://www.ars.usda.gov/nea/bhnrc/mafcl)

## Tools:
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)]
- [SQL Server Management Sdutio](https://docs.microsoft.com/en-us/sql/ssms/sql-server-management-studio-ssms)
- [PostgreSQL](https://www.postgresql.org/)
- [pgAdmin](https://www.pgadmin.org/)
- [CsvHelper](https://joshclose.github.io/CsvHelper/)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [ReSharper](https://www.jetbrains.com/resharper/)
- [Unit Tests](https://xunit.net/)
- [Git Extensions](http://gitextensions.github.io/)

## Author
:fire: [Greg Eakin](https://www.linkedin.com/in/gregeakin)

# PostgreSQL DB Stuff
```
docker exec -it sqlserver-sqldb-1 psql -U sqlserver -c "CREATE database sr28;"
docker exec -it sqlserver-sqldb-1 psql -U sqlserver -c "CREATE USER postgres;"
docker exec -it sqlserver-sqldb-1 psql -U sqlserver -c "ALTER USER postgres WITH PASSWORD 'sqlserver';"
docker exec -it sqlserver-sqldb-1 psql -U sqlserver -c "GRANT ALL PRIVILEGES ON DATABASE sr28 TO postgres;"
docker exec -it sqlserver-sqldb-1 psql -U sqlserver -c "ALTER USER postgres WITH SUPERUSER;"
```
