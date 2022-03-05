using System;

namespace DesignPatterns
{
    public class Decorator_Pattern
    {
        public Decorator_Pattern()
        {

            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();

            // decorators

            d1.SetComponent(c);
            d2.SetComponent(d1);

            d2.Operation();

            
            Console.ReadKey();
        }
    }
    public abstract class DecoratorComponent
    {
        public abstract void Operation();
    }


    public class ConcreteComponent : DecoratorComponent
    {
        public override void Operation()
        {
            Console.WriteLine("ConcreteComponent.Operation()");
        }
    }

    public abstract class Decorator : DecoratorComponent
    {
        protected DecoratorComponent component;

        public void SetComponent(DecoratorComponent component)
        {
            this.component = component;
        }

        public override void Operation()
        {
            if (component != null)
            {
                component.Operation();
            }
        }
    }

   
    public class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("ConcreteDecoratorA.Operation()");
        }
    }


    public class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("ConcreteDecoratorB.Operation()");
        }

        void AddedBehavior()
        {
        }
    }
}
