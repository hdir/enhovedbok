using System;

// det sendes en trekk-kvittering per kontakt. Dvs alle forsystemreferanser er knyttet til utbetaling til samme person.
public class TrekkKvitteringNav
{
    public int SystemId { get; set; }

    public string[] ForsystemReferanser { get; set; }
    
    public string[] TrekkTyper { get; set; }

    public decimal TotaltOpprinneligBelop { get; set; }

    public decimal TotaltTrukketBelop { get; set; }

    public DateTime SapTrekkDato { get; set; }
}
