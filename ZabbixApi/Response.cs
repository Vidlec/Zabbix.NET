using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabbixApi
{
    class Response
    {
        public Response()
        {
        }

        public string jsonrpc { get; set; }
        public dynamic result = new ExpandoObject();
        public int id { get; set; }
    }
}
