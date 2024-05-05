using System.Linq.Expressions;

using KidsWallet.Extensions;
using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Persistence.Model.Accounts;
using KidsWallet.Shared.Expressions.And;

namespace KidsWallet.Persistence.Model.Wallets.Specifications;

public sealed record KidWalletSpecification(
    Guid? Id = default,
    Guid? KidId = default,
    string? Name = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default,
    bool? IncludeKidAccounts = default,
    bool? IncludeKidAccountOperations = default) : ISpecification<KidWallet>
{
    public Expression<Func<KidWallet, bool>> ToFilterExpression()
    {
        List<Expression<Func<KidWallet, bool>>> predicates = [];
        
        if (Id is not null)
        {
            predicates.Add(x => x.Id == Id);
        }
        
        if (KidId is not null)
        {
            predicates.Add(x => x.KidId == KidId);
        }
        
        if (Name is not null)
        {
            predicates.Add(x => x.Name == Name);
        }
        
        if (CreatedAt is not null)
        {
            predicates.Add(x => x.CreatedAt == CreatedAt);
        }
        
        if (UpdatedAt is not null)
        {
            predicates.Add(x => x.UpdatedAt == UpdatedAt);
        }
        
        return predicates.Count != 0 ? predicates.Aggregate(AndExpression<KidWallet>.And) : x => true;
    }
    
    public string[] ToIncludeExpression()
    {
        ICollection<string> includes = [];
        
        if (IncludeKidAccounts is true)
        {
            includes.Add(nameof(KidWallet.KidAccounts));
        }
        
        if (IncludeKidAccountOperations is true)
        {
            includes.Add($"{nameof(KidWallet.KidAccounts)}.{nameof(KidAccount.KidAccountOperations)}");
        }
        
        return includes.ToArray();
    }
    
    public override string ToString()
    {
        return this.Print();
    }
}