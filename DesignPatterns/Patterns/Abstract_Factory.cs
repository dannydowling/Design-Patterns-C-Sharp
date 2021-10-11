using System;

namespace DesignPatterns
{

    class Abstract_Factory
    {
        public Abstract_Factory()
        {
            // Abstract factory #1

            AbstractFactory factory1 = new ConcreteFactory1();
            Client client1 = new Client(factory1);
            client1.Run();

            // Abstract factory #2

            AbstractFactory factory2 = new ConcreteFactory2();
            Client client2 = new Client(factory2);
            client2.Run();

            Console.ReadKey();
        }      
    }

    abstract class AbstractFactory
    {
        public abstract Abstract_Product_A CreateProductA();
        public abstract Abstract_Product_B CreateProductB();
    }

    class ConcreteFactory1 : AbstractFactory
    {
        public override Abstract_Product_A CreateProductA()
        {
            return new ProductA1();
        }
        public override Abstract_Product_B CreateProductB()
        {
            return new ProductB1();
        }
    }

    class ConcreteFactory2 : AbstractFactory
    {
        public override Abstract_Product_A CreateProductA()
        {
            return new ProductA2();
        }
        public override Abstract_Product_B CreateProductB()
        {
            return new ProductB2();
        }
    }

    abstract class Abstract_Product_A
    {
    }

    abstract class Abstract_Product_B
    {
        public abstract void Interact(Abstract_Product_A a);
    }

    //Product A1 is a type of Abstract Product A
    
    class ProductA1 : Abstract_Product_A
    {
    }
    
    class ProductB1 : Abstract_Product_B
    {
        public override void Interact(Abstract_Product_A a)
        {
            Console.WriteLine(GetType().Name +
              " interacts with " + a.GetType().Name);
        }
    }

    //Product A2 is a type of Abstract Product A as well...

    class ProductA2 : Abstract_Product_A
    {
    }


    class ProductB2 : Abstract_Product_B
    {
        public override void Interact(Abstract_Product_A a)
        {
            Console.WriteLine(GetType().Name +
              " interacts with " + a.GetType().Name);
        }
    }

    
    // Interaction for the products.

    class Client
    {
        private Abstract_Product_A _abstractProductA;
        private Abstract_Product_B _abstractProductB;

        // Constructor

        public Client(AbstractFactory factory)
        {
            _abstractProductB = factory.CreateProductB();
            _abstractProductA = factory.CreateProductA();
        }

        public void Run()
        {
            _abstractProductB.Interact(_abstractProductA);
        }
    }
}
