using UrgentCareApp.Models;

namespace UrgentCareTests;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("")]
    [DataRow("adsads")]
    [DataRow("ads@akd@")]
    [DataRow("ads@dsa#a.ru")]
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
    public void IsEmailAddress_True(string str) 
    {
        EmailAddress email = new(str);
        Assert.IsTrue(email.IsEmail());
    }
}
