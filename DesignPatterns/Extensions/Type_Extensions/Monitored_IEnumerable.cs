using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DesignPatterns.Extensions
{
    internal class Thread_Monitored_Collection<T> where T : class
    {
        private int _capacity;
        private int _expansionLimit;
        private int unitPos;
        private int waitUnitPos;
        private int waitCount;
        private int lockState;
        private object lockObj;
        private object expansionLockObj;
        private T[] units;
        Func<T> _unitFactory;
        

        public Thread_Monitored_Collection(IEnumerable<T> collection)
        {
            _capacity = collection.Count();
            _expansionLimit = collection.IndexOf(collection.Max());
            
            lockObj = new object();
            expansionLockObj = new object();
        }

        public T Fetch()
        {
            T unit;
            Lock();
            unit = (unitPos != _capacity) ? unit = units[unitPos++] : (_capacity < _expansionLimit ? Expand() : Wait());
            Unlock();

            return unit;
        }

        public void Store(T unit)
        {
            Lock();

            if (waitCount == 0)
            {
                units[--unitPos] = unit;
            }
            else
            {
                Pulse(unit);
            }

            Unlock();
        }

        private T Expand()
        {
            T unit = null;

            bool lockTaken = false;

            try
            {
                Monitor.TryEnter(expansionLockObj, ref lockTaken);

                if (!lockTaken)
                {
                    Unlock();
                    Monitor.Enter(expansionLockObj, ref lockTaken);
                    Lock();
                }

                if (unitPos != this._capacity)
                {
                    unit = units[unitPos++];
                }
                else
                {
                    if (this._capacity == _expansionLimit)
                    {
                        unit = Wait();
                    }
                    else
                    {
                        Unlock();

                        int capacity = this._capacity;
                        int newCapacity = capacity * 2;

                        if (newCapacity > _expansionLimit)
                        {
                            newCapacity = _expansionLimit;
                        }

                        T[] newUnits = new T[newCapacity];

                        for (int i = capacity; i < newCapacity; i++)
                        {
                            newUnits[i] = _unitFactory.Invoke();
                        }

                        Lock();

                        Array.Copy(units, 0, newUnits, 0, capacity);
                        units = newUnits;
                        this._capacity = newCapacity;
                        unit = units[unitPos++];
                    }
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(expansionLockObj);
                }
            }

            return unit;
        }

        private T Wait()
        {
            waitCount++;

            lock (lockObj)
            {
                Unlock();
                Monitor.Wait(lockObj);
            }

            Lock();

            return units[--waitUnitPos];
        }

        private void Pulse(T unit)
        {
            waitCount--;
            units[waitUnitPos++] = unit;

            lock (lockObj)
            {
                Monitor.Pulse(lockObj);
            }
        }

        private void Lock()
        {
            if (Interlocked.CompareExchange(ref lockState, 1, 0) != 0)
            {
                SpinLock();
            }
        }

        private void SpinLock()
        {
            SpinWait spinWait = new SpinWait();

            do
            {
                spinWait.SpinOnce();
            }
            while (Interlocked.CompareExchange(ref lockState, 1, 0) != 0);
        }

        private void Unlock()
        {
            Interlocked.Exchange(ref lockState, 0);
        }

      
    }
}
