using System;

namespace DesignPatterns
{
    internal class Program
    {
        static void Main()
        {     
            Console.WriteLine("Test the design pattern in the code with one of the following options...");
            Console.WriteLine("0: Abstract Factory");
            Console.WriteLine("1: Adapter");
            Console.WriteLine("2: Bridge");
            Console.WriteLine("3: Builder");
            Console.WriteLine("4: Chain Of Responsibility");
            Console.WriteLine("5: Command");
            Console.WriteLine("6: Composite");
            Console.WriteLine("7: Decorator");
            Console.WriteLine("8: Facade");
            Console.WriteLine("9: Factory");
            Console.WriteLine("10: Flyweight");
            Console.WriteLine("11: Interpreter");
            Console.WriteLine("12: Iterator");
            Console.WriteLine("13: Mediator");
            Console.WriteLine("14: Memento");
            Console.WriteLine("15: Observer");
            Console.WriteLine("16: Prototype");
            Console.WriteLine("17: Proxy");
            Console.WriteLine("18: Singleton");
            Console.WriteLine("19: State");
            Console.WriteLine("20: Strategy");
            Console.WriteLine("21: Template");
            Console.WriteLine("22: Visitor");
            Console.WriteLine("");
            Console.WriteLine("Please enter your selection:");

           
            var arg = int.Parse(Console.ReadLine());

            switch (arg)
            {
                case 0:
                    var Thing0 = new Abstract_Factory();
                    return;
                case 1:
                    var Thing1 = new Adapter();
                    return;
                case 2:
                    var Thing2 = new Bridge();
                    return;
                case 3:
                    var Thing3 = new Builder();
                    return;
                case 4:
                    var Thing4 = new ChainResponsibility();
                    return;
                case 5:
                    var Thing5 = new Command_Pattern();
                    return;
                case 6:
                    var Thing6 = new Composite_Pattern();
                    return;
                case 7:
                    var Thing7 = new Decorator_Pattern();
                    return;
                case 8:
                    var Thing8 = new Facade();
                    return;
                case 9:
                    var Thing9 = new Factory();
                    return;
                case 10:
                    var Thing10 = new Flyweight();
                    return;
                case 11:
                    var Thing11 = new Interpreter();
                    return;
                case 12:
                    var Thing12 = new Iterator();
                    return;
                case 13:
                    var Thing13 = new Mediator();
                    return;
                case 14:
                    var Thing14 = new Memento();
                    return;
                case 15:
                    var Thing15 = new Observer();
                    return;
                case 16:
                    var Thing16 = new Prototype();
                    return;
                case 17:
                    var Thing17 = new Proxy();
                    return;
                case 18:
                    var Thing18 = new Singleton();
                    return;
                case 19:
                    var Thing19 = new State();
                    return;
                case 20:
                    var Thing20 = new Strategy();
                    return;
                case 21:
                    var Thing21 = new Tenplate();
                    return;
                case 22:
                    var Thing22 = new Visitor();
                    return;


                default:
                    break;
            }


        }
    }
}
