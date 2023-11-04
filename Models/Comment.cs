namespace MVCApplication.Models
{
    public class Comment
    {
        public string? CommentTitle { get; set; }
        public string? CommentedUser { get; set; }
        public string? CommentTime { get; set; }


        public Comment()
        {

        }

        public Comment(string? commentTitle, string? commentedUser, string? commentTime)
        {
            CommentTitle = commentTitle;
            CommentedUser = commentedUser;
            CommentTime = commentTime;
        }
    }
}
