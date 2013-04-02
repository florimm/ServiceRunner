using System;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface ILogger
    {
        void Debug(string msg, Exception ex);
        void Info(string msg);
        void Error(string msg, Exception ex);
        void Fatal(string msg, Exception ex);
    }
}
