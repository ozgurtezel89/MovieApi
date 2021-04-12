using Newtonsoft.Json;

namespace Movie.Api.Models
{
    public class MetadataViewModel
    {
        [JsonProperty(@"movieId")]
        public int MovieId { get; set; }

        [JsonProperty(@"title")]
        public string Title { get; set; }

        [JsonProperty(@"language")]
        public string Language { get; set; }

        [JsonProperty(@"duration")]
        public string Duration { get; set; }

        [JsonProperty(@"releaseYear")]
        public int ReleaseYear { get; set; }
    }
}
