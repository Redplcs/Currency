using Microsoft.EntityFrameworkCore;
using Storage.Database;

namespace Storage.Core;

public interface ICurrencyRepository
{
    void Create(Currency currency);
    void Remove(Currency currency);
    void Update(Currency currency);

    Currency? Find(Predicate<Currency> predicate);
    IEnumerable<Currency> GetAll();
    Currency GetById(int id);
}

public class CurrencyRepository(StorageContext context) : ICurrencyRepository
{
    private readonly StorageContext _context = context;

    public void Create(Currency currency)
    {
        _context.Currencies.Add(currency);
        _context.SaveChanges();
    }

    public Currency? Find(Predicate<Currency> predicate)
    {
        return _context.Currencies.FirstOrDefault(item => predicate(item));
    }

    public IEnumerable<Currency> GetAll()
    {
        return _context.Currencies;
    }

    public Currency GetById(int id)
    {
        return _context.Currencies.First(item => item.Id == id);
    }

    public void Remove(Currency currency)
    {
        _context.Currencies.Remove(currency);
        _context.SaveChanges();
    }

    public void Update(Currency currency)
    {
        _context.Currencies.Entry(currency).State = EntityState.Modified;
        _context.SaveChanges();
    }
}
