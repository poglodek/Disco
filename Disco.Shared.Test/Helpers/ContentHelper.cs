using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Disco.Shared.Test.Helpers;

public static class ContentHelper
{
    public static StringContent GetContent(object value) 
        => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    
    
    public static async Task<T?> ReturnObjectFromContent<T>(HttpResponseMessage result)
    {
        var text = await result.Content.ReadAsStringAsync();
        var obj = JsonConvert.DeserializeObject<T>(text);
        return obj;
    }
}