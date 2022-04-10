using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Windows_Runtime_Examples.Example_Code
{
    internal static class Toast_Notifier
    {
        public static string NameOfCallingClass(this Task task)
        {
            string fullName;
            Type? declaringType;
            int skipFrames = 2;
            try
            {
                // going to offset the stackframe to capture the method that called this execution
                MethodBase? method = new StackFrame(skipFrames, false).GetMethod();

                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                // the declaring type is the class that called the method.
                fullName = declaringType.FullName;

                // we're looking for the first class that isn't in mscorlib, meaning a custom class.
                while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase)) ;

                return fullName;
            }
            catch (AccessViolationException)
            {
                Console.WriteLine("The thread context has changed");
                return task.NameOfCallingClass();
            }
        }
        public static void NotifyOnCompletion(this Task task, DateTime offset)
        {
            string taskName = task.NameOfCallingClass();

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(taskName)))
            {
                //wait for the task to complete
                task.Wait();
            }
            else
            {
                BackgroundTaskCompletedEventArgs args = new BackgroundTaskCompletedEventArgs();

                //we'll build up the toast notification that the task has completed.

                XmlDocument dom = new XmlDocument();

                string xml = String.Format("<head><body><Inner> The Following Task Has Completed: </head> </Inner> <Outer> {0}</Outer></body></head>", taskName + args);
                dom.LoadXml(xml);

                ScheduledToastNotification scheduledToastNotification = new ScheduledToastNotification(
                    dom, offset, TimeSpan.FromMinutes(1), 2);
            }
        }
    }
}
