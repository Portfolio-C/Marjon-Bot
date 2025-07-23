using Newtonsoft.Json;

namespace MarjonBot.Domain.Entities;

public class Car
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("type_id")]
    public string TypeId { get; set; }

    [JsonProperty("state_number")]
    public string StateNumber { get; set; }

    [JsonProperty("mark")]
    public Mark Mark { get; set; }

    [JsonProperty("model")]
    public Model Model { get; set; }
}
