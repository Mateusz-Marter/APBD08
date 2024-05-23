namespace APBD08.Models;

public class ClientTrip
{
    public int IdClient { get; set; }
    public int IdTrip { get; set; }
    public DateTime RegisteredAt { get; set; }
    public int? PaymentDate { get; set; }
    public virtual Client IDN { get; set; }
    public virtual Trip ITN { get; set; }
}