namespace UrgentCareApp.UnitTests.Services;

public class LoginServiceTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("adsads", "p")]
    [InlineData("ads@akd@", "p")]
    [InlineData("ads@dsa#a.ru", "p")]
    // Тестирование аутентификации с неверными данными
    public async Task UserExists_False(string email, string pass)
    {
        LoginService loginService = new LoginService();
        Assert.Equal("", await loginService.AuthenticateUser(email, pass));
    }

    [Theory]
    [InlineData("a@b.c", "abc")]
    /*[InlineData("a@m.ru.ru", "123")]
    [InlineData("k.p.a@m.ru.ru", "1#121")]
    [InlineData("LongMail@yandex.ru", "1Long##Pass")]*/
    // Тестирование аутентификации с верными данными
    public async Task UserExists_True(string email, string pass)
    {
        // Для работы теста создать пользователя
        /*if (!await Server.UserExistsAsync(email))
            await Server.SaveUserAsync(user);*/

        LoginService loginService = new LoginService();
        Assert.NotEqual("", await loginService.AuthenticateUser(email, pass));
    }
}
