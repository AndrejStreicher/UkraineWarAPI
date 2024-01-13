using HtmlAgilityPack;
using UkraineWarAPI.Models;

namespace UkraineWarAPI;

public class UnderstandingWarScraper
{
    private readonly string _baseLink =
        "https://www.understandingwar.org/backgrounder/russian-offensive-campaign-assessment-";

    private HtmlDocument GetLatestWarReport()
    {
        return GetWarReportByDate(GetLatestReportDate());
    }

    private HtmlDocument GetWarReportByDate(DateTime dateTime)
    {
        var web = new HtmlWeb();
        var dateString = dateTime.ToString("MMMM-d-yyyy");
        var htmlDoc = web.Load(_baseLink + dateString);
        var fieldItems =
            htmlDoc.DocumentNode.SelectNodes(
                "//div[@class='field-item even' and contains(@property, 'content:encoded')]");
        htmlDoc.LoadHtml(fieldItems[0].InnerHtml);
        return htmlDoc;
    }

    private bool IsReportAvailable(string link)
    {
        var web = new HtmlWeb();
        var doc = web.Load(link);

        var pageTitleNode = doc.DocumentNode.SelectSingleNode("//title");
        var pageTitleString = pageTitleNode.InnerHtml.Trim();
        if (pageTitleString.Contains("404") || pageTitleString.Contains("denied")) return false;
        return true;
    }

    private DateTime GetLatestReportDate()
    {
        DateTime currentDate;
        string linkToLatestReport;
        var i = 1;
        do
        {
            currentDate = DateTime.Now.AddDays(-i);
            linkToLatestReport = _baseLink + currentDate.ToString("MMMM-d-yyyy").ToLower();
            i++;
        } while (!IsReportAvailable(linkToLatestReport));

        return currentDate;
    }

    private List<string> GetKeyTakeawaysList(HtmlDocument warReportDoc)
    {
        var keyTakeawaysList = new List<string>();
        var keyTakeawaysHtmlNodeCollection = warReportDoc.DocumentNode.SelectNodes("/ul[1]/li");
        foreach (var takeaway in keyTakeawaysHtmlNodeCollection)
        {
            var textContent = takeaway.InnerText.Trim();
            keyTakeawaysList.Add(textContent);
        }

        return keyTakeawaysList;
    }

    public UWKeyTakeawaysModel GetLatestKeyTakeaways()
    {
        var latestReportDate = GetLatestReportDate().ToString("MMMM-d-yyyy");
        var linkToLatestReport = _baseLink + latestReportDate.ToLower();
        return new UWKeyTakeawaysModel
        {
            Date = latestReportDate,
            Link = linkToLatestReport,
            KeyTakeaways = GetKeyTakeawaysList(GetLatestWarReport())
        };
    }

    public UWKeyTakeawaysModel GetKeyTakeawaysByDate(DateTime dateTime)
    {
        var requestedTimeString = dateTime.ToString("MMMM-d-yyyy").ToLower();
        var linkToRequestedReport = _baseLink + requestedTimeString;
        if (!IsReportAvailable(linkToRequestedReport)) return null!;
        return new UWKeyTakeawaysModel
        {
            Date = requestedTimeString,
            Link = linkToRequestedReport,
            KeyTakeaways = GetKeyTakeawaysList(GetWarReportByDate(dateTime))
        };
    }
}