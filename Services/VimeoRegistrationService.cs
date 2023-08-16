using Poc.Vimeo.Configuration;
using Poc.Vimeo.DTO;
using Poc.Vimeo.Model;
using System.Net.Http.Headers;

namespace Poc.Vimeo.Services;

public class VimeoRegistrationService
{
    private readonly VimeoConfiguration vimeoConfiguration;

    public VimeoRegistrationService()
    {
        vimeoConfiguration = ServiceLocator.Get<VimeoConfiguration>()!;
    }

    public async Task<VimeoUploadResultDTO> Register(IFormFile file)
    {
        var videoInfo = await CreateVideo((int)file.Length);

        return await UploadVideo(videoInfo, file);
    }

    private async Task<VimeoVideo?> CreateVideo(int fileSize)
    {
        using HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(vimeoConfiguration.Url)
        };

        client.DefaultRequestHeaders.Add("Authorization", $"bearer {vimeoConfiguration.AuthenticationToken}");
        client.DefaultRequestHeaders.Add("Accept", "application/vnd.vimeo.*+json;version=3.4");

        var tenant = new TenantService().GetTenant();

        var content = new VimeoVideoCreateOptionsDTO
        {
            Name = $"{tenant.Host}/{Guid.NewGuid()}",
            Privacy = new VimeoPrivacyOptionsDTO
            {
                View = "nobody"
            },
            Upload = new VimeoUploadOptionsDTO
            {
                Approach = "post",
                Size = fileSize,
                RedirectUrl = $"https://{tenant.Host}/api/vimeo/complete"
            }
        };

        var response = await client.PostAsJsonAsync(vimeoConfiguration.UploadPath, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Vimeo create video error: {response.StatusCode}");
        }

        return await response.Content.ReadAsAsync<VimeoVideo>();
    }

    private async Task<VimeoUploadResultDTO> UploadVideo(VimeoVideo uploadInfo, IFormFile file)
    {
        byte[] fileBytes;

        await using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            fileBytes = stream.ToArray();
        }

        using var fileContent = new ByteArrayContent(fileBytes, 0, fileBytes.Length);

        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
        fileContent.Headers.ContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

        var boundary = Guid.NewGuid().ToString();

        using var formData = new MultipartFormDataContent(boundary)
        {
            { fileContent, "file_data", uploadInfo.Name}
        };

        formData.Headers.Remove("Content-Type");
        formData.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);

        using var client = new HttpClient();

        client.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");

        var response = await client.PostAsync(uploadInfo.Upload.UploadLink, formData);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error upload vimeo video: {response.StatusCode}");
        }

        return await response.Content.ReadAsAsync<VimeoUploadResultDTO>();
    }
}
