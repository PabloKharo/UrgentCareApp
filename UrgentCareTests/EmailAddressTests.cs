using UrgentCareApp.Models;

namespace UrgentCareTests;

[TestClass]
public class EmailAddressTests
{
    [TestMethod]
    [DataRow("")]
    [DataRow("adsads")]
    [DataRow("ads@akd@")]
    [DataRow("ads@dsa#a.ru")]
    // Тестирование неверного адреса почты
    public void IsEmailAddress_False(string str)
    {
        EmailAddress email = new(str);
        Assert.IsFalse(email.IsEmail());
    }

    [TestMethod]
    [DataRow("a@m.ru")]
    [DataRow("a@m.ru.ru")]
    [DataRow("k.p.a@m.ru.ru")]
    [DataRow("LongMail@yandex.ru")]
    // Тестирование верного адреса почты
    public void IsEmailAddress_True(string str) 
    {
        EmailAddress email = new(str);
        Assert.IsTrue(email.IsEmail());
    }
}
