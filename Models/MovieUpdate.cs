namespace MVCApplication.Models
{
    public class MovieUpdate
    {
        
            public int MovieID { get; set; }
            public string Title { get; set; }
            public List<string> Directors { get; set; }
            public string Genre { get; set; }
            public double Rating { get; set; }
            public List<String> Comments { get; set; }
        }
    
}
