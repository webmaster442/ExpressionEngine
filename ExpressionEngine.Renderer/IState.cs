//-----------------------------------------------------------------------------
// (c) 2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

namespace ExpressionEngine.Renderer
{
    public interface IState : IVariables
    {
        void SetExpression(string variableName, IExpression? expression);
        bool IsExpression(string variableName);
        IExpression? GetExpression(string variableName);
        INumber Ans { get; set; }
    }
}
