using System;

namespace DesignPatterns
{    
    public class Mediator
    {
        public Mediator()
        {        
            ConcreteMediator m = new ConcreteMediator();

            ConcreteColleague1 c1 = new ConcreteColleague1(m);
            ConcreteColleague2 c2 = new ConcreteColleague2(m);

            m.Colleague1 = c1;
            m.Colleague2 = c2;

            c1.Send("How Long Will This Take?");
            c2.Send("A long time indeed");


            Console.ReadKey();
        }
    }


    public abstract class MediatorAbstract
    {
        public abstract void Send(string message,
            Colleague colleague);
    }


    public class ConcreteMediator : MediatorAbstract
    {
        ConcreteColleague1 taylor;
        ConcreteColleague2 danny;

        public ConcreteColleague1 Colleague1
        {
            set { taylor = value; }
        }

        public ConcreteColleague2 Colleague2
        {
            set { danny = value; }
        }

        public override void Send(string message, Colleague colleague)
        {
            if (colleague == taylor)
            {
                danny.Notify(message);
            }
            else
            {
                taylor.Notify(message);
            }
        }
    }


    public abstract class Colleague
    {
        protected MediatorAbstract mediator;

        public Colleague(MediatorAbstract mediator)
        {
            this.mediator = mediator;
        }
    }

    public class ConcreteColleague1 : Colleague
    {
        public ConcreteColleague1(MediatorAbstract mediator)
            : base(mediator)
        {
        }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine("Taylor gets message: "
                + message);
        }
    }


    public class ConcreteColleague2 : Colleague
    {
        public ConcreteColleague2(MediatorAbstract mediator)
            : base(mediator)
        {
        }

        public void Send(string message)
        {
            mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine("Danny gets message: "
                + message);
        }
    }
}
