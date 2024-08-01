using System.Linq.Expressions;

namespace KidsWallet.Persistence.Model.Abstraction;

public interface ISpecification<TEntity> where TEntity : class
{
    Expression<Func<TEntity, bool>> ToFilterExpression();

    string[] ToIncludeExpression();
}