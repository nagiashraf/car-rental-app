namespace Presentation.DTOs;

public class CurrencyDto
{
    public CurrencyDto(int id, string code, string name)
    {
        this.Id = id;
        this.Code = code;
        this.Name = name;
    }

    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}