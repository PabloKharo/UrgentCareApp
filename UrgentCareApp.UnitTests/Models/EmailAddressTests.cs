namespace UrgentCareApp.UnitTests.Models;

public class EmailAddressTests
{
    [Theory]
    [InlineData("")]
    [InlineData("adsads")]
    [InlineData("ads@akd@")]
    [InlineData("ads@dsa#a.ru")]
    // Тестирование неверного адреса почты
    public void IsEmailAddress_False(string str)
    {
        EmailAddress email = new(str);
        Assert.False(email.IsEmail());
    }

    [Theory]
    [InlineData("a@m.ru")]
    [InlineData("a@m.ru.ru")]
    [InlineData("k.p.a@m.ru.ru")]
    [InlineData("LongMail@yandex.ru")]
    // Тестирование верного адреса почты
    public void IsEmailAddress_True(string str)
    {
        EmailAddress email = new(str);
        Assert.True(email.IsEmail());
    }
}
