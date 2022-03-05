using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns.Extensions.Function_Extensions
{
    public struct ShortMonitor
    {
        private int _thId;
        public void Enter()
        {

            var id = Thread.CurrentThread.ManagedThreadId;
            if (_thId != id)
            {
                var spinner = new SpinWait();
                while (Interlocked.CompareExchange(ref _thId, id, 0) != 0)
                    spinner.SpinOnce();
            }
        }

        public bool TryEnter()
        {
            var id = Thread.CurrentThread.ManagedThreadId;
            if (_thId == id)
            {
                return true;
            }
            return Interlocked.CompareExchange(ref _thId, id, 0) == 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Exit() => _thId = 0;

    }
}


     