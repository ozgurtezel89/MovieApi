using System;

namespace Movie.CSVHandler.Models
{
    public class MovieMetadata
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public TimeSpan Duration { get; set; }

        public int ReleaseYear { get; set; }
    }
}
