using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Client.Models;

namespace Client.Services
{
    public class PatternApiClient
    {
        private readonly HttpClient _http;
        private readonly RequestLogService _log;
        public string BaseUrl { get; private set; } = "/api/Patterns/";
        public event Action<string>? OnError;

        public PatternApiClient(HttpClient http, RequestLogService log)
        {
            _http = http;
            _log = log;
        }

        public void SetBaseUrl(string url)
        {
            BaseUrl = url.EndsWith("/") ? url : url + "/";
        }

        private async Task<T?> GetAsync<T>(string url, int retries = 2)
        {
            for (int i = 0; i <= retries; i++)
            {
                try
                {
                    string fullUrl;
                    if (url.StartsWith("/api/"))
                        fullUrl = url;
                    else
                        fullUrl = BaseUrl.TrimEnd('/') + "/" + url.TrimStart('/');

                    _log.LogRequest(fullUrl);
                    var resp = await _http.GetAsync(fullUrl);
                    var content = await resp.Content.ReadAsStringAsync();
                    _log.LogResponse(fullUrl, content, resp.IsSuccessStatusCode);
                    if (resp.IsSuccessStatusCode)
                    {
                        if (typeof(T) == typeof(string))
                            return (T)(object)content;
                        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        OnError?.Invoke($"API error: {resp.StatusCode} {content}");
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    if (i == retries)
                    {
                        OnError?.Invoke($"Network/API error: {ex.Message}");
                        return default;
                    }
                    await Task.Delay(500);
                }
            }
            return default;
        }

        public class StatsDto
        {
            public int Competitions { get; set; }
            public int Matches { get; set; }
            public int Notifications { get; set; }
        }

        public class LocationDto
        {
            public string Name { get; set; } = "";
        }

        public class CompetitionDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Sport { get; set; } = "";
            public string Type { get; set; } = "";
            public string Location { get; set; } = "";
            public bool Started { get; set; }
            public List<string> Participants { get; set; } = new();
        }

        public class ParticipantDto
        {
            public string Kind { get; set; } = "";
            public string Name { get; set; } = "";
        }
        
        public class IteratedMatchesDto
        {
            public int CompetitionId { get; set; }
            public List<MatchItemDto> Matches { get; set; } = new();
        }
        
        public class MatchItemDto
        {
            public string Name { get; set; } = "";
            public string Stadium { get; set; } = "";
        }

        public Task<StatsDto?> GetStatsAsync() => GetAsync<StatsDto>("/api/Stats");

        // Creational
        public Task<string?> ConfigureTournamentAsync(string type) => GetAsync<string>($"/api/Patterns/abstractfactory/configure?tournamentType={Uri.EscapeDataString(type)}");
        public Task<string?> GenerateRoundsAsync(string type) => GetAsync<string>($"/api/Patterns/factorymethod/generate?roundsType={Uri.EscapeDataString(type)}");
        public Task<string?> BuildCompetitionAsync(string name, string sport, string type, string location, string[] participants, int organizerId = 0) 
        {
            var pQuery = participants != null && participants.Length > 0 
                ? "&" + string.Join("&", participants.Select(p => $"participants={Uri.EscapeDataString(p)}"))
                : "";
            return GetAsync<string>($"/api/Patterns/builder/build?name={Uri.EscapeDataString(name)}&sport={Uri.EscapeDataString(sport)}&type={Uri.EscapeDataString(type)}&location={Uri.EscapeDataString(location)}&organizerId={organizerId}{pQuery}");
        }
        public Task<string?> CreateParticipantAsync(string kind, string name) => GetAsync<string>($"/api/Participants/create?kind={Uri.EscapeDataString(kind)}&name={Uri.EscapeDataString(name)}");

        public Task<List<ParticipantDto>?> ListParticipantsAsync() => GetAsync<List<ParticipantDto>>("/api/Participants/list");
        public Task<string?> DeleteParticipantAsync(string name) => GetAsync<string>($"/api/Participants/delete?name={Uri.EscapeDataString(name)}");

