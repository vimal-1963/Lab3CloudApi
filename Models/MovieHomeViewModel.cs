namespace MVCApplication.Models
{
    public class MovieHomeViewModel
    {
        public List<Movie> TopRatedMovies { get; set; }
        public List<Movie> SciFiMovies { get; set; }
        public List<Movie> ActionMovies { get; set; }
        public List<Movie> ComedyMovies { get; set; }
        public List<Movie> HorrorMovies { get; set; }
    }
}
