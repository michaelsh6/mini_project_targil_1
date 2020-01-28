using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   

    public class DateMismatchException : Exception { public DateMismatchException(string message) : base(message) { } }

    public class test : Exception{ public test(string message) : base(message){ }}

}
