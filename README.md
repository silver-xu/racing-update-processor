# Sportsbet Code Challenge
## Horse Race Update File Processing System

### Introductions:

The Application is designed and implemented to take a Horse Race Update File from external providers, perform basic validation, transform and publish internally so that it could be consumed by any other system. It was designed to run on AWS Ecosystem. The application is a Lambda subscribed to a SNS topic that will invoke by new message in the incoming message Queue, Download file from storage, Process the file, upload the file to storage then send a completion message to another SQS Queue (not implemented). When there is an exception or error occured upon processing the file, the incoming message is then moved to a predefined DLQ.

The aim of this project is to demonstrate my understanding of asynchronous Microservices and my c# skills. For the time sake the exact infrastructure is not included in the project and it cannot be deploy to the AWS Cloud just yet.

![image](https://user-images.githubusercontent.com/11991333/285088682-c3829a73-2f41-4ef0-8929-079f6b5a1a8e.png)

### Assumptions:
1. The upstream system only send Horse Race Update Files that contains changes to organised racing (for performance and size).
2. The external party could send messages to our incoming SQS Queue when it finished uploading the unprocessed files. This could be done via us providing a seperate Restful API but it is out of scope in this project.

### Design Considerations:

1. SQS/SNS was chosen for small footprint and simplicity. However because of Assumption 1, delivery ordering and guarantee are critical therefore I recommend choosing SQS FIFO Queue.
2. SQS has a maximum message limitation of 256KB therefore only filenames/urls are to be sent and handled in the message. An external storage is required to store files.
3. For simplicity the current implementation uses local file system as storage. In real life a cloud based storage solution such as S3 will be preferred.
4. The application will log to CloudWatch when deployed to AWS.
5. Monitoring can be done on observing the number of Dead-Letters in DLQ. We might have two options to ensure accuracy of the final snapshot of the race data, because of Assumption 2:
	1. Replay all subsequent message relating to the Race in order. This will need a seperate storage to keep track of what are the subsequent messages.
	2. Request the external party to send a snapshot of current Race instead of individual updates, to refresh our Race data.
  
### How to start in local:
#### Prerequisites:
* .Net Framework 6.0 sdk

#### Restore Packages
```
dotnet restore
```

#### Run Tests
```
dotnet test
```

#### Run in Lambda Test Tool
```
dotnet build
cd RacingUpdateProcessor.Lambda\bin\Debug\net6.0
```
Then
```
dotnet lambda-test-tool-6.0
```

Or simply run the project inside Visual Studio 2022

#### Sample SNS Content with working xml filename
```
{
  "Records": [
    {
      "EventSource": "aws:sns",
      "EventVersion": "1.0",
      "EventSubscriptionArn": "arn:{partition}:sns:EXAMPLE",
      "Sns": {
        "Type": "Notification",
        "MessageId": "95df01b4-ee98-5cb9-9903-4c221d41eb5e",
        "TopicArn": "arn:{partition}:sns:EXAMPLE",
        "Subject": "TestInvoke",
        "Message": "HorseRaceUpdate.xml",
        "Timestamp": "1970-01-01T00:00:00Z",
        "SignatureVersion": "1",
        "Signature": "EXAMPLE",
        "SigningCertUrl": "EXAMPLE",
        "UnsubscribeUrl": "EXAMPLE",
        "MessageAttributes": {
          "Test": {
            "Type": "String",
            "Value": "TestString"
          },
          "TestBinary": {
            "Type": "Binary",
            "Value": "TestBinary"
          }
        }
      }
    }
  ]
}
```