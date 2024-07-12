using KidsWallet.Persistence;
using KidsWallet.Persistence.Extensions;
using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Services.Abstraction;
using KidsWallet.Shared.Abstraction;
using KidsWallet.Shared.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace KidsWallet.Services;

public sealed class CrudOperationsService<TEntity>(KidsWalletDbContext kidsWalletDbContext, IClock clock)
    : ICrudOperationsService<TEntity> where TEntity : class, IAuditableEntity<Guid>
{
    private readonly IClock _clock = clock ?? throw new ArgumentNullException(paramName: nameof(clock));

    private readonly KidsWalletDbContext _kidsWalletDbContext =
        kidsWalletDbContext ?? throw new ArgumentNullException(paramName: nameof(kidsWalletDbContext));

    public async Task<TEntity?> GetByIdAsync(Guid id, bool throwWhenNotFound, CancellationToken cancellationToken,
        bool trackChanges = true)
    {
        TEntity? entity = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(useTracking: trackChanges)
            .FirstOrDefaultAsync(predicate: x => x.Id == id, cancellationToken: cancellationToken);

        if (entity is null && throwWhenNotFound)
        {
            throw new NotFoundException(id: id);
        }

        return entity;
    }

    public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, bool throwWhenNotFound,
        CancellationToken cancellationToken, bool trackChanges = true)
    {
        TEntity? entity = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(useTracking: trackChanges)
            .IncludeMany(includes: specification.ToIncludeExpression())
            .SingleOrDefaultAsync(predicate: specification.ToFilterExpression(), cancellationToken: cancellationToken);

        if (entity is null && throwWhenNotFound)
        {
            throw new NotFoundException(id: specification);
        }

        return entity;
    }

    public async Task<IReadOnlyCollection<TEntity>> GetManyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken, bool trackChanges = true)
    {
        IEnumerable<TEntity> entities = await _kidsWalletDbContext
            .Set<TEntity>()
            .Tracking(useTracking: trackChanges)
            .IncludeMany(includes: specification.ToIncludeExpression())
            .Where(predicate: specification.ToFilterExpression())
            .ToListAsync(cancellationToken: cancellationToken);

        return entities.ToList();
    }

    public async Task<TEntity> CreateAsync(Guid id, Func<TEntity> createEntityFunc, CancellationToken cancellationToken,
        bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id: id, throwWhenNotFound: false, cancellationToken: cancellationToken);

        if (dbEntity is not null)
        {
            throw new ConflictException(id: id);
        }

        TEntity entity = createEntityFunc();
        entity.CreatedAt = _clock.UtcNow;
        await _kidsWalletDbContext.AddAsync(entity: entity, cancellationToken: cancellationToken);
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken: cancellationToken);

        return entity;
    }

    public async Task UpdateAsync(Guid id, Func<TEntity, TEntity> updateEntityFunc, CancellationToken cancellationToken,
        bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id: id, throwWhenNotFound: true, cancellationToken: cancellationToken);
        TEntity entity = updateEntityFunc(arg: dbEntity!);
        entity.UpdatedAt = _clock.UtcNow;
        kidsWalletDbContext.Update(entity: updateEntityFunc(arg: entity));
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken, bool saveChanges = true)
    {
        TEntity? dbEntity = await GetByIdAsync(id: id, throwWhenNotFound: true, cancellationToken: cancellationToken);
        _kidsWalletDbContext.Remove(entity: dbEntity!);
        await _kidsWalletDbContext.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}