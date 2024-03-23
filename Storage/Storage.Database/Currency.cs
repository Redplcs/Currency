namespace Storage.Database;

public class Currency
{
    /// <summary>
    /// ID записи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название валюты.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Название валюты на английском.
    /// </summary>
    public string? EnglishName { get; set; }

    /// <summary>
    /// Внутренний уникальный код валюты, которая являлась базовой(предыдущей) для данной валюты.
    /// </summary>
    public string? RId { get; set; }

    /// <summary>
    /// Трёхбуквенный алфавитный код валюты в стандарте ISO.
    /// </summary>
    public string? IsoCode { get; set; }
}
