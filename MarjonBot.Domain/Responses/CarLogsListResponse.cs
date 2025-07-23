using Newtonsoft.Json;

namespace MarjonBot.Domain.Responses;

public class CarLogsListResponse
{
    [JsonProperty("car_id")]
    public int Id { get; set; }

    [JsonProperty("tech_passport")]
    public string TechPassport { get; set; }

    [JsonProperty("distance_covered")]
    public int DistanceCovered { get; set; }

    [JsonProperty("tracker_id")]
    public string TrackerId { get; set; }
}
