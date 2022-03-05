using System;

namespace DesignPatterns
{
    public class Proxy
    {
        public Proxy()
        {

            // Create proxy and request a service

            ProxyClass proxy = new ProxyClass();
            proxy.Request();

            // Wait for user

            Console.ReadKey();
        }
    }


    public abstract class Subject
    {
        public abstract void Request();
    }

    public class RealSubject : Subject
    {
        public override void Request()
        {
            Console.WriteLine("Called RealSubject.Request()");
        }
    }

    public class ProxyClass : Subject
    {
        private RealSubject realSubject;

        public override void Request()
        {
            // Use 'lazy initialization'

            if (realSubject == null)
            {
                realSubject = new RealSubject();
            }

            realSubject.Request();
        }
    }
}

