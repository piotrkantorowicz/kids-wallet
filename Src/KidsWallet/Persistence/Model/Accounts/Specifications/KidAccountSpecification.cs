using System.Linq.Expressions;

using KidsWallet.Extensions;
using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Shared.Expressions.And;

namespace KidsWallet.Persistence.Model.Accounts.Specifications;

public sealed record KidAccountSpecification(
    Guid? Id = default,
    Guid? KidWalletId = default,
    string? Name = default,
    decimal? Balance = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccountOperations = default) : ISpecification<KidAccount>
{
    public Expression<Func<KidAccount, bool>> ToFilterExpression()
    {
        List<Expression<Func<KidAccount, bool>>> predicates = [];
        
        if (Id is not null)
        {
            predicates.Add(x => x.Id == Id);
        }
        
        if (KidWalletId is not null)
        {
            predicates.Add(x => x.KidWalletId == KidWalletId);
        }
        
        if (Name is not null)
        {
            predicates.Add(x => x.Name == Name);
        }
        
        if (Balance is not null)
        {
            predicates.Add(x => x.Balance == Balance);
        }
        
        if (CreatedAt is not null)
        {
            predicates.Add(x => x.CreatedAt == CreatedAt);
        }
        
        if (UpdatedAt is not null)
        {
            predicates.Add(x => x.UpdatedAt == UpdatedAt);
        }
        
        return predicates.Count != 0 ? predicates.Aggregate(AndExpression<KidAccount>.And) : x => true;
    }
    
    public string[] ToIncludeExpression()
    {
        ICollection<string> includes = [];
        
        if (IncludeKidAccountOperations is true)
        {
            includes.Add(nameof(KidAccount.KidAccountOperations));
        }
        
        return includes.ToArray();
    }
    
    public override string ToString()
    {
        return this.Print();
    }
}