using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   
    //Exception class:
   
    public class DateMismatchException : Exception { public DateMismatchException(string message) : base(message) { } }

    public class MailValiditionException : Exception{ public MailValiditionException(string message) : base(message){ }}
   
   public class IncorrectPhoneException : Exception{ public IncorrectPhoneException(string message) : base(message){ }}
   
   public class UnitIsAvailablException : Exception{ public UnitIsAvailablException(string message) : base(message){ }}
   
   public class StartStatusException : Exception{ public StartStatusException(string message) : base(message){ }}
   
   public class GuestStatusException : Exception{ public GuestStatusException(string message) : base(message){ }}
   
   public class NoBankCreditException : Exception{ public NoBankCreditException(string message) : base(message){ }}
 
   public class NoOpenOrderException : Exception{ public NoOpenOrderException(string message) : base(message){ }}
   
   //public class NotImplementedException : Exception{ public NotImplementedException(){ }}
  
}
