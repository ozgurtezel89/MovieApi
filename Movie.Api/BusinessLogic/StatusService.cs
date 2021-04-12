using Movie.Api.Models;
using Movie.CSVHandler;
using Movie.CSVHandler.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movie.Api.BusinessLogic
{
    public class StatusService : IStatusService
    {
        private readonly ICSVService _csvService;
        private readonly IMetadataService _metadataService;
        private string _fileName = "stats.csv";

        public StatusService(ICSVService csvService, IMetadataService metadataService)
        {
            _csvService = csvService ?? throw new ArgumentNullException(nameof(csvService));
            _metadataService = metadataService ?? throw new ArgumentNullException(nameof(metadataService));
        }

        public IEnumerable<StatViewModel> GetViewingStatistics()
        {
            var allStatuses = _csvService.ReadFromCSV<MovieStat>(_fileName);

            var mappedStatuses = MapToStatViewModel(allStatuses);

            var orderedByMostWatchedAndReleaseYear = OrderMovieStatsByMostWatchedAndReleaseYear(mappedStatuses);

            return orderedByMostWatchedAndReleaseYear;
        }

        private IEnumerable<StatViewModel> OrderMovieStatsByMostWatchedAndReleaseYear(IEnumerable<StatViewModel> movieStats)
        {
            var result = movieStats
                .OrderByDescending(x => x.Watches)
                .ThenByDescending(x => x.ReleaseYear);
            
            return result;
        }

        private List<StatViewModel> MapToStatViewModel(IEnumerable<MovieStat> movieStats)
        {
            List<StatViewModel> statViewModels = new List<StatViewModel>();
            IEnumerable<int> uniqueMovieStatIds = movieStats
                .Select(x => x.MovieId)
                .Distinct();

            foreach (var movieStatId in uniqueMovieStatIds)
            {
                var movieMetadata = _metadataService.GetMetadata(movieStatId);

                if (movieMetadata is null)
                {
                    continue;
                }

                var watches = CalculateMovieWatches(movieStats, movieStatId);
                var averageWatchDurationInSeconds = CalculateAverageWatchDurationInSeconds(movieStats, movieStatId);
                statViewModels.Add(new StatViewModel
                {
                    MovieId = movieStatId,
                    ReleaseYear = movieMetadata.ReleaseYear,
                    Title = movieMetadata.Title,
                    Watches = watches,
                    AverageWatchDurationSeconds = averageWatchDurationInSeconds
                });
            }

            return statViewModels;
        }

        private long CalculateMovieWatches(IEnumerable<MovieStat> movieStats, int movieId)
        {
            var result = movieStats.Where(x => x.MovieId == movieId).LongCount();

            return result;
        }

        private long CalculateAverageWatchDurationInSeconds(IEnumerable<MovieStat> movieStats, int movieId)
        {
            var allWatchDurationsInMiliSeconds = movieStats
                .Where(x => x.MovieId == movieId)
                .Select(x => x.WatchDurationMS);

            var totalWatchDurationsInMiliSeconds = allWatchDurationsInMiliSeconds.Sum();
            var averageWatchDurationsInMiliSeconds = totalWatchDurationsInMiliSeconds / allWatchDurationsInMiliSeconds.LongCount();
            var averageWatchDurationsInSeconds = averageWatchDurationsInMiliSeconds / 1000;

            return averageWatchDurationsInSeconds;
        }
    }
}
