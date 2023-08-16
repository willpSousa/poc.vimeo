using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace Poc.Vimeo;

public static class HttpClientExtensions
{
    public static async Task<T> ReadAsAsync<T>(this HttpContent content)
    {
        var json = await content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static async Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string requestUri, object obj)
    {
        var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        return await client.PostAsync(requestUri, new StringContent(json, Encoding.UTF8, "application/json"));
    }
}