using Core.Dtos.Common;
using Infrastructure.Common;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos;

public class BaseRepo<T>(AppDbContext context) where T : class
{
    public DbSet<T> DbSet { get { return context.Set<T>(); } }
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public virtual IQueryable<T> GetAllQuery(PaginatedRequest searchReq, bool trackChanges = false)
    {
        IQueryable<T> query = context.Set<T>();

        //if (deletionType != DeletionType.All)
        //{
        //    query = query.Where(x => x.IsDeleted == (deletionType == DeletionType.Deleted));
        //}

        query = query.Search(searchReq.SearchTerm).Sort(searchReq.OrderBy);

        return trackChanges ? query : query.AsNoTracking();
    }

    public virtual async Task<PaginatedResponse<T>> GetPageAsync(PaginatedRequest searchReq, bool trackChanges = false)
    {
        var query = GetAllQuery(searchReq, trackChanges);

        var pageItems = await query
            .Skip((searchReq.PageNumber - 1) * searchReq.PageSize)
            .Take(searchReq.PageSize)
            .ToListAsync();

        var paginatedRes = new PaginatedResponse<T>
        {
            PageNumber = searchReq.PageNumber,
            PageSize = searchReq.PageSize,
            TotalItemsCount = await query.CountAsync(),
            Items = pageItems
        };

        return paginatedRes;
    }
    public T? GetById(int id,  bool trackChanges = false)
    {
        return context.Set<T>().Find(id);
    }
    public virtual async Task<bool> AddAsync(T item, bool trackChanges = false)
    {
        try
        {
            context.Add(item);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            //logger.LogError("generic repo add exception", ex.Message, ex);
            return false;
        }

    }
    public void Update(T item, bool trackChanges = false)
    {
        context.Update(item);
    }
    public void Delete(int id)
    {
        T? item = context.Set<T>().Find(id);
        if (item is not null)
        {
            context.Set<T>().Remove(item);
        }
    }
}
