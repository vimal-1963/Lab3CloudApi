namespace MVCApplication.Models
{
    public class AddMovieModel
    {
        public string Title { get; set; }
        public string DirectorName { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Rating { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile Video { get; set; }
    }
}
