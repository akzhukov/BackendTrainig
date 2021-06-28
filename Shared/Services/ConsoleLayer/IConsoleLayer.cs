using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.ConsoleLayer
{
    public interface IConsoleLayer
    {
        public void WriteLine(string str);
        public string ReadLine();

        public delegate void ConsoleHandler(string message);
        public event ConsoleHandler Notify;

    }
}
