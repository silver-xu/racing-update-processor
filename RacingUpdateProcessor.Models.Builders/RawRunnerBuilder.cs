using BuilderGenerator;
using Faker;
using RacingUpdateProcessor.Models;

namespace RaceUpdateProcessor.Models.Builders;

[BuilderFor(typeof(RawRunner))]

public partial class RawRunnerBuilder
{
    public RawRunnerBuilder WithCompleteFakerData()
    {
        var runnerId = RandomNumber.Next();
        var runnerTabNo = RandomNumber.Next();
        var runnerBarrier = RandomNumber.Next();
        var runnerName = Faker.Name.FullName();
        var runnerPrice = decimal.Divide(RandomNumber.Next(), 100);
        var runnerJockey = Faker.Name.FullName();
        var runnerTrainer = Faker.Name.FullName();

        return this
            .WithId(runnerId.ToString())
            .WithTabNo(runnerTabNo.ToString())
            .WithBarrier(runnerBarrier.ToString())
            .WithName(runnerName)
            .WithPrice(runnerPrice.ToString())
            .WithJockey(runnerJockey)
            .WithTrainer(runnerTrainer);
    }
}
