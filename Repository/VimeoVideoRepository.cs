using Poc.Vimeo.Configuration;
using Poc.Vimeo.Constants;
using Poc.Vimeo.DTO;
using Poc.Vimeo.Model;
using Poc.Vimeo.Services;
using System;
using System.Web;

namespace Poc.Vimeo.Repository;

public class VimeoVideoRepository
{
    private readonly VimeoConfiguration vimeoConfiguration;

    public VimeoVideoRepository() =>
        vimeoConfiguration = ServiceLocator.Get<VimeoConfiguration>()!;
    
    public async Task<VimeoVideo> GetById(int id)
    {
        using HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(VimeoApiUrl.Url)
        };

        client.DefaultRequestHeaders.Add("Authorization", $"bearer {vimeoConfiguration.AuthenticationToken}");
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.vimeo.*+json;version=3.4");

        var response = await client.GetAsync($"{VimeoApiUrl.VideoPath}/{id}?{BuildFieldsParam()}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Vimeo create video error: {response.StatusCode}");
        }

        return await response.Content.ReadAsAsync<VimeoVideo>();
    }

    private string? BuildFieldsParam()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["fields"] = "uri,upload.status,transcode.status";

        return query.ToString();
    }
}
