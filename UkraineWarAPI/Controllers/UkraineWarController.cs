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
    private readonly UnderstandingWarScraper _understandingWarScraper = new();
    private readonly ILoggerManager _logger;

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
}