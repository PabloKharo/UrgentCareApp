using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OnmpApp.Helpers;
using OnmpApp.Models;
using OnmpApp.Properties;

using OnmpApp.Services.Database;

namespace OnmpApp.Services.Authorize;

public static class LoginService
{

    public static async Task<bool> AuthenticateUser(string email, string password)
    {
        try
        {
            using var client = new HttpClient();
            var json = new
            {
                email = email,
                password = password
            };

            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(json), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Settings.ApiAddress}token/", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var token = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["token"];
                Settings.Token = token;

                if (!await DatabaseService.UserEmailExists(email))
                    await DatabaseService.UserCreate(email);

                return true;
            }
            else
            {
                var error = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["error"];
                throw new Exception($"{error}");
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
