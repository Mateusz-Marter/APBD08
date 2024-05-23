﻿namespace APBD08.Models;

public class Client
{
    public int IdClient { get; set; }
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
    public String? Email { get; set; }
    public String? Telephone { get; set; }
    public String? Pesel { get; set; }
    public virtual ICollection<ClientTrip> ClientTrips { get; set; }
}