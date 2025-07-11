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