using Crawler.Core.CentralBank;
using Crawler.Database;

namespace Crawler.Core;

public record struct DayQuotes(DateOnly Date, IEnumerable<CurrencyQuote> Quotes);

public class CrawlerService(HangfireContext context, CbrClient client)
{
    private readonly HangfireContext _context = context;
    private readonly CbrClient _client = client;

    public async Task<IEnumerable<CurrencyDetails>> GetCurrencies()
    {
        return await _client.GetDetailsAsync();
    }

    public async Task<IEnumerable<CurrencyQuote>> GetExchangeRate(DateOnly date)
    {
        return await _client.GetQuotesAsync(date);
    }

    public async Task<(IEnumerable<CurrencyDetails>, IEnumerable<DayQuotes>)> Fetch()
    {
        DateOnly dateNow = DateOnly.FromDateTime(DateTime.Now);

        // Получаем дату, на которой остановились в предыдущий раз, либо начинаем запрашивать за два года
        DateOnly from = _context.Timestamps.OrderByDescending(item => item.Id).FirstOrDefault()?.Date ?? dateNow.AddYears(-2);

        var currencies = await _client.GetDetailsAsync();
        var response = new List<DayQuotes>();

        for (DateOnly i = from; i <= dateNow; i = i.AddDays(1))
        {
            response.Add(new DayQuotes(i, await _client.GetQuotesAsync(i)));
        }

        _context.Timestamps.Add(new Timestamp
        {
            Id = _context.Timestamps.Count(),
            Date = dateNow,
        });

        _context.SaveChanges();

        return (currencies, response);
    }
}
