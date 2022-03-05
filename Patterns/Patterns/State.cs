using System;

namespace DesignPatterns
{
    public class State
    {
        public State()
        {

            var context = new ContextClass(new ConcreteStateA());

            // Issue requests, which toggles state

            context.Request();
            context.Request();
            context.Request();
            context.Request();

            Console.ReadKey();
        }
    }


    public abstract class StateAbstract
    {
        public abstract void Handle(ContextClass context);
    }


    public class ConcreteStateA : StateAbstract
    {
        public override void Handle(ContextClass context)
        {
            context.State = new ConcreteStateB();
        }
    }


    public class ConcreteStateB : StateAbstract
    {
        public override void Handle(ContextClass context)
        {
            context.State = new ConcreteStateA();
        }
    }


    public class ContextClass
    {
        StateAbstract state;

        public ContextClass(StateAbstract state)
        {
            this.State = state;
        }

        // Gets or sets the state

        public StateAbstract State
        {
            get { return state; }
            set
            {
                state = value;
                Console.WriteLine("State: " + state.GetType().Name);
            }
        }

        public void Request()
        {
            state.Handle(this);
        }
    }
}
