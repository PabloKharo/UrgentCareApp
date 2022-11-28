using SQLite;

namespace UrgentCareApp.Models;

// TODO: Пользователи БД, после создания сервера, убрать
[Table("Users")]
public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [NotNull, Indexed(Name = "email_idx", Order = 1, Unique = true)]
    public string Email { get; set; }
    [NotNull]
    public string Password { get; set; }

    public User() { }
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
