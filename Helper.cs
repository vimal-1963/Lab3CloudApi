using Amazon;
using Amazon.DynamoDBv2;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace MVCApplication
{
    public class Helper
    {
        public readonly static IAmazonS3 s3Client;
        public readonly static AmazonDynamoDBClient dynamoDBClient;
        static Helper()
        {
            s3Client = GetS3Client();
            dynamoDBClient = GetDynamoDBClient();
        }

        private static IAmazonS3 GetS3Client()
        {
            string accessKey = ConfigurationManager.AppSettings["accessid"];
            string awsSecretKey = ConfigurationManager.AppSettings["password"];
            return new AmazonS3Client(accessKey, awsSecretKey, RegionEndpoint.CACentral1);
        }

        private static AmazonDynamoDBClient GetDynamoDBClient()
        {
            string accessKey = ConfigurationManager.AppSettings["accessId"];
            string awsSecretKey = ConfigurationManager.AppSettings["password"];
            return new AmazonDynamoDBClient(accessKey, awsSecretKey, RegionEndpoint.CACentral1);
        }
    }
}
