using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Exceptions
{
    public sealed class CompanyCollectionBadRequest:Exception
    {
        public CompanyCollectionBadRequest() : base("Company collection sent from a client is null")
        {
            
        }
    }
}
