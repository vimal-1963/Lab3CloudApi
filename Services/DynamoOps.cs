using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MVCApplication.Models;

namespace MVCApplication.Services
{
    public class DynamoOps
    {
       
         AmazonDynamoDBClient client = Helper.dynamoDBClient;

        public async Task<List<Movie>> GetMoviesByGenreAsync(string genre)
        {
            var context = new DynamoDBContext(client);
            var queryRequest = new QueryRequest
            {
                TableName = "Movies", // Your DynamoDB table name
                IndexName = "MovieGenre-index", // Your GSI name
                KeyConditionExpression = "MovieGenre = :genre",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":genre", new AttributeValue { S = genre } }
            }
            };

            var queryResponse = await client.QueryAsync(queryRequest);

            var movies = new List<Movie>();
            foreach (var item in queryResponse.Items)
            {
                var movie = context.FromDocument<Movie>(Document.FromAttributeMap(item));
                movies.Add(movie);
            }

            return movies;
        }

        public async Task<List<Movie>> GetMoviesByRatingAsync()
        {
            var context = new DynamoDBContext(client);
            var scanRequest = new ScanRequest
            {
                TableName = "Movies",
                FilterExpression = "MovieRating > :rating",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
    {
        { ":rating", new AttributeValue { N = "9" } }
    }
            };

            var scanResponse = await client.ScanAsync(scanRequest);

            var movies = new List<Movie>();
            foreach (var item in scanResponse.Items)
            {
                var movie = context.FromDocument<Movie>(Document.FromAttributeMap(item));
                movies.Add(movie);
            }
            // Sort the movies by MovieRating in ascending order
            movies = movies.OrderByDescending(m => m.Rating).ToList();
            return movies;
        }

        internal async Task<int> GetSequence()
        {
            var context = new DynamoDBContext(client);

            var sequenceName = "movie_sequence";
            var sequence = await context.LoadAsync<Sequence>(sequenceName);

            if (sequence != null)
            {
                sequence.CurrentValue++;
                await context.SaveAsync(sequence);
                // The updated sequence value is now available as sequence.CurrentValue
                return sequence.CurrentValue;
            }
            return 0;
        }

        internal async Task SaveNewMovie(Movie movieEntity)
        {
            var context = new DynamoDBContext(client);
            // Save the data to DynamoDB
            await context.SaveAsync(movieEntity);
        }
    }
}
