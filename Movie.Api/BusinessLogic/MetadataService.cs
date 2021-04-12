using Movie.Api.Models;
using Movie.CSVHandler;
using Movie.CSVHandler.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Movie.Api.BusinessLogic
{
    public class MetadataService : IMetadataService
    {
        private readonly ICSVService _csvService;
        private static List<MetadataViewModel> _database;
        private readonly string _fileName = "metadata.csv";

        public MetadataService(ICSVService csvService)
        {
            _csvService = csvService ?? throw new ArgumentNullException(nameof(csvService));
            _database = new List<MetadataViewModel>();
        }

        public IEnumerable<MetadataViewModel> GetMetadatas(int movieId)
        {
            var results = _csvService.ReadFromCSV<MovieMetadata>(_fileName);
            var filteredByMovieId = results.Where(x => x.MovieId == movieId);
            var filteredHighestId = FilterToLatestPieceOfMetadataWhenMultipleRecordsForGivenLanguage(filteredByMovieId);
            var orderedByLanguage = OrderResultsAlphabeticallyByLanguage(filteredHighestId);
            return MapToMetaDataViewModel(orderedByLanguage);
        }

        private IEnumerable<MovieMetadata> FilterToLatestPieceOfMetadataWhenMultipleRecordsForGivenLanguage(IEnumerable<MovieMetadata> metadatas)
        {
            var result = metadatas
                .GroupBy(x => x.Language)
                .Distinct()
                .Select(group => group.First())
                .OrderByDescending(x => x.Id)
                .ThenByDescending(x => x.Language);

            return result;
        }

        private IEnumerable<MovieMetadata> OrderResultsAlphabeticallyByLanguage(IEnumerable<MovieMetadata> metadatas)
        {
            var result = metadatas
                .OrderBy(x => x.Language);

            return result;
        }

        private List<MetadataViewModel> MapToMetaDataViewModel(IEnumerable<MovieMetadata> metadatas)
        {
            List<MetadataViewModel> results = new List<MetadataViewModel>();

            foreach (var metadata in metadatas)
            {
                results.Add(MapToMetaDataViewModel(metadata));
            }

            return results;
        }

        private MetadataViewModel MapToMetaDataViewModel(MovieMetadata metadata)
        {
            if (metadata is null)
            {
                return null;
            }

            MetadataViewModel result = new MetadataViewModel
            {
                Duration = FormatTimeSpan(metadata.Duration),
                Language = metadata.Language,
                MovieId = metadata.MovieId,
                ReleaseYear = metadata.ReleaseYear,
                Title = metadata.Title
            };

            return result;
        }

        // Formatting timespan value for Duration to match the given sample 1:49:23 rather than 01:49:23 with leading zero
        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            var timeSpanString = timeSpan.ToString();
            var timeSpanFirstChar = timeSpanString.ToCharArray()[0];

            if (timeSpanFirstChar == '0')
            {
                return timeSpanString.Remove(0, 1).ToString();
            }

            return timeSpanString;
        }

        public MetadataViewModel SaveMetadata(MetadataViewModel metadataViewModel)
        {
            // As per the requested approach only adding this to a list called _database.
            _database.Add(metadataViewModel);
            return metadataViewModel;
        }

        public MetadataViewModel GetMetadata(int movieId)
        {
            var result = _csvService
                .ReadFromCSV<MovieMetadata>(_fileName)
                .Where(x => x.MovieId == movieId)
                .FirstOrDefault();

            var mappedResult = MapToMetaDataViewModel(result);

            return mappedResult;
        }
    }
}
