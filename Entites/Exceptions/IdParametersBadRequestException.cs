using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public sealed class IdParametersBadRequestException : Exception
    {
        public IdParametersBadRequestException() :base("Parameter ids is null.") { }
    }

}
