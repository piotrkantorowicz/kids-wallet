using System.Linq.Expressions;

namespace KidsWallet.Shared.Expressions;

public sealed class ReplaceExpressionVisitor(Expression oldValue, Expression? newValue) : ExpressionVisitor
{
    public override Expression? Visit(Expression? node)
    {
        return node == oldValue ? newValue : base.Visit(node);
    }
}