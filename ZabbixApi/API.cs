using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Dynamic;

namespace ZabbixApi
{


    //Main
    public class API
    {
        public API(string user, string password, string zabbixURL)
        {
            this.user = user;
            this.password = password;
            this.zabbixURL = zabbixURL;
            initialConnect();
        }

        private string user;
        private string password;
        private string zabbixURL;
        private string auth;

        private void initialConnect()
        {
            dynamic userAuth = new ExpandoObject();
            userAuth.user = user;
            userAuth.password = password;
            Response zbxResponse = JsonConvert.DeserializeObject<Response>(callApi("user.login", userAuth));
            auth = zbxResponse.result;          
        }

        public string callApi(string method, object parameters)
        {
            Request zbxRequest = new Request("2.0", method, 1, auth, parameters);
            string jsonParams = JsonConvert.SerializeObject(zbxRequest);
            return sendRequest(jsonParams);
        }

        private string sendRequest(string jsonParams)
        {
            WebRequest request = WebRequest.Create(zabbixURL);
            request.ContentType = "application/json-rpc";
            request.Method = "POST";
            string jsonResult;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonParams);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonResult = streamReader.ReadToEnd();
            }

            return jsonResult;
        }


    }
}
