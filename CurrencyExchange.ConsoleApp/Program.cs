
using CurrencyExchange.APIService.DataAccess.Mapper;

string connectionString = System.Environment.GetEnvironmentVariable($"ConnectionStrings:connString", EnvironmentVariableTarget.Process);
var serviceProvider = new ServiceCollection()
     .AddLogging((loggingBuilder) => loggingBuilder
        .SetMinimumLevel(LogLevel.Information)
        .AddConsole())
        .AddSingleton<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>()
        .AddDbContext<CurrencyExachangedbContext>(
    options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString))
        .AddAutoMapper(typeof(MappingProfile))
        .BuildServiceProvider();
var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
var repository = serviceProvider.GetService<ICurrencyExchangeRateRepository>();

try
{
    string baseCurrency;
    string targetCurrency;
    decimal amount;
    DateTime? date;
    Console.Write("Enter Base Currency: ");
    baseCurrency = Console.ReadLine();
    Console.Write("Enter Target Currency: ");
    targetCurrency = Console.ReadLine();
    Console.Write("Enter Amount to be converted: ");
    amount = Convert.ToDecimal(Console.ReadLine());
    Console.Write("Enter Exchange Rate Date (in YYYY-MM-DD):");
    string exchangeDate = Console.ReadLine();
    //DateTime y = Convert.ToDateTime(x);
    date =!string.IsNullOrEmpty(exchangeDate) ? Convert.ToDateTime(exchangeDate) :null;
    var outputAmount = await repository.GetExchageRate(baseCurrency, targetCurrency, amount, date);
    Console.WriteLine("Output Amount = " + Math.Round(outputAmount, 2).ToString());
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException);
}
