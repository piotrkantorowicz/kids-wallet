using System.Linq.Expressions;

namespace KidsWallet.Shared.Expressions;

public sealed class ReplaceExpressionVisitor : ExpressionVisitor
{
    private readonly Expression? _newValue;
    private readonly Expression _oldValue;

    public ReplaceExpressionVisitor(Expression oldValue, Expression? newValue)
    {
        _oldValue = oldValue;
        _newValue = newValue;
    }

    public override Expression? Visit(Expression? node)
    {
        return node == _oldValue ? _newValue : base.Visit(node: node);
    }
}