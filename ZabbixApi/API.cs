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
        }

        string user;
        string password;
        string zabbixURL;
        string auth;

        string jsonResult;
        string jsonParams;

        WebRequest request;
        WebResponse response;
        Request zbxRequest;
        Response zbxResponse;

        public void initialConnect()
        {
            dynamic userAuth = new ExpandoObject();
            userAuth.user = user;
            userAuth.password = password;

            zbxRequest = new Request("2.0", "user.login", 1, null, userAuth);
            jsonParams = JsonConvert.SerializeObject(zbxRequest);
            zbxResponse = JsonConvert.DeserializeObject<Response>(sendRequest());
            auth = zbxResponse.result;          
        }

        public string callApi(string method, object parameters)
        {
            zbxRequest = new Request("2.0", method, 1, auth, parameters);
            jsonParams = JsonConvert.SerializeObject(zbxRequest);
            return sendRequest();
        }

        private string sendRequest()
        {
            
            request = WebRequest.Create(zabbixURL);
            request.ContentType = "application/json-rpc";
            request.Method = "POST";

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonParams);
                streamWriter.Flush();
                streamWriter.Close();
            }

            response = request.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonResult = streamReader.ReadToEnd();
            }

            return jsonResult;
        }


    }
}
