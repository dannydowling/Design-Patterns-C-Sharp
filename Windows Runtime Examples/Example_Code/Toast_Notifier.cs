using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Windows_Runtime_Examples.Example_Code
{
    internal static  class Toast_Notifier
    {
        public static void NotifyOnCompletion(this Task task, DateTime offset)
        {
            // TODO:
            //get the class name of the parent using reflection and compose that into the toast message

            string taskName = task.Id.ToString();

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
