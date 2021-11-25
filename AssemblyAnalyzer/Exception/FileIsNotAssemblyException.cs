using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyAnalyzer.Exception
{
    public class FileIsNotAssemblyException : ArgumentException
    {
        public FileIsNotAssemblyException() { }

        public FileIsNotAssemblyException(string message)
            : base(message) { }

        public FileIsNotAssemblyException(string message, System.Exception inner)
            : base(message, inner) { }
    }
}
