namespace Synch
{
    internal class Program
    {
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine();
            Mutex mutex = null;
            try
            {
                mutex = Mutex.OpenExisting(AppDomain.CurrentDomain.FriendlyName);
            }
            catch { }
            if (mutex != null) {
                mutex.Close();
                Console.WriteLine("Instance already started");
                Console.ReadKey();  
                return;
            }
            else
            {
                mutex = new Mutex(false, AppDomain.CurrentDomain.FriendlyName);
            }


                Task.Run(Worker);

            Console.WriteLine("Press Enter to complete..");
            Console.ReadLine();

            autoResetEvent.Set();
        }

        static void Worker()
        {
            Console.WriteLine("Thread waiting...");

            autoResetEvent.WaitOne();

            Console.WriteLine("Thread complete waiting. Proceed...");
        }


        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1);

        static async Task Database(int threadId)
        {
            Console.WriteLine($"Thread#{threadId} waiting");

            await semaphoreSlim.WaitAsync();

            Console.WriteLine($"Thread#{threadId} work with db");
            await Task.Delay(2000);

            Console.WriteLine($"Thread#{threadId} done");
            semaphoreSlim.Release();
        }

        static void SemaphoreExample() {
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() => Database(i));
            }
            Console.ReadLine();
        }


       
        static int _counter = 0;
        // ключ
        static readonly object _lock = new();
        //static bool lockTaken = false;
        static void LockExample()
        {
            // Lock(Monitor) - використовуючи таку конструкцію ми обмежуємо доступ до критичної секції
            // тільки один потік має доступ до критичної секції. Як визначити потік який має доступ, у такиї потоків є ключ
            // коли потік заходить у критичну секцію, він забирає ключ.
            // для створення ключа необхідно створити екземпляр класу object
            List<Task> tasks = new();

            for (int i=0; i < 100; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                 
                    for (int j = 0; j < 1000; j++)
                    {
                        //try
                        //{
                        //    Monitor.Enter(_counter, ref lockTaken);
                        //    _counter++;
                        //}
                        //finally
                        //{
                        //    if(lockTaken) Monitor.Exit(_counter); 
                        //}
                        lock (_lock)
                        {
                            // Критична секція:
                            _counter++;
                        }
                    }
                }));
            }

            Task.WaitAll(tasks);

            Console.WriteLine("counter: {0}", _counter);
        }
    }
}
