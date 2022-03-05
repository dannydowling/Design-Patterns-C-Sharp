using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public class Builder
    {
        public Builder()
        {
            // Create director and builders

            Director director = new Director();

            BuilderAbstract b1 = new ConcreteBuilder1();
            BuilderAbstract b2 = new ConcreteBuilder2();

            // Construct two products

            director.Construct(b1);
            ProductClass p1 = b1.GetResult();
            p1.Show();

            director.Construct(b2);
            ProductClass p2 = b2.GetResult();
            p2.Show();

            Console.ReadKey();
        }
    }

    class Director
    {
        public void Construct(BuilderAbstract builder)
        {
            builder.BuildPartA();
            builder.BuildPartB();
        }
    }

    abstract class BuilderAbstract
    {
        public abstract void BuildPartA();
        public abstract void BuildPartB();
        public abstract ProductClass GetResult();
    }


    class ConcreteBuilder1 : BuilderAbstract
    {
        private ProductClass _product = new ProductClass();

        public override void BuildPartA()
        {
            _product.Add("PartA");
        }

        public override void BuildPartB()
        {
            _product.Add("PartB");
        }

        public override ProductClass GetResult()
        {
            return _product;
        }
    }


    class ConcreteBuilder2 : BuilderAbstract
    {
        private ProductClass _product = new ProductClass();

        public override void BuildPartA()
        {
            _product.Add("PartX");
        }

        public override void BuildPartB()
        {
            _product.Add("PartY");
        }

        public override ProductClass GetResult()
        {
            return _product;
        }
    }


    class ProductClass
    {
        private List<string> _parts = new List<string>();

        public void Add(string part)
        {
            _parts.Add(part);
        }

        public void Show()
        {
            Console.WriteLine("\nProduct Parts -------");
            foreach (string part in _parts)
                Console.WriteLine(part);
        }
    }
}
