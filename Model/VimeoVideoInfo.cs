namespace Poc.Vimeo.Model;

public class VimeoVideoInfo
{
    public string Uri { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Link { get; set; }
    public VimeoUploadInfo Upload { get; set; }
}
