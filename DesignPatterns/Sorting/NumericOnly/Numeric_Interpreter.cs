using System;

namespace DesignPatterns.Sorting
{
    public class NumericInterpreter
    {
        public NumericInterpreter()
        {
            string input = string.Empty;
            Console.WriteLine("Input an expression, for example: (2.11*-3*(5+7.18+9.23))+---1+++(-+-+--(1+2))*(-+-3+4)*(5+6)+3*(2+1)");
            input = Console.ReadLine() as string;
            {
                try
                {
                    Interpreter interpreter = new Interpreter(input);
                    Expression node = interpreter.Parse();

                    Console.WriteLine(string.Format("Tree graph:{0}{1}", Environment.NewLine, node.Accept(new GraphBuilder())));
                    Console.WriteLine(string.Format("Value: {0}", node.Accept(new ValueBuilder())));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    internal class Interpreter
    {
        private Token currentToken;
        private int currentPosition;
        private int charCount;
        private char currentChar;
        public string Text { get; private set; }

        public Interpreter(string text)
        {
            Text = string.IsNullOrEmpty(text) ? string.Empty : text;
            charCount = Text.Length;
            currentToken = Token.None();

            currentPosition = -1;
            Advance();
        }

        internal Expression Parse()
        {
            NextToken();
            Expression node = expression();
            ExpectToken(TokenType.None);
            return node;
        }

        private Token ExpectToken(TokenType tokenType)
        {
            if (currentToken.Type == tokenType)
            {
                return currentToken;
            }
            else
            {
                throw new Exception(string.Format("Invalid syntax at position {0}. Expected {1} but {2} is given.", currentPosition, tokenType, currentToken.Type.ToString()));
            }
        }

        private Expression expression()
        {
            Expression left = GrabTerm();

            while (currentToken.Type == TokenType.Plus || currentToken.Type == TokenType.Minus)
            {
                Token operation = currentToken;
                NextToken();
                Expression right = GrabTerm();
                left = new BinaryOperation(operation, left, right);
            }

            return left;
        }

        private Expression GrabTerm()
        {
            Expression left = GrabFactor();

            while (currentToken.Type == TokenType.Multiply || currentToken.Type == TokenType.Divide)
            {
                Token operation = currentToken;
                NextToken();
                Expression right = GrabFactor();
                left = new BinaryOperation(operation, left, right);
            }

            return left;
        }

        private Expression GrabFactor()
        {
            if (currentToken.Type == TokenType.Plus || currentToken.Type == TokenType.Minus)
            {
                Expression node = GrabUnaryExpr();
                return node;
            }
            else if (currentToken.Type == TokenType.LeftParenthesis)
            {
                Expression node = GrabBracketExpr();
                return node;
            }
            else
            {
                Token token = ExpectToken(TokenType.Number);
                NextToken();
                return new Num(token);
            }
        }

        private Expression GrabUnaryExpr()
        {
            Token op;

            if (currentToken.Type == TokenType.Plus)
            {
                op = ExpectToken(TokenType.Plus);
            }
            else
            {
                op = ExpectToken(TokenType.Minus);
            }

            NextToken();

            if (currentToken.Type == TokenType.Plus || currentToken.Type == TokenType.Minus)
            {
                Expression expr = GrabUnaryExpr();
                return new UnaryOp(op, expr);
            }
            else
            {
                Expression expr = GrabFactor();
                return new UnaryOp(op, expr);
            }
        }

        private Expression GrabBracketExpr()
        {
            ExpectToken(TokenType.LeftParenthesis);
            NextToken();
            Expression node = this.expression();
            ExpectToken(TokenType.RightParenthesis);
            NextToken();
            return node;
        }

        private void NextToken()
        {
            switch (currentChar)
            {
                case char.MinValue:
                    currentToken = Token.None();
                    break;

                case ' ':
                    while (currentChar != char.MinValue && currentChar == ' ')
                    {
                        Advance();
                    }
                    break;
                case '+':
                    currentToken = new Token(TokenType.Plus, currentChar.ToString());
                    Advance();
                    break;
                case '-':
                    currentToken = new Token(TokenType.Minus, currentChar.ToString());
                    Advance();
                    break;
                case '*':
                    currentToken = new Token(TokenType.Multiply, currentChar.ToString());
                    Advance();
                    break;
                case '/':
                    currentToken = new Token(TokenType.Divide, currentChar.ToString());
                    Advance();
                    break;
                case '(':
                    currentToken = new Token(TokenType.LeftParenthesis, currentChar.ToString());
                    Advance();
                    break;
                case ')':
                    currentToken = new Token(TokenType.RightParenthesis, currentChar.ToString());
                    Advance();
                    break;

                default:
                    break;
            }
            string number = string.Empty;
            try
            {

                if (currentChar >= '0' && currentChar <= '9')
                {
                    while (currentChar >= '0' && currentChar <= '9')
                    {
                        number += currentChar.ToString();
                        Advance();
                    }

                    if (currentChar == '.')
                    {
                        number += currentChar.ToString();
                        Advance();

                        if (currentChar >= '0' && currentChar <= '9')
                        {

                            number += currentChar.ToString();
                            Advance();
                        }
                    }
                }
            }

            catch (Exception)
            {

                string.Format("Invalid syntax at position {0}. Unexpected symbol {1}.", currentPosition, currentChar);

            }
            currentToken = new Token(TokenType.Number, number);
            return;
        }
        private void Advance()
        {
            currentPosition += 1;

            if (currentPosition < charCount)
            {
                currentChar = Text[currentPosition];
            }
            else
            {
                currentChar = char.MinValue;
            }
        }
    }


    internal class ValueBuilder : INodeVisitor
    {
        public object VisitBinOp(Token op, INode left, INode right)
        {
            switch (op.Type)
            {
                case TokenType.Plus:
                    return (decimal)left.Accept(this) + (decimal)right.Accept(this);
                case TokenType.Minus:
                    return (decimal)left.Accept(this) - (decimal)right.Accept(this);
                case TokenType.Multiply:
                    return (decimal)left.Accept(this) * (decimal)right.Accept(this);
                case TokenType.Divide:
                    return (decimal)left.Accept(this) / (decimal)right.Accept(this);
                default:
                    throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
            }
        }

        public object VisitNum(Token num)
        {
            return decimal.Parse(num.Value);
        }

        public object VisitUnaryOp(Token op, INode node)
        {
            switch (op.Type)
            {
                case TokenType.Plus:
                    return (decimal)node.Accept(this);
                case TokenType.Minus:
                    return -(decimal)node.Accept(this);
                default:
                    throw new Exception(string.Format("Token of type {0} cannot be evaluated.", op.Type.ToString()));
            }
        }
    }
    internal class Num : Expression
    {
        internal Token Token { get; private set; }

        public Num(Token token)
        {
            Token = token;
        }

        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitNum(this.Token);
        }
    }

    internal class UnaryOp : Expression
    {
        internal Token Op { get; private set; }
        internal Expression Node { get; private set; }

        public UnaryOp(Token op, Expression node)
        {
            Op = op;
            Node = node;
        }

        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitUnaryOp(Op, Node);
        }
    }

    internal class BinaryOperation : Expression
    {
        internal Token Op { get; private set; }
        internal Expression Left { get; private set; }
        internal Expression Right { get; private set; }

        public BinaryOperation(Token op, Expression left, Expression right)
        {
            Op = op;
            Left = left;
            Right = right;
        }

        override public object Accept(INodeVisitor visitor)
        {
            return visitor.VisitBinOp(Op, Left, Right);
        }
    }
}