Zabbix.NET
===================


**Zabbix.NET** is .NET Zabbix API library which is incredible easy to use thanks to C# dynamic objects. 
There is no need for special classes for zabbix API methods. You can pass parameters via the anonymous objects. Or just your regular objects.

Library consists of a main class called **Zabbix**. This class has method **callAPI**. 
This method return a json file (string)

Usage:
```
Zabbix zabbix = new Zabbix(user, pass, zabbixUrl);
string json = zabbix.callApi("host.get", new {output: "extend"});
```
Note that you pass API method to Zabbix.callAPI method as a string.

> **Note:**

> - Zabbix.NET uses Newtonsoft.Json library for json serialization and deserialization
> - .NET 4.5.2, but you can compile to any version of .NET as long as it supports ExpandoObject and Newtonsoft.Json 
> - Now available on NuGET **PM> Install-Package Zabbix.NET**