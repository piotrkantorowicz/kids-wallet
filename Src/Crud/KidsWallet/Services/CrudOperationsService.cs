using KidsWallet.Persistence;
using KidsWallet.Persistence.Extensions;
using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Services.Abstraction;
using KidsWallet.Services.Exceptions;
using KidsWallet.Shared.Abstraction;

using Microsoft.EntityFrameworkCore;

namespace KidsWallet.Services;

public sealed class CrudOperationsService<TEntity>(KidsWalletDbContext kidsWalletDbContext, IClock clock)
    : ICrudOperationsService<TEntity> where TEntity : class, IAuditableEntity<Guid>
{
    private readonly IClock _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    
    private readonly KidsWalletDbContext _kidsWalletDbContext =
        kidsWalletDbContext ?? throw new ArgumentNullException(nameof(kidsWalletDbContext));
    
    public async Task<TEntity?> GetByIdAsync(Guid id, bool throwWhenNotFound, CancellationToken cancellationToken,
        bool trackChanges = true)
    {
        TEntity? entity = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(trackChanges)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (entity is null && throwWhenNotFound)
        {
            throw new NotFoundException(id);
        }
        
        return entity;
    }
    
    public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, bool throwWhenNotFound,
        CancellationToken cancellationToken, bool trackChanges = true)
    {
        TEntity? entity = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(trackChanges)
            .IncludeMany(specification.ToIncludeExpression())
            .SingleOrDefaultAsync(specification.ToFilterExpression(), cancellationToken);
        
        if (entity is null && throwWhenNotFound)
        {
            throw new NotFoundException(specification);
        }
        
        return entity;
    }
    
    public async Task<IReadOnlyCollection<TEntity>> GetManyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken, bool trackChanges = true)
    {
        IEnumerable<TEntity> entities = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(trackChanges)
            .IncludeMany(specification.ToIncludeExpression())
            .Where(specification.ToFilterExpression())
            .ToListAsync(cancellationToken);
        
        return entities.ToList();
    }
    
    public async Task<TEntity> CreateAsync(Guid id, Func<TEntity> createEntityFunc, CancellationToken cancellationToken,
        bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id, false, cancellationToken);
        
        if (dbEntity is not null)
        {
            throw new AlreadyExistsException(id);
        }
        
        TEntity entity = createEntityFunc();
        entity.CreatedAt = _clock.UtcNow;
        await _kidsWalletDbContext.AddAsync(entity, cancellationToken);
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
    
    public async Task UpdateAsync(Guid id, Func<TEntity, TEntity> updateEntityFunc, CancellationToken cancellationToken,
        bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id, true, cancellationToken);
        TEntity entity = updateEntityFunc(dbEntity!);
        entity.UpdatedAt = _clock.UtcNow;
        kidsWalletDbContext.Update(updateEntityFunc(entity));
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken, bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id, true, cancellationToken);
        _kidsWalletDbContext.Remove(dbEntity!);
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken);
    }
}