using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrgentCareServer.Data;

namespace UrgentCareTests;

[TestClass]
public class UserControlTests
{
    [TestMethod]
    [DataRow("", "")]
    [DataRow("adsads", "p")]
    [DataRow("ads@akd@", "p")]
    [DataRow("ads@dsa#a.ru", "p")]
    // Тестирование аутентификации с неверными данными
    public async Task UserExists_False(string email, string pass)
    {
        Assert.IsFalse(await Server.UserExistsAsync(email, pass));
    }

    [TestMethod]
    [DataRow("a@b.c", "abc")]
    [DataRow("a@m.ru.ru", "123")]
    [DataRow("k.p.a@m.ru.ru", "1#121")]
    [DataRow("LongMail@yandex.ru", "1Long##Pass")]
    // Тестирование аутентификации с верными данными
    public async Task UserExists_True(string email, string pass)
    {
        UrgentCareServer.Models.User user = new();
        user.Email = email;
        user.Password = pass;
        // Для работы теста создать пользователя
        if (!await Server.UserExistsAsync(email))
            await Server.SaveUserAsync(user);
        Assert.IsTrue(await Server.UserExistsAsync(email, pass));
    }
}