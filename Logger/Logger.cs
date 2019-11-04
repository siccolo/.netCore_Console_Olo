using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Logger
{
    public interface ILogger
    {
        void LogError(Exception errorInfo
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0);

        void LogError(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0);

        void LogInfo(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0);
    }

    public class Logger : ILogger
    {
        public Logger()
        {

        }

        public void LogError(Exception errorInfo
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine($"Error at {callingMethodName} {errorInfo.Message}");
        }


        public void LogError(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
            )
        {
            Console.WriteLine($"Error at {callingMethodName} {message}");
        }

        public void LogInfo(string message
                    , [System.Runtime.CompilerServices.CallerMemberName] string callingMethodName = ""
                    , [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = ""
                    , [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0
            )
        {
            Console.WriteLine($"Info at {callingMethodName} {message}");
        }
    }
}
