using CsvHelper.Configuration;
using Movie.CSVHandler.Models;

namespace Movie.CSVHandler.Mappers
{
    public class MovieStatMapper: ClassMap<MovieStat>
    {
        public MovieStatMapper()
        {
            // Reason to not use nameof(MovieStat.MovieId) and nameof(MovieStat.WatchDurationMS) same as MetadataMapper.cs
            // (which would make it more maintainable and error proof), is to match csv file due to header starting with lowercase
            // In this instance: I did not want to change the header in csv file, and decided to match the code to it. Even though I would prefer changing the csv file's header
            Map(movieStat => movieStat.MovieId).Name("movieId");
            Map(movieStat => movieStat.WatchDurationMS).Name("watchDurationMs");
        }
    }
}
