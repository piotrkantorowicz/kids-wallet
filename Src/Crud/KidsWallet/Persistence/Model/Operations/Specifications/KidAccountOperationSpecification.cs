using System.Linq.Expressions;

using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Shared.Expressions.And;

namespace KidsWallet.Persistence.Model.Operations.Specifications;

public sealed record KidAccountOperationSpecification(
    Guid? Id = default,
    Guid? KidAccountId = default,
    string? Description = default,
    decimal? Amount = default,
    DateTimeOffset? DueDate = default,
    OperationType? OperationType = default,
    DateTimeOffset? CreatedAt = default,
    DateTimeOffset? UpdatedAt = default) : ISpecification<KidAccountOperation>
{
    public Expression<Func<KidAccountOperation, bool>> ToFilterExpression()
    {
        List<Expression<Func<KidAccountOperation, bool>>> predicates = [];
        
        if (Id is not null)
        {
            predicates.Add(x => x.Id == Id);
        }
        
        if (KidAccountId is not null)
        {
            predicates.Add(x => x.KidAccountId == KidAccountId);
        }
        
        if (Description is not null)
        {
            predicates.Add(x => x.Description == Description);
        }
        
        if (Amount is not null)
        {
            predicates.Add(x => x.Amount == Amount);
        }
        
        if (DueDate is not null)
        {
            predicates.Add(x => x.DueDate == DueDate);
        }
        
        if (OperationType is not null)
        {
            predicates.Add(x => x.OperationType == OperationType);
        }
        
        if (CreatedAt is not null)
        {
            predicates.Add(x => x.CreatedAt == CreatedAt);
        }
        
        if (UpdatedAt is not null)
        {
            predicates.Add(x => x.UpdatedAt == UpdatedAt);
        }
        
        return predicates.Count != 0 ? predicates.Aggregate(AndExpression<KidAccountOperation>.And) : x => true;
    }
    
    public string[] ToIncludeExpression()
    {
        // No includes needed
        return [];
    }
}