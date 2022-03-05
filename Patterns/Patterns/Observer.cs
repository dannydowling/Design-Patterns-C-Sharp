using System;
using System.Collections.Generic;

namespace DesignPatterns
{
    public class Observer
    {
        public Observer()
        {

            ConcreteSubject s = new ConcreteSubject();

            s.Attach(new ConcreteObserver(s, "X"));
            s.Attach(new ConcreteObserver(s, "Y"));
            s.Attach(new ConcreteObserver(s, "Z"));

            // Change subject and notify observers

            s.SubjectState = "ABC";
            s.Notify();


            Console.ReadKey();
        }
    }


    public abstract class SubjectAbstract
    {
        private List<ObserverAbstract> observers = new List<ObserverAbstract>();

        public void Attach(ObserverAbstract observer)
        {
            observers.Add(observer);
        }

        public void Detach(ObserverAbstract observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (ObserverAbstract o in observers)
            {
                o.Update();
            }
        }
    }


    public class ConcreteSubject : SubjectAbstract
    {
        private string subjectState;

        // Gets or sets subject state

        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }
    }


    public abstract class ObserverAbstract
    {
        public abstract void Update();
    }


    public class ConcreteObserver : ObserverAbstract
    {
        private string name;
        private string observerState;
        private ConcreteSubject subject;
      
        public ConcreteObserver(
            ConcreteSubject subject, string name)
        {
            this.subject = subject;
            this.name = name;
        }

        public override void Update()
        {
            observerState = subject.SubjectState;
            Console.WriteLine("Observer {0}'s new state is {1}",
                name, observerState);
        }

        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }
}
