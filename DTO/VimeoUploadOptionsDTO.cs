using Newtonsoft.Json;

namespace Poc.Vimeo.DTO;

public class VimeoUploadOptionsDTO
{
    public int Size { get; set; }
    public string Approach { get; set; }
    [JsonProperty("redirect_url")]
    public string? RedirectUrl { get; set; }
}
