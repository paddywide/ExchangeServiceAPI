using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exception
{
    public class HttpResponseNullException : System.Exception
    {

        public HttpResponseNullException(string message) : base(message)
        {
            this.message = message;
        }
        public string message;
    }
}
