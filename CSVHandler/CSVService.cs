using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Movie.CSVHandler.Models;
using Movie.CSVHandler.Mappers;

namespace Movie.CSVHandler
{
    public class CSVService: ICSVService
    {
        public IEnumerable<T> ReadFromCSV<T>(string fileName)
        {
            var directory = "C:\\Temp";
            var fileLocation = $"{directory}\\{fileName}";
            try
            {
                using (var reader = new StreamReader(fileLocation, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    if (typeof(T) == typeof(MovieMetadata))
                    {
                        csv.Context.RegisterClassMap<MetadataMapper>();
                    }
                    else if (typeof(T) == typeof(MovieStat))
                    {
                        csv.Context.RegisterClassMap<MovieStatMapper>();
                    }
                    else
                    {
                        throw new InvalidDataException($"{nameof(T)} does not have a valid mapper for CSV!");
                    }
                    
                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
