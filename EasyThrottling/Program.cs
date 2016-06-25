using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyThrottling
{
    public class Program
    {
        public const int MaxTasks = 3;
        public const int MaxIterations = 5;

        static void Main(string[] args)
        {
            TaskData data;
            Throttle throttle = new Throttle(new TimeSpan(0, 0, 2), MaxTasks);
            throttle.OnNotify += Throttle_OnNotify;

            for (int i = 1; i <= 5; i++) {
                data = new TaskData { TaskId = i, Throttle = throttle };
                Task task = new Task(t => DoSomething((TaskData)t), data);
                task.Start();
            }

            Console.ReadKey();
        }

        private static void Throttle_OnNotify(ThrottleState state, int numberOfItemsInProgress, int limit)
        {
            if (state == ThrottleState.Allowed || state == ThrottleState.Continue)
            {
                Console.WriteLine(string.Format("{0} - {1} of {2} tasks in progress", state.ToString(), numberOfItemsInProgress, limit));
            } else
            {
                Console.WriteLine(string.Format("{0} - Waiting before executing the next task... Number of active tasks {1}", state.ToString(), numberOfItemsInProgress- 1));
            }
        }

        private static void DoSomething(TaskData data)
        {
            for (int i=1; i <= MaxIterations; i++)
            {
                data.Throttle.Wait();

                Console.WriteLine(string.Format("Task #{0} iteration {1}", data.TaskId, i));
                Thread.Sleep(3000);
            }

            Console.WriteLine(string.Format("Finish task #{0}", data.TaskId));
        }
    }

    public class TaskData
    {
        public int TaskId { get; set; }
        public Throttle Throttle;
    }
}
