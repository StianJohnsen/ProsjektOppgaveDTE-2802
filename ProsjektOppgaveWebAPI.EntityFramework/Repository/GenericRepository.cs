using Microsoft.EntityFrameworkCore;
namespace ProsjektOppgaveWebAPI.EntityFramework.Repository;

public class GenericRepository<T>: IGenericRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    
    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    { 
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T> GetById(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public IQueryable<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking();
    }
}