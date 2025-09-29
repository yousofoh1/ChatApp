using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions;

public class IdentityException:AppException
{
    public readonly object Errors=default!;
    public IdentityException(object errors)
    {
        this.Errors = errors;
    }
}
