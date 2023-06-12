namespace CurrencyExchange.APIService.Models.Domain;

public class CurrencyRate
{
    public int CurrencyRateId { get; set; }

    public int BaseCurrencyId { get; set; }

    public int TargetCurrencyId { get; set; }

    public decimal ExchageRate { get; set; }

    public DateTime Date { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public bool IsLatest { get; set; }

    public virtual CurrencyMaster BaseCurrency { get; set; } = null!;
}

