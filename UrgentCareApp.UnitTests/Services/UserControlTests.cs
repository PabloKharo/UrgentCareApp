namespace UrgentCareApp.UnitTests.Services;

public class UserControlTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("adsads", "p")]
    [InlineData("ads@akd@", "p")]
    [InlineData("ads@dsa#a.ru", "p")]
    // Тестирование аутентификации с неверными данными
    public async Task UserExists_False(string email, string pass)
    {
        Assert.False(await Server.UserExistsAsync(email, pass));
    }

    [Theory]
    [InlineData("a@b.c", "abc")]
    [InlineData("a@m.ru.ru", "123")]
    [InlineData("k.p.a@m.ru.ru", "1#121")]
    [InlineData("LongMail@yandex.ru", "1Long##Pass")]
    // Тестирование аутентификации с верными данными
    public async Task UserExists_True(string email, string pass)
    {
        User user = new(email, pass);

        // Для работы теста создать пользователя
        if (!await Server.UserExistsAsync(email))
            await Server.SaveUserAsync(user);
        Assert.True(await Server.UserExistsAsync(email, pass));
    }
}
