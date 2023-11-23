namespace RacingUpdateProcessor.Business.Interfaces;

public interface IRaceUpdateProcessor
{
    public Task Process(string sourceUrl, string targetUrl);
}
