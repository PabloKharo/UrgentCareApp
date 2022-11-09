using UrgentCareApp.Pages.AuthorizePages;

namespace UrgentCareTests;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("")]
    [DataRow("adsads")]
    [DataRow("ads@akd@")]
    [DataRow("ads@dsa#a.ru")]
    public void IsEmail_False(string str)
    {
        UrgentCareApp.Pages.AuthorizePages.Email email = new();
        email.Value = str;
        Assert.IsFalse(email.IsEmail());
    }

    [TestMethod]
    [DataRow("a@m.ru")]
    [DataRow("a@m.ru.ru")]
    [DataRow("k.p.a@m.ru.ru")]
    [DataRow("LongMail@yandex.ru")]
    public void IsEmail_True(string str) 
    {
        UrgentCareApp.Pages.AuthorizePages.Email email = new();
        email.Value = str;
        Assert.IsTrue(email.IsEmail());
    }
}
