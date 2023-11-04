using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApplication.Models;

namespace MVCApplication.Services
{
    public class S3Ops
    {

        internal async Task<string> saveFiles(IFormFile file)
        {
            return await UploadObjectToS3AndGetUrl(file, "movie-data-vv");
        }

        private async Task<string> UploadObjectToS3AndGetUrl(IFormFile file, string bucketName)
        {
            IAmazonS3 client = Helper.GetS3Client();
            // Create a TransferUtility instance with the configured S3 client
            var transferUtility = new TransferUtility(client);

            // Upload the file to S3
            await transferUtility.UploadAsync(file.OpenReadStream(), bucketName, file.FileName);

            // Construct and return the S3 URL
            string s3Url = $"https://{bucketName}.s3.amazonaws.com/{file.FileName}";

            return s3Url;
        }

        internal async Task<bool>deletes3URL(string s3ObjectURLArg)
        {
            IAmazonS3 client = Helper.GetS3Client();
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = "movie-data-vv",
                Key = s3ObjectURLArg
            };
            try
            {
                await client.DeleteObjectAsync(deleteRequest);
                return true;
            }catch(Exception ex) { 
                Console.WriteLine(ex.ToString());   
                return false;
            }
            
            
        }
    }
}
