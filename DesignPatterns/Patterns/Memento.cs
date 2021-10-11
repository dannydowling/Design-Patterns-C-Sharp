using System;

namespace DesignPatterns
{
    public class Memento
    {
        public Memento()
        {

            Originator o = new Originator();
            o.State = "On";

            // Store internal state

            Caretaker c = new Caretaker();
            c.Memento = o.CreateMemento();

            // Continue changing originator

            o.State = "Off";

            // Restore saved state

            o.SetMemento(c.Memento);

            Console.ReadKey();
        }
    }

    public class Originator
    {
        string state;

        public string State
        {
            get { return state; }
            set
            {
                state = value;
                Console.WriteLine("State = " + state);
            }
        }

        // Creates memento 

        public MementoClass CreateMemento()
        {
            return (new MementoClass(state));
        }

        // Restores original state

        public void SetMemento(MementoClass memento)
        {
            Console.WriteLine("Restoring state...");
            State = memento.State;
        }
    }


    public class MementoClass
    {
        string state;

        public MementoClass(string state)
        {
            this.state = state;
        }

        public string State
        {
            get { return state; }
        }
    }

    public class Caretaker
    {
        MementoClass memento;

        public MementoClass Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
