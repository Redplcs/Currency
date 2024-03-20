namespace Crawler.Core.CentralBank;

public class CurrencyQuote
{
    /// <summary>
    /// Внутренний уникальный код валюты.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Цифровой код валюты.
    /// </summary>
    public int NumCode { get; set; }

    /// <summary>
    /// Алфавитный код валюты.
    /// </summary>
    public string? CharCode { get; set; }

    /// <summary>
    /// Номинал в единицах.
    /// </summary>
    public int Nominal { get; set; }

    /// <summary>
    /// Название валюты.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Цена номинала.
    /// </summary>
    public decimal Value { get; set; }

    /// <summary>
    /// Цена за одну единицу валюты.
    /// </summary>
    public decimal UnitRate { get; set; }
}
