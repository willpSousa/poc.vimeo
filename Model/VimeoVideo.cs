﻿using Newtonsoft.Json;

namespace Poc.Vimeo.Model;

public class VimeoVideo
{
    public string Uri { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Link { get; set; }
    [JsonProperty("player_embed_url")]
    public string PlayerEmbedUrl { get; set; } 
    public VimeoVideoUpload Upload { get; set; }
    public VimeoVideoTranscode Transcode { get; set; }
}
