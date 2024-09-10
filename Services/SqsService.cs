using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using Motos.Settings;

namespace Motos.Services
{
    public class SqsService
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl;

        public SqsService(IOptions<AwsSettings> awsSettings)
        {
            var credentials = new Amazon.Runtime.BasicAWSCredentials(awsSettings.Value.AccessKey, awsSettings.Value.SecretKey);
            var config = new AmazonSQSConfig
            {
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsSettings.Value.Region)
            };
            _sqsClient = new AmazonSQSClient(credentials, config);
            _queueUrl = awsSettings.Value.SQSQueueUrl;

        }

        public async Task SendMessageAsync(string message)
        {
            var sendMessageRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
            };
            await _sqsClient.SendMessageAsync(sendMessageRequest);
        }
    }
}
