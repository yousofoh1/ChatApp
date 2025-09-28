using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public class AppException: Exception
{
    object Details = "";
    public AppException()
    {
        
    }
    public AppException(string message): base(message)
    {

    }
    public AppException(object message)
    {
        Details = message;
    }
}
