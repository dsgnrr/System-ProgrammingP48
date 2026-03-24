using System.Diagnostics;


namespace Process_Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Process.Start("notepad.exe");

            //Process.Start("explorer.exe", @"C:\Users\semenyuk_o\Downloads\");

            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "--version",
                RedirectStandardOutput = true,
                CreateNoWindow = false,
                UseShellExecute = false
            };

            using var process = Process.Start(startInfo);
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            Console.WriteLine(result);

            Console.WriteLine(Process.GetCurrentProcess().ProcessName);

            foreach (var item in Process.GetProcesses())
            {
                Console.WriteLine(item.ProcessName);
            }

            var notepad = Process.Start("notepad.exe");

            notepad.WaitForExit();

            Console.WriteLine("notepad closed");
            Console.ReadKey();

            notepad.CloseMainWindow();

            Console.ReadKey();

            notepad.Kill();
        }
    }
}
