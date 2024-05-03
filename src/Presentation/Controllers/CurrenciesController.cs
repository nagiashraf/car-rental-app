using Core.Interfaces.Repositories;

using Microsoft.AspNetCore.Mvc;

using Presentation.DTOs;

namespace Presentation.Controllers;

public class CurrenciesController : BaseController
{
    private readonly ICurrencyRepository currencyRepository;

    public CurrenciesController(ICurrencyRepository currencyRepository)
        => this.currencyRepository = currencyRepository;

    // TODO: What if the languageId doesn't exist?
    [HttpGet("{languageId}")]
    public async Task<ActionResult<IEnumerable<CurrencyDto>>> Get(int languageId)
        => this.Ok((await this.currencyRepository
            .GetAllByLanguageAsync(languageId))
            .Select(c => c.ToCurrencyDto()));
}