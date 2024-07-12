using System.Linq.Expressions;

using KidsWallet.Extensions;
using KidsWallet.Persistence.Model.Abstraction;
using KidsWallet.Shared.Expressions.And;

namespace KidsWallet.Persistence.Model.Operations.Specifications;

public sealed record KidAccountOperationSpecification(
    Guid? Id = default,
    Guid? KidAccountId = default,
    string? Description = default,
    decimal? Amount = default,
    DateTime? DueDate = default,
    OperationType? OperationType = default,
    DateTime? CreatedAt = default,
    DateTime? UpdatedAt = default) : ISpecification<KidAccountOperation>
{
    public Expression<Func<KidAccountOperation, bool>> ToFilterExpression()
    {
        List<Expression<Func<KidAccountOperation, bool>>> predicates = [];

        if (Id is not null)
        {
            predicates.Add(item: x => x.Id == Id);
        }

        if (KidAccountId is not null)
        {
            predicates.Add(item: x => x.KidAccountId == KidAccountId);
        }

        if (Description is not null)
        {
            predicates.Add(item: x => x.Description == Description);
        }

        if (Amount is not null)
        {
            predicates.Add(item: x => x.Amount == Amount);
        }

        if (DueDate is not null)
        {
            predicates.Add(item: x => x.DueDate == DueDate);
        }

        if (OperationType is not null)
        {
            predicates.Add(item: x => x.OperationType == OperationType);
        }

        if (CreatedAt is not null)
        {
            predicates.Add(item: x => x.CreatedAt == CreatedAt);
        }

        if (UpdatedAt is not null)
        {
            predicates.Add(item: x => x.UpdatedAt == UpdatedAt);
        }

        return predicates.Count != 0 ? predicates.Aggregate(func: AndExpression<KidAccountOperation>.And) : x => true;
    }

    public string[] ToIncludeExpression()
    {
        // No includes needed
        return [];
    }

    public override string ToString()
    {
        return this.Print();
    }
}