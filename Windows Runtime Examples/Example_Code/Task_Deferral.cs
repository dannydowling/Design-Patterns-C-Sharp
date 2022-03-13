using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Windows_Runtime_Examples.Example_Code
{
    internal class Defer_A_Task
    {
        SuspendingDeferral? deferral;
        SuspendingOperation? _operation;
            
            private async void Defer_Execution_Return(Task t)
        {
            if (!t.IsCompleted)
            { _operation?.GetDeferral();
                // set the task to do the work
                await t.ContinueWith(t => setTheThreadToWork());
            }             
         deferral?.Complete(); 
        }

        private async void setTheThreadToWork()
        {
           await Task.Delay(2000);
        }
    }

    class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }
        public EventArgs(T val)
        {
            Value = val;
        }
    }

    internal class Save_State_With_Deferral
    {
        public event EventHandler<SuspendingEventArgs> methodPassed;


        public Save_State_With_Deferral(RuntimeMethodHandle s, RuntimeArgumentHandle e)
        {
            methodPassed += (o, e) => OnSuspendingEvent(s, e);
        
        }

        private Dictionary<string, object> _suspended_State = new Dictionary<string, object>();
        
        private readonly string _saveFileName = "store.xml";

        private async void OnSuspendingEvent(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();         
            await SaveStateAsync();
            deferral.Complete();
        }
        private async Task SaveStateAsync()
        {
            var memoryStream = new MemoryStream();
            var serializer = new DataContractSerializer(typeof(Dictionary<string, object>));
            serializer.WriteObject(memoryStream, _suspended_State);

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(_saveFileName, CreationCollisionOption.ReplaceExisting);

            using (var fileStream = await file.OpenStreamForWriteAsync())
            {
                //because we have written to the stream, set the position back to start
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
        }
    }
}
