using System.Net.Http.Json;
using SearchNarutoAPI.Models;

namespace SearchNarutoAPI.Services;

public class NarutoApiService
{
    private readonly HttpClient _http;

    public NarutoApiService(HttpClient http) => _http = http;

    public async Task<CharactersResponse> GetCharactersAsync(
        string? name = null, int page = 1, int limit = 12, CancellationToken ct = default)
    {
        var url = $"characters?page={page}&limit={limit}";
        if (!string.IsNullOrWhiteSpace(name))
            url += $"&name={Uri.EscapeDataString(name.Trim())}";

        try
        {
            return await _http.GetFromJsonAsync<CharactersResponse>(url, ct)
                   ?? new CharactersResponse();
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new CharactersResponse(); // busca sem resultado
        }
    }

    public Task<Character?> GetCharacterByIdAsync(int id, CancellationToken ct = default)
        => _http.GetFromJsonAsync<Character>($"characters/{id}", ct);
}