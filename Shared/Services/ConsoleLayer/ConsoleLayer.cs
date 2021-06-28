using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.ConsoleLayer
{
    public class ConsoleLayer : IConsoleLayer
    {
        public event IConsoleLayer.ConsoleHandler Notify;

        public string ReadLine()
        {
            string str = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Notify?.Invoke($"User entered: \"{str}\" at {DateTime.Now}");
            Console.ResetColor();
            return str;
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }
    }
}
