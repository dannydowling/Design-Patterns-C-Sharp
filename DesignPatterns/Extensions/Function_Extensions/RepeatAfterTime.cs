using System;
using System.Timers;

namespace DesignPatterns.Extensions
{
    public static class TimedReplay
    {
        public static dynamic RepeatAfterTime (RuntimeMethodHandle runtimeMethod, double t)
        {
            
            Timer timer = new Timer (t);
           timer.Elapsed += Timer_Elapsed;
            return runtimeMethod;
            
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            AsyncCallback asyncCallback = sender as AsyncCallback;
            asyncCallback(sender as IAsyncResult);
        }
    }
}
