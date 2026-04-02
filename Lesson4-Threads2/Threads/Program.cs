using System.Threading;
using System.Net.Http;
namespace Threads
{
    internal class Program
    {
        static async Task<string> GetDataAsync(CancellationToken cancellationToken)
        {
            await Task.Delay(3000);
            cancellationToken.ThrowIfCancellationRequested();
            using HttpClient client = new HttpClient();
            string result = await client.GetStringAsync(@"https://randomuser.me/api/");
            return $"GET data: {result}";
        }

        static async Task Main(string[] args)
        {

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            //string result = await GetDataAsync();
            //Console.WriteLine(result);
            var task = GetDataAsync(token);

            await Task.Delay(1000);
            cts.Cancel();

            //while (!task.IsCompleted)
            //{
            //    Console.Write(".");
            //    await Task.Delay(1000);
            //}

            try
            {
                Console.WriteLine(await task);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Task was canceled");
            }
        }

        #region ThreadsPool
        static void SampleTask(object state)
        {
            string data = (string)state;
            Console.WriteLine(data);
        }

        static void threads_pool()
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
    #endregion
}
