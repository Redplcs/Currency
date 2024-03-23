using Microsoft.EntityFrameworkCore;
using Storage.Database;

namespace Storage.Core;

public interface ICurrencyExchangeRateRepository
{
    void Create(CurrencyExchangeRate currency);
    void Remove(CurrencyExchangeRate currency);
    void Update(CurrencyExchangeRate currency);

    CurrencyExchangeRate? Find(Predicate<CurrencyExchangeRate> predicate);
    IEnumerable<CurrencyExchangeRate> GetAll();
    IQueryable<CurrencyExchangeRate> GetByDate(DateOnly from);
    IQueryable<CurrencyExchangeRate> GetByDate(DateOnly from, DateOnly to);
    CurrencyExchangeRate GetById(string? id);
}

public class CurrencyExchangeRateRepository(StorageContext context) : ICurrencyExchangeRateRepository
{
    private readonly StorageContext _context = context;

    public void Create(CurrencyExchangeRate currency)
    {
        _context.ExchangeRates.Add(currency);
        _context.SaveChanges();
    }

    public CurrencyExchangeRate? Find(Predicate<CurrencyExchangeRate> predicate)
    {
        return _context.ExchangeRates.FirstOrDefault(item => predicate(item));
    }

    public IEnumerable<CurrencyExchangeRate> GetAll()
    {
        return _context.ExchangeRates;
    }

    public IQueryable<CurrencyExchangeRate> GetByDate(DateOnly from)
    {
        return GetByDate(from, DateOnly.FromDateTime(DateTime.Now));
    }

    public IQueryable<CurrencyExchangeRate> GetByDate(DateOnly from, DateOnly to)
    {
        return _context.ExchangeRates.Where(item => from < item.Date && item.Date < to);
    }

    public CurrencyExchangeRate GetById(string? id)
    {
        return _context.ExchangeRates.First(item => item.CurrencyId == id);
    }

    public void Remove(CurrencyExchangeRate currency)
    {
        _context.ExchangeRates.Remove(currency);
        _context.SaveChanges();
    }

    public void Update(CurrencyExchangeRate currency)
    {
        _context.ExchangeRates.Entry(currency).State = EntityState.Modified;
        _context.SaveChanges();
    }
}
