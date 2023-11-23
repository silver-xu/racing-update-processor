using Amazon.Lambda.Annotations;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RacingUpdateProcessor.Business.Interfaces;
using static Amazon.Lambda.SNSEvents.SNSEvent;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace RacingUpdateProcessor.Lambda;

public class Function
{
    private readonly IRaceUpdateProcessor _raceUpdateProcessor;
    private readonly ILogger<Function> _logger;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function(IRaceUpdateProcessor raceUpdateProcessor,
        ILogger<Function> logger,
        IConfiguration configuration)
    {
        _configuration = configuration;
        _raceUpdateProcessor = raceUpdateProcessor;
        _logger = logger;
    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SNS event object and can be used 
    /// to respond to SNS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    [LambdaFunction]
    public async Task FunctionHandler(SNSEvent evnt, ILambdaContext context)
    {
        foreach (var record in evnt.Records)
        {
            await ProcessRecordAsync(record, context);
        }
    }

    private async Task ProcessRecordAsync(SNSRecord record, ILambdaContext context)
    {
        var rawRacingUpdateFileName = record.Sns.Message;
        var rawRacingUpdateUrl = Path.Join(_configuration["SourceFolder"], rawRacingUpdateFileName);
        var racingUpdateFileName = Path.ChangeExtension(rawRacingUpdateFileName, ".json");
        var racingUpdateUrl = Path.Join(_configuration["TargetFolder"], racingUpdateFileName);

        _logger.LogInformation($"Information: Processing Raw RacingUpdateFile {rawRacingUpdateFileName} will begin shortly");

        await _raceUpdateProcessor.Process(rawRacingUpdateUrl, racingUpdateUrl);

        _logger.LogInformation($"Information: Processing Raw RacingUpdateFile {rawRacingUpdateFileName} has completed");

        // TODO: Do interesting work based on the new message
        await Task.CompletedTask;
    }
}