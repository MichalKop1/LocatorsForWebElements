namespace Business.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Address Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Website { get; set; } = default!;
    public Company Company { get; set; } = default!;
}

public class Address
{
    public string Street { get; set; } = default!;
    public string Suite { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Zipcode { get; set; } = default!;
    public Geo Geo { get; set; } = default!;
}

public class Geo
{
    public string Lat { get; set; } = default!;
    public string Lng { get; set; } = default!;
}

public class Company
{
    public string Name { get; set; } = default!;
    public string CatchPhrase { get; set; } = default!;
    public string Bs { get; set; } = default!;
}