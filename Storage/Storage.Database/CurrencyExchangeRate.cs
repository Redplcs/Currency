namespace Storage.Database;

public class CurrencyExchangeRate
{
    /// <summary>
    /// Id записи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Внутренний уникальный код валюты, которая являлась базовой(предыдущей) для данной валюты.
    /// </summary>
    public string? BaseCurrencyId { get; set; }

    /// <summary>
    /// Внутренний уникальный код валюты.
    /// </summary>
    public string? CurrencyId { get; set; }

    /// <summary>
    /// Дата.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Значение курса валюты за дату.
    /// </summary>
    public decimal Value { get; set; }
}
