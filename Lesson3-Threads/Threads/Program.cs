using System.Threading;

namespace Threads
{
    internal class Program
    {
        static void taskWithParametr(object param)
        {
            string msg = (string)param;
            Console.WriteLine($"Message: {msg}");
        }
        static void printNumbers()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Background thread: {i}");
                Thread.Sleep(2000);
            }
        }
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            ////Thread - клас який дозволяє управляти потоками, запускати нові потоки

            //Thread.Sleep(5000);

            //Console.WriteLine("Bye, world");

            Thread thread = new(printNumbers);
            // За замовчування усі потоки є Foreground(приорітетні), програма не завершиться до тих пір, поки усі потоки не виконаються
            // Коли ми робимо потік фоновим, це означає, що, якщо основний потік завершує свою роботу, то з ним примусово завершуються і 
            // фонові потоки, навіть якщо вони не завершили свою задачу.
            thread.IsBackground = true;
            thread.Start();

            thread.Join();

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Main thread works..");
                Thread.Sleep(1000);
            }

            //Thread thread2 = new(taskWithParametr);
            //thread2.Start("msg from main thread");
        }
    }
}
