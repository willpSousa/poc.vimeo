using Newtonsoft.Json;

namespace Poc.Vimeo.Model;

public class VimeoUploadInfo
{
    public string Status { get; set; }
    [JsonProperty("upload_link")]
    public string UploadLink { get; set; }
    public string Form { get; set; }
    public string Approach { get; set; }
    public int? Size { get; set; }
    [JsonProperty("redirect_url")]
    public string RedirectUrl { get; set; }
    public string Link { get; set; }
}
