using System;

namespace DesignPatterns
{

    public class Bridge
    {

        public Bridge()
        {
            Abstraction ab = new RefinedAbstraction();

            // Set implementation and call

            ab.Implementor = new ConcreteImplementorA();
            ab.Operation();

            // Change implemention and call

            ab.Implementor = new ConcreteImplementorB();
            ab.Operation();

            Console.ReadKey();
        }      
    }

    public class Abstraction
    {
        protected ImplementorAbstract implementor;

        public ImplementorAbstract Implementor
        {
            set { implementor = value; }
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    public abstract class ImplementorAbstract
    {
        public abstract void Operation();
    }

    public class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    public class ConcreteImplementorA : ImplementorAbstract
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorA Operation");
        }
    }
    public class ConcreteImplementorB : ImplementorAbstract
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteImplementorB Operation");
        }
    }
}
