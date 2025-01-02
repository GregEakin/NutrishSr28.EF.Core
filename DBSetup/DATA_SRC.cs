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

using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace DBSetup;

public static class DATA_SRC
{
    private static readonly string Filename = "../../../../data/DATA_SRC.txt";

    public static async Task ParseFileAsync(DbContext context)
    {
        using var reader = new StreamReader(Filename);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "^",
            Quote = '~',
            Escape = '$',
            HasHeaderRecord = false,
            BadDataFound = x => throw new Exception($"Bad data: <{x.RawRecord}>"),
            MissingFieldFound = x => throw new Exception($"Missing Filed: <{x.Index}>"),
        };

        using var csv = new CsvReader(reader, config);
        csv.Context.TypeConverterOptionsCache.GetOptions<string>().NullValues.Add("");

        await foreach (var record in csv.GetRecordsAsync<DataSourceDto>())
        {
            var item = ParseDataSource(record);
            context.Add(item);
            Console.WriteLine(item.DataSourceId + " " + item.Title);
        }

        await context.SaveChangesAsync();
        Console.WriteLine("Data Source done!");
    }

    private static DataSource ParseDataSource(DataSourceDto record)
    {
        var item = new DataSource
        {
            DataSourceId = record.DataSrc_ID,
            Authors = record.Authors,
            Title = record.Title,
            Year = record.Year,
            Journal = record.Journal,
            Vol_City = record.Vol_City,
            Issue_State = record.Issue_State,
            Start_Page = record.Start_Page,
            End_Page = record.End_Page,
        };

        return item;
    }
}

#nullable disable
#pragma warning disable CS8632

[Table("DATA_SRC")]
[Comment("This file provides a citation to the DataSrc_ID in the Sources of Data Link file.")]
public class DataSource
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Required]
    [MaxLength(6)]
    [Column("DataSrc_ID")]
    [Comment("Unique ID identifying the reference/source.")]
    public string DataSourceId { get; set; }

    [MaxLength(255)]
    [Column("Authors")]
    [Comment("List of authors for a journal article or name of sponsoring organization for other documents.")]
    public string? Authors { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("Title")]
    [Comment("Title of article or name of document, such as a report from a company or trade association.")]
    public string Title { get; set; }

    [MaxLength(4)]
    [Column("Year")]
    [Comment("Year article or document was published.")]
    public string? Year { get; set; }
    
    [MaxLength(135)]
    [Column("Journal")]
    [Comment("Name of the journal in which the article was published.")]
    public string? Journal { get; set; }
    
    [MaxLength(16)]
    [Column("Vol_City")]
    [Comment("Volume number for journal articles, books, or reports; city where sponsoring organization is located.")]
    public string? Vol_City { get; set; }
    
    [MaxLength(5)]
    [Column("Issue_State")]
    [Comment("Issue number for journal article; State where the sponsoring organization is located.")]
    public string? Issue_State { get; set; }
    
    [MaxLength(5)]
    [Column("Start_Page")]
    [Comment("Starting page number of article/document.")]
    public string? Start_Page { get; set; }

    [MaxLength(5)]
    [Column("End_Page")]
    [Comment("Ending page number of article/document.")]
    public string? End_Page { get; set; }

    //-----------------------------------------------
    //Relationships
    ICollection<DataSourceLink> DataSourceLinks { get; set; }
}

public class DataSourceDto
{
    public string DataSrc_ID { get; set; }
    public string? Authors { get; set; }
    public string Title { get; set; }
    public string? Year { get; set; }
    public string? Journal { get; set; }
    public string? Vol_City { get; set; }
    public string? Issue_State { get; set; }
    public string? Start_Page { get; set; }
    public string? End_Page { get; set; }
}
