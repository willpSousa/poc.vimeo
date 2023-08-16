namespace Poc.Vimeo.DTO;

public class VimeoVideoCreateOptionsDTO
{
    public string Name { get; set; }
    public VimeoUploadOptionsDTO Upload { get; set; }
    public VimeoPrivacyOptionsDTO Privacy { get; set; }
}
