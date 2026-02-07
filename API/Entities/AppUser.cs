namespace API.Entities;


public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string DisplayName { get; set;} // the entity is related to the database
    public required string  Email { get; set;} // we put required in order to say that this field is not null

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
