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
                    var fullUrl = BaseUrl.TrimEnd('/') + "/" + url.TrimStart('/');
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

        public Task<StatsDto?> GetStatsAsync() => GetAsync<StatsDto>("stats");

        // Creational
        public Task<string?> ConfigureTournamentAsync(string type) => GetAsync<string>($"tournaments/configure?tournamentType={Uri.EscapeDataString(type)}");
        public Task<string?> GenerateRoundsAsync(string type) => GetAsync<string>($"rounds/generate?roundsType={Uri.EscapeDataString(type)}");
        public Task<string?> BuildCompetitionAsync(string name, string sport, string type, string location, int organizerId = 0) 
            => GetAsync<string>($"builder/build?name={Uri.EscapeDataString(name)}&sport={Uri.EscapeDataString(sport)}&type={Uri.EscapeDataString(type)}&location={Uri.EscapeDataString(location)}&organizerId={organizerId}");
        public Task<string?> CreateParticipantAsync(string kind, string name) => GetAsync<string>($"participants/create?kind={Uri.EscapeDataString(kind)}&name={Uri.EscapeDataString(name)}");

        public Task<List<ParticipantDto>?> ListParticipantsAsync() => GetAsync<List<ParticipantDto>>("participants/list");
        public Task<string?> DeleteParticipantAsync(string name) => GetAsync<string>($"participants/delete?name={Uri.EscapeDataString(name)}");

        // Manager / Competition
        public Task<List<CompetitionDto>?> ListCompetitionsAsync() => GetAsync<List<CompetitionDto>>("competitions/list");
        public Task<string?> UpdateCompetitionAsync(int id, string sport, string type, string location) => GetAsync<string>($"competitions/update?id={id}&sport={Uri.EscapeDataString(sport)}&type={Uri.EscapeDataString(type)}&location={Uri.EscapeDataString(location)}");
        public Task<string?> AttachParticipantAsync(int compId, string pName) => GetAsync<string>($"competitions/participant?compId={compId}&participantName={Uri.EscapeDataString(pName)}");

        // Structural
        public Task<string?> GetVenueInfoAsync(string key) => GetAsync<string>($"venues/info?venueKey={Uri.EscapeDataString(key)}");
        public Task<List<LocationDto>?> ListLocationsAsync() => GetAsync<List<LocationDto>>("venues/list");
        public Task<string?> AddVenueAsync(string name) => GetAsync<string>($"venues/add?name={Uri.EscapeDataString(name)}");
        public Task<string?> DeleteVenueAsync(string name) => GetAsync<string>($"venues/delete?name={Uri.EscapeDataString(name)}");
        public Task<string?> EditVenueAsync(string oldName, string newName) => GetAsync<string>($"venues/edit?oldName={Uri.EscapeDataString(oldName)}&newName={Uri.EscapeDataString(newName)}");
        public Task<string?> StartCompetitionAsync(int id) => GetAsync<string>($"proxy/start?competitionId={id}");
        public Task<string?> SendNotificationAsync(string channel, string message) => GetAsync<string>($"notifications/send?channel={Uri.EscapeDataString(channel)}&message={Uri.EscapeDataString(message)}");
        public Task<string?> SendInviteAsync(string tournament, string channel) => GetAsync<string>($"notifications/send-invite?tournament={Uri.EscapeDataString(tournament)}&channel={Uri.EscapeDataString(channel)}");
        public Task<string?> SendResultAsync(int matchId, string channel) => GetAsync<string>($"notifications/send-result?matchId={matchId}&channel={Uri.EscapeDataString(channel)}");
        public Task<List<MatchDto>?> ListMatchesAsync() => GetAsync<List<MatchDto>>("matches/list");

        // Behavioral
        public Task<IteratedMatchesDto?> IterateMatchesAsync(int competitionId) => GetAsync<IteratedMatchesDto>($"matches/iterate?competitionId={competitionId}");
        public Task<string?> SubscribeLiveMatchAsync(int matchId) => GetAsync<string>($"matches/subscribe-live?matchId={matchId}");
        public Task<string?> ExecuteCommandAsync(string commandName) => GetAsync<string>($"commands/execute?commandName={Uri.EscapeDataString(commandName)}");
        public Task<string?> ApplyStrategyAsync(string strategyName) => GetAsync<string>($"strategies/apply?strategyName={Uri.EscapeDataString(strategyName)}");
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
