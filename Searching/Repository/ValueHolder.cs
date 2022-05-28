using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_Snippets.Code_Snippets
{
    public class concreteClass
    {
        public Guid CustomerId { get; set; }

        // Virtual because a proxy can now override its behaviour
        public virtual string Name { get; set; }
        public object DataId { get; internal set; }


        public class Example : concreteClass
        {
            public IValueHolder<byte[]> exampleInfoValueHolder { get; set; }

            public byte[] exampleData
            {
                get
                {
                    return exampleInfoValueHolder.GetValue(Name);
                }
            }
        }

        //Below is doing the heavy lifting. It'll take in a parameter and return the specific item that matches the parameter to the consumer.

        public interface IValueHolder<T>
        {
            T GetValue(object parameter);
        }
        public class ValueHolder<T> : IValueHolder<T>
        {
            private readonly Func<object, T> getValue;
            private T _value;

            public ValueHolder(Func<object, T> getValue)
            {
                this.getValue = getValue;
            }
            public T GetValue(object parameter)
            {
                if (_value == null)
                {
                    _value = getValue(parameter);
                }

                return _value;
            }
        }
    }
}

