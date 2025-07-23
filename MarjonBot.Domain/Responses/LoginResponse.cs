using Newtonsoft.Json;

namespace MarjonBot.Domain.Responses;

public class LoginResponse
{
    [JsonProperty("access")]
    public string Token { get; set; }

    [JsonProperty("refresh")]
    public string Refresh { get; set; }
}
