using System;

namespace DesignPatterns
{
    public class Singleton
    {
        public Singleton()
        {
            // Constructor is protected -- cannot use new

            SingletonClass s1 = SingletonClass.Instantiate();
            SingletonClass s2 = SingletonClass.Instantiate();

            // Test for same instance

            if (s1 == s2)
            {
                Console.WriteLine("Objects are the same instance");
            }

            Console.ReadKey();
        }
    }

    public class SingletonClass
    {
        static SingletonClass instance;

        // Constructor is 'protected'

        protected SingletonClass()
        {
        }

        public static SingletonClass Instantiate()
        {
            // Uses lazy initialization.
            // Note: this is not thread safe.
            if (instance == null)
            {
                instance = new SingletonClass();
            }

            return instance;
        }
    }
}
