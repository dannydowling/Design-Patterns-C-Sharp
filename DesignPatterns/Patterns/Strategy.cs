using System;

namespace DesignPatterns
{

    public class Strategy
    {
        public Strategy()
        {
            Context_Class context;

            // Three contexts following different strategies

            context = new Context_Class(new ConcreteStrategyA());
            context.ContextInterface();

            context = new Context_Class(new ConcreteStrategyB());
            context.ContextInterface();

            context = new Context_Class(new ConcreteStrategyC());
            context.ContextInterface();


            Console.ReadKey();
        }
    }


    public abstract class StrategyAbstract
    {
        public abstract void AlgorithmInterface();
    }


    public class ConcreteStrategyA : StrategyAbstract
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine(
                "Called Concrete Strategy A");
        }
    }

  
    public class ConcreteStrategyB : StrategyAbstract
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine(
                "Called Concrete Strategy B");
        }
    }


    public class ConcreteStrategyC : StrategyAbstract
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine(
                "Called Concrete Strategy C");
        }
    }
    

    public class Context_Class
    {
        StrategyAbstract strategy;

        // Constructor

        public Context_Class(StrategyAbstract strategy)
        {
            this.strategy = strategy;
        }

        public void ContextInterface()
        {
            strategy.AlgorithmInterface();
        }
    }
}
