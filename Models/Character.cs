using System.Text.Json;
using System.Text.Json.Serialization;

namespace SearchNarutoAPI.Models;

public class CharactersResponse
{
    [JsonPropertyName("characters")]
    public List<Character> Characters { get; set; } = new();

    [JsonPropertyName("currentPage")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}

public class Character
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new();

    [JsonPropertyName("jutsu")]
    public List<string> Jutsu { get; set; } = new();

    [JsonPropertyName("personal")]
    public JsonElement? Personal { get; set; }

    [JsonPropertyName("rank")]
    public JsonElement? Rank { get; set; }

    public string? FirstImage => Images.FirstOrDefault(u => !string.IsNullOrWhiteSpace(u));

    // Clã (personal.clan) ou, se ausente, afiliação (personal.affiliation)
    public string ClanOrAffiliation
    {
        get
        {
            var clan = ReadPersonal("clan");
            if (!string.IsNullOrWhiteSpace(clan)) return clan;

            var affiliation = ReadPersonal("affiliation");
            return string.IsNullOrWhiteSpace(affiliation) ? "Não informado" : affiliation;
        }
    }

    // Patente ninja (rank.ninjaRank), pode vir por fase da história
    public IReadOnlyList<string> NinjaRanks
    {
        get
        {
            var result = new List<string>();
            if (Rank is not { ValueKind: JsonValueKind.Object } rank ||
                !rank.TryGetProperty("ninjaRank", out var ninjaRank))
                return result;

            switch (ninjaRank.ValueKind)
            {
                case JsonValueKind.String:
                    result.Add(ninjaRank.GetString()!);
                    break;
                case JsonValueKind.Object:
                    foreach (var p in ninjaRank.EnumerateObject())
                        result.Add($"{p.Name}: {Flatten(p.Value)}");
                    break;
                case JsonValueKind.Array:
                    result.AddRange(ninjaRank.EnumerateArray().Select(Flatten));
                    break;
            }
            return result;
        }
    }

    private string? ReadPersonal(string key)
    {
        if (Personal is not { ValueKind: JsonValueKind.Object } personal ||
            !personal.TryGetProperty(key, out var value))
            return null;

        var text = Flatten(value);
        return string.IsNullOrWhiteSpace(text) ? null : text;
    }

    // Converte string / lista / objeto em texto legível
    private static string Flatten(JsonElement e) => e.ValueKind switch
    {
        JsonValueKind.String => e.GetString() ?? string.Empty,
        JsonValueKind.Array => string.Join(", ", e.EnumerateArray().Select(Flatten)),
        JsonValueKind.Object => string.Join(", ", e.EnumerateObject().Select(p => Flatten(p.Value))),
        JsonValueKind.Null or JsonValueKind.Undefined => string.Empty,
        _ => e.ToString()
    };
}