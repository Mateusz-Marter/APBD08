﻿namespace APBD08.Models;

public class CountryTrip
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }
    
    public virtual Country IdCountryNavigation { get; set; }
    public virtual Trip IdTripNavigation { get; set; }
}