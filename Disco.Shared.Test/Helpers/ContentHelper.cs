using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Disco.Service.Users.Integration.Helpers;

public static class ContentHelper
{
    public static StringContent GetContent(object value) 
        => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
}