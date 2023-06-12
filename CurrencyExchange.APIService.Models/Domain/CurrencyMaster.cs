namespace CurrencyExchange.APIService.Models.Domain;

public partial class CurrencyMaster
{
    public int CurrencyId { get; set; }

    public string CurrencyCode { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDateTime { get; set; }


    public virtual ICollection<CurrencyRate> TblCurrencyRates { get; set; } = new List<CurrencyRate>();
}
