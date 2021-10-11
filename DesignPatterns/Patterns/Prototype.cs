using System;

namespace DesignPatterns
{ 

    public class Prototype
    {
        public Prototype()
        {
            // Create two instances and clone each

            ConcretePrototype1 p1 = new ConcretePrototype1("I");
            ConcretePrototype1 c1 = (ConcretePrototype1)p1.Clone();
            Console.WriteLine("Cloned: {0}", c1.Id);

            ConcretePrototype2 p2 = new ConcretePrototype2("II");
            ConcretePrototype2 c2 = (ConcretePrototype2)p2.Clone();
            Console.WriteLine("Cloned: {0}", c2.Id);

            Console.ReadKey();
        }
    }


    public abstract class PrototypeAbstract
    {
        string id;

        public PrototypeAbstract(string id)
        {
            this.id = id;
        }

        // Gets id

        public string Id
        {
            get { return id; }
        }

        public abstract PrototypeAbstract Clone();
    }

    public class ConcretePrototype1 : PrototypeAbstract
    {
        public ConcretePrototype1(string id)
            : base(id)
        {
        }

        // Returns a shallow copy

        public override PrototypeAbstract Clone()
        {
            return (PrototypeAbstract)this.MemberwiseClone();
        }
    }
  
    public class ConcretePrototype2 : PrototypeAbstract
    {
        // Constructor

        public ConcretePrototype2(string id)
            : base(id)
        {
        }

        // Returns a shallow copy

        public override PrototypeAbstract Clone()
        {
            return (PrototypeAbstract)this.MemberwiseClone();
        }
    }
}
