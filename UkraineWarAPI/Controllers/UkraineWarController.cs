using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UkraineWarAPI.Models;

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
    public UWKeyTakeawaysModel GetLatestKeyTakeaways()
    {
        _logger.LogInfo("LatestTakeaways endpoint used.");
        return _understandingWarScraper.GetLatestKeyTakeaways();
    }

    [HttpGet("keytakeaways/{dateTimeString}", Name = "GetKeyTakeawaysByDate")]
    public UWKeyTakeawaysModel GetTakeawaysByDate(string dateTimeString)
    {
        _logger.LogInfo("Key takeaways by date endpoint used.");
        var dateTime = DateTime.Parse(dateTimeString);
        return _understandingWarScraper.GetKeyTakeawaysByDate(dateTime);
    }
}