using OnmpApp.Helpers;
using OnmpApp.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Services.Database;

namespace OnmpApp.Services.Authorize;

public class RegistrationService
{
    public RegistrationService() { }

    public async Task<bool> RegisterUser(string email, string password, string firstName, string secondName)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var json = new
                {
                    email = email,
                    password = password,
                    first_name = firstName,
                    last_name = secondName
                };


                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(json), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{Settings.ApiAddress}user_create/", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    _ = await DatabaseService.UserCreate(email);
                    return true;
                }
                else
                {
                    // TODO: исправить тут
                    var error = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["email"];
                    throw new Exception($"{error}");
                }
            }
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"Ошибка: {0}", ex.Message);
#endif
            ToastHelper.Show($"Ошибка: {ex.Message}");
        }

        return false;
    }

}
