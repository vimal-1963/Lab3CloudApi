using Amazon.DynamoDBv2.DataModel;

namespace MVCApplication.Models
{

    [DynamoDBTable("Movies")]
    public class Movie
    {
        [DynamoDBHashKey]
        public string MovieID { get; set; }

        [DynamoDBProperty("MovieComments")]
        public List<MovieComment> Comments { get; set; }

        [DynamoDBProperty("MovieDirectors")]
        public List<string> Directors { get; set; }

        [DynamoDBProperty("MovieGenre")]
        public string Genre { get; set; }

        [DynamoDBProperty("MovieRating")]
        public float Rating { get; set; }

        [DynamoDBProperty("MovieTitle")]
        public string Title { get; set; }

        [DynamoDBProperty("MovieURL")]
        public string URL { get; set; }

        [DynamoDBProperty("UploadedUserId")]
        public string UploadedUserId { get; set; }

        [DynamoDBProperty("MovieImageUrl")]
        public string MovieImageUrl { get; set; }
        
    }

    public class MovieComment
    {
        public MovieComment()
        {
          
        }

        public string Comment { get; set; }
        public string CommentedUser { get; set; }
        public string CommentTime { get; set; }
    }
}
