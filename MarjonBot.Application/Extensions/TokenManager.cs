using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MarjonBot.Application.Extensions;
internal static class TokenManager
{
    public static async Task UpdateAccessTokenAsync(string token, string path = "appsettins.json")
    {
        if (!File.Exists(path))
        {
            return;
        }

        var json = await File.ReadAllTextAsync(path);
        var jsonObject = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

        if (!jsonObject.ContainsKey("Api"))
        {
            jsonObject["Api"] = new Dictionary<string, object>();
        }

        var apiSection = (jsonObject["Api"] as JObject);
        apiSection["Token"] = token;

        jsonObject["Api"] = apiSection;

        var upadtedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);

        await File.WriteAllTextAsync(path, upadtedJson);

        Console.WriteLine("Token has been successfully updated!");
    }

    public static void AddAuthorizationHeader(HttpClient httpClient, IConfiguration configuration)
    {
        var token = configuration["Api:Token"];
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
