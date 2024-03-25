using Crawler.Core;
using Crawler.Core.CentralBank;
using Crawler.Database;
using Microsoft.AspNetCore.Mvc;

namespace Crawler.Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CrawlerController(CrawlerService crawler) : ControllerBase
{
    private readonly CrawlerService _crawler = crawler;

    [HttpGet]
    public async Task<IActionResult> GetExchangeRates()
    {
        (IEnumerable<CurrencyDetails>, IEnumerable<DayQuotes>) data = await _crawler.Fetch();

        var response = new List<dynamic>();

        foreach (DayQuotes dayQuotes in data.Item2)
        {
            foreach (CurrencyQuote quote in dayQuotes.Quotes)
            {
                CurrencyDetails currency = data.Item1.First(currency => currency.Id == quote.Id);

                response.Add(new
                {
                    Name = currency.Name,
                    EnglishName = currency.EnglishName,
                    CharCode = currency.CharCode,
                    Nominal = quote.Nominal,
                    Date = dayQuotes.Date,
                    Value = quote.Value,
                });
            }
        }

        return Ok(response);
    }

}
