using Newtonsoft.Json;

namespace MarjonBot.Domain.Entities;
public class LoginDto
{
    [JsonProperty("phone_number")]
    public required string PhoneNumber { get; set; }

    [JsonProperty("password")]
    public required string Password { get; set; }
}
