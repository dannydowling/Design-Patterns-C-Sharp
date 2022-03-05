using System;

namespace DesignPatterns
{ 
   
    public class Adapter_Pattern
    {
        public Adapter_Pattern()
        {
            // Create adapter and place a request

            Target target = new Adapter();
            target.Request();

            Console.ReadKey();
        }       

    }
    
    public class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }

    
    public class Adapter : Target
    {
        private Adaptee adaptee = new Adaptee();

        public override void Request()
        {
            adaptee.SpecificRequest();
        }
    }
    
    public class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}
