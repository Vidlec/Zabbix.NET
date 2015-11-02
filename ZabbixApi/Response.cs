using System.Dynamic;

namespace ZabbixApi
{
    //Main
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
