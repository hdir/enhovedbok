public class TrekkMeldingDto
{
    [JsonProperty(PropertyName = "systemId")]
    public int SystemId { get; set; }

    [JsonProperty(PropertyName = "offisiellId")]
    public string OffisiellId { get; set; }

    [JsonProperty(PropertyName = "forsystemReferanser")]
    public string[] ForsystemReferanser { get; set; }

    [JsonProperty(PropertyName = "forsystemReferanserFraPeriode")]
    public string[] AlleForsystemReferanserFraPeriode { get; set; }

    [JsonProperty(PropertyName = "totaltOpprinneligBelop")]
    public decimal TotaltOpprinneligBelop { get; set; }

    [JsonProperty(PropertyName = "totaltTrukketBelop")]
    public decimal TotaltTrukketBelop { get; set; }

    [JsonProperty(PropertyName = "sapTrekkDato")]
    public DateTime SapTrekkDato { get; set; }
    
    [JsonProperty(PropertyName = "kjorenummer")]
    public string Kjorenummer { get; set; }

    [JsonProperty(PropertyName = "trekk", NullValueHandling = NullValueHandling.Ignore)]
    public TrekkTypeOppsummeringDto[] Trekk { get; set; }
}

public class TrekkTypeOppsummeringDto
{
    [JsonProperty(PropertyName = "trekkType", NullValueHandling = NullValueHandling.Ignore)]
    public string TrekkType { get; set; }

    [JsonProperty(PropertyName = "trekkBelop", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? TrekkBelop { get; set; }

    [JsonProperty(PropertyName = "saldoTrekkType", NullValueHandling = NullValueHandling.Ignore)]
    public decimal? SaldoTrekkType { get; set; }
}
