using System.Globalization;
using System.Xml.Linq;
using Crawler.Core.Extensions;

namespace Crawler.Core.CentralBank;

public class CbrClient(HttpClient client)
{
    private readonly HttpClient _client = client;

    public async Task<IEnumerable<CurrencyQuote>> GetQuotesAsync(DateOnly? date)
    {
        Stream responseStream = await _client.GetStreamAsync(date is not null ?
            @$"/scripts/XML_daily.asp?date_req={date:dd/MM/yyyy}" :
            @"/scripts/XML_daily.asp");

        return XDocument.Load(responseStream).Descendants("Valute").Select(item =>
        {
            return new CurrencyQuote
            {
                Id = item.GetRequiredAttribute("ID").Value,
                NumCode = int.Parse(item.GetRequiredElement("NumCode").Value),
                CharCode = item.GetRequiredElement("CharCode").Value,
                Nominal = int.Parse(item.GetRequiredElement("Nominal").Value),
                Name = item.GetRequiredElement("Name").Value,
                Value = decimal.Parse(item.GetRequiredElement("Value").Value, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint),
                UnitRate = decimal.Parse(item.GetRequiredElement("VunitRate").Value, NumberStyles.AllowExponent | NumberStyles.AllowDecimalPoint),
            };
        });
    }

    public async Task<IEnumerable<CurrencyDetails>> GetDetailsAsync()
    {
        Stream responseStream = await _client.GetStreamAsync(@"/scripts/XML_valFull.asp");

        return XDocument.Load(responseStream).Descendants("Item").Select(item =>
        {
            var details = new CurrencyDetails
            {
                Id = item.GetRequiredAttribute("ID").Value,
                Name = item.GetRequiredElement("Name").Value,
                EnglishName = item.GetRequiredElement("EngName").Value,
                Nominal = int.Parse(item.GetRequiredElement("Nominal").Value),
                ParentCode = item.GetRequiredElement("ParentCode").Value.TrimEnd(),
                CharCode = item.GetRequiredElement("ISO_Char_Code").Value,
            };

            if (int.TryParse(item.GetRequiredElement("ISO_Num_Code").Value, out int numCode))
            {
                details.NumCode = numCode;
            }

            return details;
        });
    }
}
