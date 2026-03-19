namespace SimpleLib
{
    public class Messenger
    {
        public string GetMessage(string param)
        {
            return $"Hello from DLL. Your param: {param}";
        }
    }
}
