using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using SimpleLib;

namespace SysProgP48
{
    internal class Program
    {

        // C# -> CLR -> PInvoke -> DLL -> WinAPI -> Windows -> MessageBox

        [DllImport("user32.dll", SetLastError =true, CharSet =CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll")]
        static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

        [DllImport("kernel32.dll")]
        static extern bool Beep(int freq, int duration);

        [DllImport("advapi32.dll")]
        static extern bool GetUserName(StringBuilder sb, ref int length);

        static unsafe void main()
        {
            int x = 10;
            int* ptr = &x;

            Console.WriteLine("Value: {0}", *ptr);
        }

        static void make_dynamic_array(int length)
        {
            int size = sizeof(int) * length;

            IntPtr ptr = Marshal.AllocHGlobal(size);

            try
            {
                for (int i = 0; i < length; i++)
                {
                    Marshal.WriteInt32(ptr, i * sizeof(int), i * 10);
                }

                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine("Value: {0}, Index: {1}",
                        Marshal.ReadInt32(ptr, i * sizeof(int)),
                        i
                        );
                }
            }
            finally { Marshal.FreeHGlobal(ptr); }
        }

        static void Legacy()
        {
            main();
            make_dynamic_array(10);
            // API -Application Programming Interface

            //Backend:
            /* HTTP, HTTPS
             * GET POST PUT DELETE
             /user?username=super_pro GET -> {"username": "super_pro", "id": "21-4-32"} 
             /user/create POST
             */
            // DLL - Dynamic Linl Library

            //windowsAPI
            //MessageBox(IntPtr.Zero, "Hello", "Our Message Box", 0);

            //Console.WriteLine(MessageBox(IntPtr.Zero, "Hello", "Our Message Box", 0x24)); // yes/no + question_icon

            Beep(1000, 10);

            int length = 256;
            StringBuilder sb = new StringBuilder(length);

            if (GetUserName(sb, ref length))
            {
                Console.WriteLine("User: {0}", sb.ToString());
            }

            int size = sizeof(int);

            IntPtr ptr = Marshal.AllocHGlobal(size);

            try
            {
                Marshal.WriteInt32(ptr, 42);
                int val = Marshal.ReadInt32(ptr);
                Console.WriteLine("Addres: {0}", ptr);
                Console.WriteLine("Value: {0}", val);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

        }

        static void ChangeWindowName()
        {
            IntPtr notepadHandle = FindWindow("cmd", null);

            const uint WM_SETTEXT = 0x000C;

            if (notepadHandle != IntPtr.Zero)
            {
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string newTitle = $"Notepad - Time: {currentTime}";

                SendMessage(notepadHandle, WM_SETTEXT, IntPtr.Zero, newTitle);
            }
            else
            {
                Console.WriteLine("Notepad process not found");
            }
        }

        static void LoadDLL(string dllPath)
        {
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException(dllPath);
            }

            // System.Reflection - це набір інструментів або механізм, який дозволяє, програмі "вивчати саму себе" під час виконання.
            // Рефлексія - дозволяє програмі дізнатися: які класи, методи, параметри в неї є. Також дозволяє створювати екземпляри цих класів, та
            // виконувати методи

            // Завантажуємо збірку
            Assembly assembly = Assembly.LoadFrom(dllPath);

            // Отримуємо тип нашого класу Месенджер
            Type? messengerType = assembly.GetType("SimpleLib.Messenger");

            if (messengerType != null)
            {
                // Створюємо новий об'єкт нашого класу Месенджер
                object? messengerInstance = Activator.CreateInstance(messengerType);
                if (messengerInstance != null)
                {
                    // з класу Месенджер дістаємо метод GetMessage
                    MethodInfo? method = messengerType.GetMethod("GetMessage");
                    if (method != null)
                    {
                        // Викликаємо метод GetMessage, передаємо параметр
                        object? result = method.Invoke(messengerInstance, new object[] { "MyParam" });
                        if (result != null)
                        {
                            Console.WriteLine("Invoke result: {0}", result);
                        }
                    }
                }
                else throw new Exception("Class not found");
            }
        }

        static void UseMyDLL()
        {
            Messenger messenger = new Messenger();
            Console.WriteLine(messenger.GetMessage("MyParam"));
        }
        static void Main(string[] args)
        {
            LoadDLL(@"C:\Users\dsgnrr\source\repos\SimpleLib\bin\Debug\net10.0\SimpleLib.dll");
            UseMyDLL();
            ChangeWindowName();
        }

    }
}
