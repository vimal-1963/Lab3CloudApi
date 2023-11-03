using Amazon.DynamoDBv2.DataModel;

namespace MVCApplication.Models
{
    [DynamoDBTable("SequenceTable")]
    public class Sequence
    {
        [DynamoDBHashKey]
        public string SequenceName { get; set; }

        [DynamoDBProperty]
        public int CurrentValue { get; set; }
    }
}
