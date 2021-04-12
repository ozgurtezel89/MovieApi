using CsvHelper.Configuration;
using Movie.CSVHandler.Models;

namespace Movie.CSVHandler.Mappers
{
    public sealed class MetadataMapper: ClassMap<MovieMetadata>
    {
        public MetadataMapper()
        {
            Map(metadata => metadata.Id).Name(nameof(MovieMetadata.Id));
            Map(metadata => metadata.MovieId).Name(nameof(MovieMetadata.MovieId));
            Map(metadata => metadata.Title).Name(nameof(MovieMetadata.Title));
            Map(metadata => metadata.Language).Name(nameof(MovieMetadata.Language));
            Map(metadata => metadata.Duration).Name(nameof(MovieMetadata.Duration));
            Map(metadata => metadata.ReleaseYear).Name(nameof(MovieMetadata.ReleaseYear));
        }
    }
}
