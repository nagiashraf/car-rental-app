namespace Presentation.DTOs;

public class LanguageDto
{
    public LanguageDto(int id, string code, string englishName, string nativeName, string flagImagePath, bool isRtl)
    {
        this.Id = id;
        this.Code = code;
        this.EnglishName = englishName;
        this.NativeName = nativeName;
        this.FlagImagePath = flagImagePath;
        this.IsRtl = isRtl;
    }

    public int Id { get; set; }

    public string Code { get; set; }

    public string EnglishName { get; set; }

    public string NativeName { get; set; }

    public string FlagImagePath { get; set; }

    public bool IsRtl { get; set; }
}