namespace Crawler.Core.CentralBank;

public class CurrencyDetails
{
    /// <summary>
    /// Внутренний уникальный код валюты.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Название валюты.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Название валюты на английском.
    /// </summary>
    public string? EnglishName { get; set; }

    /// <summary>
    /// Номинал в единицах.
    /// </summary>
    public int Nominal { get; set; }

    /// <summary>
    /// Внутренний уникальный код валюты, которая являлась базовой(предыдущей) для данной валюты.
    /// </summary>
    public string? ParentCode { get; set; }

    /// <summary>
    /// Цифровой код валюты.
    /// </summary>
    public int NumCode { get; set; }

    /// <summary>
    /// Алфавитный код валюты.
    /// </summary>
    public string? CharCode { get; set; }
}
