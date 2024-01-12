using HtmlAgilityPack;
using UkraineWarAPI.Models;

namespace UkraineWarAPI;

public class UnderstandingWarScraper
{
    private readonly string _baseLink =
        "https://www.understandingwar.org/backgrounder/russian-offensive-campaign-assessment-";

    private HtmlDocument GetLatestWarReport()
    {
        var web = new HtmlWeb();
        var htmlDoc = web.Load(_baseLink + GetLatestReportDate());
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

    private string GetLatestReportDate()
    {
        string currentDate;
        string linkToLatestReport;
        var i = 1;
        do
        {
            currentDate = DateTime.Now.AddDays(-i).ToString("MMMM-d-yyyy");
            linkToLatestReport = _baseLink + currentDate.ToLower();
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
        var latestReportDate = GetLatestReportDate();
        var linkToLatestReport = _baseLink + latestReportDate.ToLower();
        return new UWKeyTakeawaysModel
        {
            Date = latestReportDate,
            Link = linkToLatestReport,
            KeyTakeaways = GetKeyTakeawaysList(GetLatestWarReport())
        };
    }
}