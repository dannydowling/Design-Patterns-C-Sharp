using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public class Interpreter
    {
        public Interpreter()
        {
       
            InterpreterContext context = new InterpreterContext();

            // Usually a tree 

            List<AbstractExpression> list = new List<AbstractExpression>();

            // Populate 'abstract syntax tree' 

            list.Add(new TerminalExpression());
            list.Add(new NonterminalExpression());
            list.Add(new TerminalExpression());
            list.Add(new TerminalExpression());

            // Interpret

            foreach (AbstractExpression exp in list)
            {
                exp.Interpret(context);
            }

            Console.ReadKey();
        }
    }
    public class InterpreterContext
    {
    }

    public abstract class AbstractExpression
    {
        public abstract void Interpret(InterpreterContext context);
    }


    public class TerminalExpression : AbstractExpression
    {
        public override void Interpret(InterpreterContext context)
        {
            Console.WriteLine("Called Terminal.Interpret()");
        }
    }

    public class NonterminalExpression : AbstractExpression
    {
        public override void Interpret(InterpreterContext context)
        {
            Console.WriteLine("Called Nonterminal.Interpret()");
        }
    }
}
