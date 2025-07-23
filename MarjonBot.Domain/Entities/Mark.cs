using Newtonsoft.Json;

namespace MarjonBot.Domain.Entities;

public class Mark
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }
}
