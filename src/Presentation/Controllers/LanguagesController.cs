using Core.Interfaces.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Presentation.DTOs;

namespace Presentation.Controllers;

public class LanguagesController : BaseController
{
    private readonly ILanguageRepository languageRepository;
    private readonly string baseUrl;

    public LanguagesController(ILanguageRepository languageRepository, IOptions<EnvironmentConfig> options)
    {
        this.languageRepository = languageRepository;
        this.baseUrl = options.Value.BaseUrl;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LanguageDto>>> Get()
    {
        var languages = await this.languageRepository.GetAllAsync();
        return this.Ok(languages.Select(l => l.ToLanguageDto(this.baseUrl)));
    }
}