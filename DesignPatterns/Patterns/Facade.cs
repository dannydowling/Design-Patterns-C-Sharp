using System;

namespace DesignPatterns
{

    public class FacadePattern
    {
        public FacadePattern()
        {
            Facade facade = new Facade();

            facade.MethodA();
            facade.MethodB();

            Console.ReadKey();
        }      
    }   
    public class SubSystemOne
    {
        public void MethodOne()
        {
            Console.WriteLine("This is method one");
        }
    }    
    public class SubSystemTwo
    {
        public void MethodTwo()
        {
            Console.WriteLine("this is method two");
        }
    }   
    public class SubSystemThree
    {
        public void MethodThree()
        {
            Console.WriteLine("this is method three");
        }
    }       
    public class SubSystemFour
    {
        public void MethodFour()
        {
            Console.WriteLine("this is method four");
        }
    }   

    //The facade is a face for another method...
    public class Facade
    {
        SubSystemOne one;
        SubSystemTwo two;
        SubSystemThree three;
        SubSystemFour four;

        public Facade()
        {
            one = new SubSystemOne();
            two = new SubSystemTwo();
            three = new SubSystemThree();
            four = new SubSystemFour();
        }

        public void MethodA()
        {
            Console.WriteLine("\nMethodA() ---- ");
            one.MethodOne();
            two.MethodTwo();
            four.MethodFour();
        }

        public void MethodB()
        {
            Console.WriteLine("\nMethodB() ---- ");
            two.MethodTwo();
            three.MethodThree();
        }
    }
}
