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

    [JsonProperty(PropertyName = "trekk", NullValueHandling = NullValueHandling.Ignore)]
    public TrekkTypeOppsummeringDto[] Trekk { get; set; }
}
