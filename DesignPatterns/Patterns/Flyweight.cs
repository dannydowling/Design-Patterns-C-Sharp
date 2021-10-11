using System;
using System.Collections.Generic;

namespace DesignPatterns
{   
    public class Flyweight
    {
        public Flyweight()
        {

            // Arbitrary extrinsic state

            int extrinsicstate = 34;

            FlyweightFactory factory = new FlyweightFactory();

            // Work with different flyweight instances

            FlyweightAbstract fx = factory.GetFlyweight("X");
            fx.Operation(--extrinsicstate);

            FlyweightAbstract fy = factory.GetFlyweight("Y");
            fy.Operation(--extrinsicstate);

            FlyweightAbstract fz = factory.GetFlyweight("Z");
            fz.Operation(--extrinsicstate);

            UnsharedConcreteFlyweight fu = new
                UnsharedConcreteFlyweight();

            fu.Operation(--extrinsicstate);


            Console.ReadKey();
        }
    }


    public class FlyweightFactory
    {
        private Dictionary<string, FlyweightAbstract> flyweights { get; set; } = new Dictionary<string, FlyweightAbstract>();

        public FlyweightFactory()
        {
            flyweights.Add("X", new ConcreteFlyweight());
            flyweights.Add("Y", new ConcreteFlyweight());
            flyweights.Add("Z", new ConcreteFlyweight());
        }

        public FlyweightAbstract GetFlyweight(string key)
        {
            return ((FlyweightAbstract)flyweights[key]);
        }
    }

    /// <summary>
    /// The 'Flyweight' abstract class
    /// </summary>

    public abstract class FlyweightAbstract
    {
        public abstract void Operation(int extrinsicstate);
    }

    /// <summary>
    /// The 'ConcreteFlyweight' class
    /// </summary>

    public class ConcreteFlyweight : FlyweightAbstract
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("ConcreteFlyweight: " + extrinsicstate);
        }
    }

    /// <summary>
    /// The 'UnsharedConcreteFlyweight' class
    /// </summary>

    public class UnsharedConcreteFlyweight : FlyweightAbstract
    {
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("UnsharedConcreteFlyweight: " +
                extrinsicstate);
        }
    }
}
