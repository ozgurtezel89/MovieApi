using Newtonsoft.Json;

namespace Movie.Api.Models
{
    public class StatViewModel
    {
        [JsonProperty(@"movieId")]
        public int MovieId { get; set; }

        [JsonProperty(@"title")]
        public string Title { get; set; }

        [JsonProperty(@"averageWatchDurationS")]
        public long AverageWatchDurationSeconds { get; set; }

        [JsonProperty(@"watches")]
        public long Watches { get; set; }

        [JsonProperty(@"releaseYear")]
        public int ReleaseYear { get; set; }
    }
}
