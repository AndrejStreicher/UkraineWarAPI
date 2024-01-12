using UkraineWarAPI;

namespace UnitTests;

public class UnitTest1
{
    private UnderstandingWarScraper _understandingWarScraper = new UnderstandingWarScraper();
    [Fact]
    public void GetJsonTakeawaysTest()
    {
        var takeaways = _understandingWarScraper.GetLatestKeyTakeaways();
    }
}