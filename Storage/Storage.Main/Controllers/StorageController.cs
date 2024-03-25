using Microsoft.AspNetCore.Mvc;
using Storage.Core;
using Storage.Database;

namespace Storage.Main.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StorageController(ICurrencyExchangeRateRepository exchangeRate) : ControllerBase
{
    private readonly ICurrencyExchangeRateRepository _exchangeRate = exchangeRate;

    [HttpGet]
    public IActionResult GetExchangeRatesByDate([FromQuery] DateOnly minDate, [FromQuery] DateOnly maxDate)
    {
        List<CurrencyExchangeRate> exchangeRates = [.. _exchangeRate.GetByDate(minDate, maxDate)];
        return Ok(exchangeRates);
    }

    [HttpGet]
    public IActionResult GetExchangeRatesById([FromQuery] string currencyId)
    {
        CurrencyExchangeRate exchangeRate = _exchangeRate.GetById(currencyId);
        return Ok(exchangeRate);
    }
}
