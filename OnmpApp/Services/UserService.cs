using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnmpApp.Database;
using OnmpApp.Helpers;
using OnmpApp.Properties;

namespace OnmpApp.Services;

public static class UserService
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

                await UserTable.Insert(email);

                return true;
            }

            var error = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)["error"];
            throw new Exception($"{error}");
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

    public static async Task<bool> RegisterUser(string email, string password, string first_name, string last_name)
    {
        try
        {
            using var client = new HttpClient();
            var json = new
            {
                email = email,
                password = password,
                first_name = first_name,
                last_name = last_name
            };


            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(json), Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Settings.ApiAddress}user_create/", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                _ = await UserTable.Insert(email);
                return true;
            }

            var errorContent = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<string>>>(responseContent);
            if (errorContent.TryGetValue("email", out var value))
            {
                var emailErrors = string.Join(", ", value);
                throw new Exception($"{emailErrors}");
            }
            else if (errorContent.TryGetValue("password", out var value1))
            {
                var passwordErrors = string.Join(", ", value1);
                throw new Exception($"{passwordErrors}");

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

    public static async Task<int> GetId(string email)
    {
        try
        {
            return await UserTable.GetId(email);
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"Ошибка: {0}", ex.Message);
#endif
            ToastHelper.Show($"Ошибка: {ex.Message}");
        }

        return -1;
    }
}