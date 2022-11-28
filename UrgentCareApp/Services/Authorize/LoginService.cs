using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UrgentCareApp.Helpers;
using UrgentCareApp.Models;

namespace UrgentCareApp.Services.Authorize;

public class LoginService
{

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    private const string ApiUrlBase = "api/login";

    public LoginService()
    {
        _client = new HttpClient();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<string> AuthenticateUser(string email, string password)
    {
        Uri uri = new Uri(UriHelper.CombineUri(Settings.ServerAddress, ApiUrlBase));

        try
        {
            if (email == "a@b.c" && password == "abc")
                return "123";

            /*User user = new User(email, password);
            string json = JsonSerializer.Serialize<User>(user, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
                return string.Empty;

            string cnt = await response.Content.ReadAsStringAsync();
            user = JsonSerializer.Deserialize<User>(cnt, _serializerOptions);

            return user.AuthToken;*/
        }
        catch (Exception ex)
        {
#if DEBUG
            Debug.WriteLine(@"\tERROR {0}", ex.Message);
#endif
            ToastHelper.Show(Properties.Resources.ErrorServerConnection);
        }
        
        return string.Empty;
    }
}
