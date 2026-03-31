using System.Threading;

namespace Threads
{
    internal class Program
    {
        static void SampleTask(object state)
        {
            string data = (string)state;
            Console.WriteLine(data);
        }

        static void Main(string[] args)
        {

            int workerThreads = 0;
            int completionThreads = 0;
            
            ThreadPool.SetMinThreads(100, 100);
            ThreadPool.SetMaxThreads(100, 100);
            ThreadPool.GetAvailableThreads(out workerThreads, out completionThreads);
            Console.WriteLine($"Worker threads: {workerThreads}\n Completion threads: {completionThreads}");
            Task.Run(() =>
            {
                Console.WriteLine("Task from pool");
            });
            ThreadPool.QueueUserWorkItem((state) =>
            {
                Console.WriteLine($"Task: {state}");
            });

            ThreadPool.QueueUserWorkItem(new WaitCallback(SampleTask), "State");

            
            //Console.WriteLine($"Worker threads: {workerThreads}\n Completion threads: {completionThreads}");

           

            Console.WriteLine("Hello, World!");
        }
    }
}
