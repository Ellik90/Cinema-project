namespace API.Models;

public class Admin : Person
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public Admin(string name, string lastName, string email, string password )
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;

    }
}