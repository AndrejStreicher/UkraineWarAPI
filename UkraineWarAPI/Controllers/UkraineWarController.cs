using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UkraineWarAPI.Controllers;

[Route("[controller]")]
[ApiController]
[AllowAnonymous]
public class UkraineWarController : ControllerBase
{
    private readonly ILoggerManager _logger;
    private readonly UnderstandingWarScraper _understandingWarScraper = new();

    public UkraineWarController(ILoggerManager logger)
    {
        _logger = logger;
    }

    [HttpGet("latesttakeaways", Name = "GetLatestKeyTakeaways")]
    public IActionResult GetLatestKeyTakeaways()
    {
        _logger.LogInfo("LatestTakeaways endpoint used.");
        var latestTakeaways = _understandingWarScraper.GetLatestKeyTakeaways();
        return Ok(latestTakeaways);
    }

    [HttpGet("keytakeaways/{dateTimeString}", Name = "GetKeyTakeawaysByDate")]
    public IActionResult GetTakeawaysByDate(string dateTimeString)
    {
        _logger.LogInfo("Key takeaways by date endpoint used.");
        var dateTime = DateTime.Parse(dateTimeString);
        var keyTakeawaysModel = _understandingWarScraper.GetKeyTakeawaysByDate(dateTime);
        if (keyTakeawaysModel == null) return NotFound($"No report found for the date: {dateTimeString}");
        return Ok(keyTakeawaysModel);
    }
}