        // Manager / Competition
        public Task<List<CompetitionDto>?> ListCompetitionsAsync() => GetAsync<List<CompetitionDto>>("/api/Competitions/list");
        public Task<string?> UpdateCompetitionAsync(int id, string sport, string type, string location) => GetAsync<string>($"/api/Competitions/update?id={id}&sport={Uri.EscapeDataString(sport)}&type={Uri.EscapeDataString(type)}&location={Uri.EscapeDataString(location)}");
        public Task<string?> AttachParticipantAsync(int compId, string pName) => GetAsync<string>($"/api/Competitions/participant?compId={compId}&participantName={Uri.EscapeDataString(pName)}");

        // Structural
        public Task<string?> GetVenueInfoAsync(string key) => GetAsync<string>($"/api/Patterns/flyweight/venue?venueKey={Uri.EscapeDataString(key)}");
        public Task<List<LocationDto>?> ListLocationsAsync() => GetAsync<List<LocationDto>>("/api/Venues/list");
        public Task<string?> AddVenueAsync(string name) => GetAsync<string>($"/api/Venues/add?name={Uri.EscapeDataString(name)}");
        public Task<string?> DeleteVenueAsync(string name) => GetAsync<string>($"/api/Venues/delete?name={Uri.EscapeDataString(name)}");
        public Task<string?> EditVenueAsync(string oldName, string newName) => GetAsync<string>($"/api/Venues/edit?oldName={Uri.EscapeDataString(oldName)}&newName={Uri.EscapeDataString(newName)}");
        
        public async Task<JsonElement?> StartCompetitionAsync(int id) => await GetAsync<JsonElement>($"/api/Patterns/proxy/start?competitionId={id}");
        public async Task<JsonElement?> SendNotificationAsync(string channel, string message) => await GetAsync<JsonElement>($"/api/Patterns/bridge/notify?channel={Uri.EscapeDataString(channel)}&message={Uri.EscapeDataString(message)}");
        public async Task<JsonElement?> StartFacadeAsync(int organizerId) => await GetAsync<JsonElement>($"/api/Patterns/facade/start?organizerId={organizerId}");
        public async Task<JsonElement?> DuplicateCompetitionAsync(int compId) => await GetAsync<JsonElement>($"/api/Patterns/prototype/clone?id={compId}");

        public Task<string?> SendInviteAsync(string tournament, string channel) => GetAsync<string>($"/api/Patterns/bridge/notify?channel={Uri.EscapeDataString(channel)}&message={Uri.EscapeDataString($"Invitation to Join: {tournament}")}");
        public Task<string?> SendResultAsync(int matchId, string channel) => GetAsync<string>($"/api/Patterns/bridge/notify?channel={Uri.EscapeDataString(channel)}&message={Uri.EscapeDataString($"Match result for match {matchId}")}");
        public Task<List<MatchDto>?> ListMatchesAsync() => GetAsync<List<MatchDto>>("/api/Matches/list");

        // Behavioral
        public Task<IteratedMatchesDto?> IterateMatchesAsync(int competitionId, string? stadium = null) 
            => GetAsync<IteratedMatchesDto>($"/api/Matches/iterate?competitionId={competitionId}&stadium={Uri.EscapeDataString(stadium ?? string.Empty)}");
        public async Task<JsonElement?> SubscribeLiveMatchAsync(int matchId) => await GetAsync<JsonElement>($"/api/Patterns/observer/subscribe?matchId={matchId}");
        public async Task<JsonElement?> ExecuteCommandAsync(string commandName) => await GetAsync<JsonElement>($"/api/Patterns/command/execute?commandName={Uri.EscapeDataString(commandName)}");
        public async Task<JsonElement?> ApplyStrategyAsync(string strategyName) => await GetAsync<JsonElement>($"/api/Patterns/strategy/apply?strategyName={Uri.EscapeDataString(strategyName)}");
    }

    public class RequestLogService
    {
        public record LogEntry(string Url, string? Response, bool Success, DateTime Timestamp);
        private readonly List<LogEntry> _entries = new();
        public IReadOnlyList<LogEntry> Entries => _entries;
        public void LogRequest(string url) => _entries.Add(new LogEntry(url, null, true, DateTime.Now));
        public void LogResponse(string url, string? response, bool success) => _entries.Add(new LogEntry(url, response, success, DateTime.Now));
        public void Clear() => _entries.Clear();
    }
}
