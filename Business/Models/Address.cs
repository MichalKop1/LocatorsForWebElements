namespace Business.Models;

public class Address
{
	public string Street { get; set; } = default!;
	public string Suite { get; set; } = default!;
	public string City { get; set; } = default!;
	public string Zipcode { get; set; } = default!;
	public Geo Geo { get; set; } = default!;
}
