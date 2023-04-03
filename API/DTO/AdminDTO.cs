using API.Models;
namespace API.DTO;
public class AdminDTO : Person
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }

    public AdminDTO(string name, string lastName, string email, string password )
    {
        Name = name;
        LastName = lastName;
        Email = email;
        Password = password;

    }
}