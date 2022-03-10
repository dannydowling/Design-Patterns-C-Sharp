using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace Windows_Runtime_Examples.Example_Code
{
    internal class Task_Deferral
    {
        SuspendingDeferral? deferral;
        SuspendingOperation? _operation;

        // The SuspendingOperation is a implementation of SuspendingDeferral.
        // There's a lot of code implemented behind the scenes to make this work.
    
            private async void Defer_Execution(SuspendingOperation operation, Task t)
        {
            // the suspending operation is what will trigger the deferral
            _operation = operation;


            // set the task to do the work
           await t.ContinueWith(t => setTheThreadToWork());

            if (!t.IsCompleted)
            { 
                _operation.GetDeferral();             
            }
             else
            {
                deferral.Complete();                
            }            
        }

        private async void setTheThreadToWork()
        {
            await Task.Delay(2000);
        }
    }
}
