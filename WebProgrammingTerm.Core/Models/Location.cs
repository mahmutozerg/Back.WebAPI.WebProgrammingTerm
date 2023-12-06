﻿namespace WebProgrammingTerm.Core.Models;

public class Location
{
    public string Id { get; set; } = string.Empty;
    public User User { get; set; } = new User();
    public string UserId { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int PostalCode { get; set; } = 0;
    public int No { get; set; } = 0;
    public string PhoneNumber { get; set; } = string.Empty;

}