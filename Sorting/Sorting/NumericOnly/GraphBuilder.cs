using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Sorting
{
    // The graphbuilder will build out a tree like display.
    // Console.WriteLine(string.Format("Tree graph:{0}{1}", Environment.NewLine, outputFromParser(new GraphBuilder())));
  
    public class GraphBuilder : INodeVisitor
    {
        private static string ReplaceLastChar(string str, char rep = ' ')
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Substring(0, str.Length - 1) + rep.ToString();
            }
            else
            {
                return "";
            }
        }

        const Char SPACE = ' ';
        const Char L_TURN = '┌';
        const Char V_PIPE = '│';
        const Char R_TURN = '└';
        const string TAB = "    ";
        const string H_PIPE = "──";

        private StringBuilder sb;
        private Stack<string> indentStack;
        private Stack<BranchOrientation> orientationStack;

        public GraphBuilder()
        {
            sb = new StringBuilder();

            indentStack = new Stack<string>();
            orientationStack = new Stack<BranchOrientation>();

            indentStack.Push(string.Empty);
            orientationStack.Push(BranchOrientation.None);
        }

        public object VisitBinOp(Token op, INode left, INode right)
        {
            BranchOrientation legacyOrientation = orientationStack.Pop();
            string legacyIndent = indentStack.Pop();

            if (legacyOrientation == BranchOrientation.Left)
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, SPACE) + TAB + L_TURN);
                orientationStack.Push(BranchOrientation.Left);
                left.Accept(this);
            }
            else
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, V_PIPE) + TAB + V_PIPE);
                orientationStack.Push(BranchOrientation.Left);
                left.Accept(this);
            }

            if (legacyOrientation == BranchOrientation.Left)
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, L_TURN) + H_PIPE + " (" + op.ToString() + ")");
            }
            else
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, R_TURN) + H_PIPE + " (" + op.ToString() + ")");
            }

            if (legacyOrientation == BranchOrientation.Right)
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, SPACE) + TAB + R_TURN);
                orientationStack.Push(BranchOrientation.Right);
                right.Accept(this);
            }
            else
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, V_PIPE) + TAB + V_PIPE);
                orientationStack.Push(BranchOrientation.Right);
                right.Accept(this);
            }

            return sb.ToString();
        }

        public object VisitNumber(Token num)
        {
            BranchOrientation legacyOrientation = orientationStack.Pop();
            string legacyIndent = indentStack.Pop();

            if (legacyOrientation == BranchOrientation.Left)
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, L_TURN) + H_PIPE + "  " + num.ToString());
            }
            else
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, R_TURN) + H_PIPE + "  " + num.ToString());
            }

            return sb.ToString();
        }

        public object VisitUnaryOp(Token op, INode node)
        {
            BranchOrientation legacyOrientation = orientationStack.Pop();
            string legacyIndent = indentStack.Pop();

            if (legacyOrientation == BranchOrientation.Left)
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, L_TURN) + H_PIPE + " (" + op.ToString() + ")");
            }
            else
            {
                sb.AppendLine(ReplaceLastChar(legacyIndent, R_TURN) + H_PIPE + " (" + op.ToString() + ")");
            }

            if (legacyOrientation == BranchOrientation.Right)
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, SPACE) + TAB + R_TURN);
                orientationStack.Push(BranchOrientation.Right);
                node.Accept(this);
            }
            else
            {
                indentStack.Push(ReplaceLastChar(legacyIndent, V_PIPE) + TAB + R_TURN);
                orientationStack.Push(BranchOrientation.Right);
                node.Accept(this);
            }

            return sb.ToString();
        }

        public object VisitNum(Token num)
        {
            return decimal.Parse(num.Value);
        }

        internal enum BranchOrientation
        {
            None,
            Left,
            Right
        }
    }
}