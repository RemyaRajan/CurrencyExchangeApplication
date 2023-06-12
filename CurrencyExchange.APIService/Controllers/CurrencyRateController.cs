namespace CurrencyExchange.APIService.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Reader")]
public class CurrencyRateController : ControllerBase
{
    private readonly ILogger<CurrencyRateController> logger;
    private readonly ICurrencyExchangeRateRepository currencyExchangeRateRepository;
    public CurrencyRateController(ILogger<CurrencyRateController> logger, ICurrencyExchangeRateRepository currencyExchangeRateRepository)
    {
        this.logger = logger;
        this.currencyExchangeRateRepository = currencyExchangeRateRepository;
    }

    /// <summary>
    /// Api for getting Currency Exchange Rate by code
    ///GET :https://localhost:7035/api/CurrencyRate/EUR/CAD/100
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>

    [HttpGet]
    [Route("{baseCode}/{targetCode}/{amount:decimal}")]
    public IActionResult GetExchageRateByCode(string baseCode, string targetCode, decimal amount)
    {

        logger.LogInformation("GetExchageRate started to execute");

        var exchangeRate = currencyExchangeRateRepository.GetExchageRateByCode(baseCode, targetCode, amount);
        if (exchangeRate == 0)
        {
            return BadRequest();
        }
        logger.LogInformation("GetExchageRate executed");

        return Ok(exchangeRate);

    }

    /// <summary>
    /// Get Currency rate by codes and date
    /// GET :https://localhost:7035/api/CurrencyRate/EUR/CAD/01-05-2023
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="amount"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("{baseCode}/{targetCode}/{amount:decimal}/{date:DateTime}")]
    public IActionResult GetCurrencyRateByDate(string baseCode, string targetCode, decimal amount, DateTime date)
    {
        logger.LogInformation("GetCurrencyRatesByDate started to execute");

        var exchangeRate = currencyExchangeRateRepository.GetCurrencyRatesByDate(baseCode, targetCode, amount, date);
        if (exchangeRate == 0)
        {
            return BadRequest();

        }
        logger.LogInformation("GetCurrencyRatesByDate executed");

        return Ok(exchangeRate);
    }

    /// <summary>
    /// Get currency rate by codes and a date range
    /// https://localhost:7035/api/CurrencyRate/EUR/CAD/01-05-2023/10-06-2023
    /// </summary>
    /// <param name="baseCode"></param>
    /// <param name="targetCode"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("{baseCode}/{targetCode}/{fromDate:DateTime}/{toDate:DateTime}")]
    public IActionResult GetCurrencyRatesByPeriod(string baseCode, string targetCode, DateTime fromDate, DateTime toDate)
    {
        logger.LogInformation("GetCurrencyRatesByPeriod started to execute");

        var currencyRates = currencyExchangeRateRepository.GetCurrencyRatesByPeriod(baseCode, targetCode, fromDate, toDate);
        if (currencyRates == null)
        {
            return NotFound();
        }
        logger.LogInformation("GetCurrencyRatesByPeriod executed");

        return Ok(currencyRates);
    }

    /// <summary>
    /// https://localhost:7035/api/CurrencyRate/GetCurrencies
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    [HttpGet]
    [Route("GetCurrencies")]
    public async Task<IActionResult> GetCurrencies()
    {
        logger.LogInformation("GetCurrencies started to execute");

        var currencies = await currencyExchangeRateRepository.GetCurrencies();
        //throw new Exception();
        if (currencies == null)
        {
            return NotFound();
        }
        logger.LogInformation("GetCurrencies executed");
        return Ok(currencies);

    }
}
