using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

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
                        return JsonSerializer.Deserialize<T>(content);
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

        // Creational
        public Task<string?> ConfigureTournamentAsync(string type) => GetAsync<string>($"tournaments/configure?tournamentType={Uri.EscapeDataString(type)}");
        public Task<string?> GenerateRoundsAsync(string type) => GetAsync<string>($"rounds/generate?roundsType={Uri.EscapeDataString(type)}");
        public Task<string?> BuildCompetitionAsync(string name) => GetAsync<string>($"competitions/build?name={Uri.EscapeDataString(name)}");
        public Task<string?> CreateParticipantAsync(string kind, string name) => GetAsync<string>($"participants/create?kind={Uri.EscapeDataString(kind)}&name={Uri.EscapeDataString(name)}");

        // Structural
        public Task<string?> GetVenueInfoAsync(string key) => GetAsync<string>($"venues/info?venueKey={Uri.EscapeDataString(key)}");
        public Task<List<string>?> ListVenuesAsync() => GetAsync<List<string>>("venues/list");
        public Task<string?> StartCompetitionViaProxyAsync(int id) => GetAsync<string>($"competitions/start-via-proxy?competitionId={id}");
        public Task<string?> GetCompetitionStatusViaProxyAsync(int id) => GetAsync<string>($"competitions/status-via-proxy?competitionId={id}");
        public Task<string?> SendNotificationAsync(string channel, string message) => GetAsync<string>($"notifications/send?channel={Uri.EscapeDataString(channel)}&message={Uri.EscapeDataString(message)}");
        public Task<string?> SendInviteAsync(string tournament, string channel) => GetAsync<string>($"notifications/send-invite?tournament={Uri.EscapeDataString(tournament)}&channel={Uri.EscapeDataString(channel)}");
        public Task<string?> SendResultAsync(int matchId, string channel) => GetAsync<string>($"notifications/send-result?matchId={matchId}&channel={Uri.EscapeDataString(channel)}");
        public Task<string?> CreateCompetitionViaFacadeAsync(string name, int organizerId) => GetAsync<string>($"competitions/create-via-facade?name={Uri.EscapeDataString(name)}&organizerId={organizerId}");
        public Task<string?> StartCompetitionViaFacadeAsync(int organizerId) => GetAsync<string>($"competitions/start-via-facade?organizerId={organizerId}");

        // Behavioral
        public Task<string?> IterateMatchesAsync(int competitionId) => GetAsync<string>($"matches/iterate?competitionId={competitionId}");
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
