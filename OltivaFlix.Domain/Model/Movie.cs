using System;

namespace OltivaFlix.Domain.Model
{
    public partial class Movie
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string Rated { get; set; }

        public string Released { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public string Director { get; set; }

        public string Writer { get; set; }

        public string Actors { get; set; }

        public string Plot { get; set; }

        public string Language { get; set; }

        public string Country { get; set; }

        public string Awards { get; set; }

        public Uri Poster { get; set; }

        public Rating[] Ratings { get; set; }

        public string Metascore { get; set; }

        public string ImdbRating { get; set; }

        public string ImdbVotes { get; set; }

        public string ImdbId { get; set; }

        public string Type { get; set; }

        public string Dvd { get; set; }

        public string BoxOffice { get; set; }

        public string Production { get; set; }

        public string Website { get; set; }

        public string Response { get; set; }
    }
}