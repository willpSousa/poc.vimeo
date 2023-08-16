namespace Poc.Vimeo.Model;

public class VimeoVideo
{
    public string Uri { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Link { get; set; }
    public VimeoVideoUpload Upload { get; set; }
}
