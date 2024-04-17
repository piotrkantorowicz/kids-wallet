using KidsWallet.Persistence.Model.Abstraction;

namespace KidsWallet.Services.Abstraction;

public interface ICrudOperationsService<TEntity> where TEntity : class, IAuditableEntity<Guid>
{
    Task<TEntity?> GetByIdAsync(Guid id, bool throwWhenNotFound, CancellationToken cancellationToken,
        bool trackChanges = true);
    
    Task<TEntity?> GetAsync(ISpecification<TEntity> specification, bool throwWhenNotFound,
        CancellationToken cancellationToken, bool trackChanges = true);
    
    Task<IReadOnlyCollection<TEntity>> GetManyAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken, bool trackChanges = true);
    
    Task<TEntity> CreateAsync(Guid id, Func<TEntity> createEntityFunc, CancellationToken cancellationToken, bool saveChanges = true);
    
    Task UpdateAsync(Guid id, Func<TEntity, TEntity> updateEntityFunc, CancellationToken cancellationToken,bool saveChanges = true);
    
    Task DeleteAsync(Guid id, CancellationToken cancellationToken,bool saveChanges = true);
}