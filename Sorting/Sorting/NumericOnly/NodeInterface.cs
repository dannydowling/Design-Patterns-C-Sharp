using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Sorting
{
    //a node is a position in a formula. A token is a representation of that position as an operator.
        public interface INode
        {
            object Accept(INodeVisitor visitor);
        }

        public interface INodeVisitor
        {
           object VisitNum(Token num);
           object VisitUnaryOp(Token op, INode node);
           object VisitBinOp(Token op, INode left, INode right);
        }

        public abstract class Expression : INode
        {
            abstract public object Accept(INodeVisitor visitor);
        }

    public class Token
    {
        public TokenType Type { get; internal set; }
        public string Value { get; internal set; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public static Token None()
        {
            return new Token(TokenType.None, "");
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public enum TokenType
    {
        None,
        Plus,
        Minus,
        Multiply,
        Divide,
        Number,
        LeftParenthesis,
        RightParenthesis
    }

}
