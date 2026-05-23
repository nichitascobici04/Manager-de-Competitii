using Microsoft.AspNetCore.Mvc;

namespace Manager_de_Competitii.Interfaces.Api
{
    // Creational
    public interface IAbstractFactoryApi
    {
        Task<string> ConfigureTournamentAsync(string tournamentType);
    }

    public interface IFactoryMethodApi
    {
        Task<string> GenerateRoundsAsync(string roundsType);
    }

    public interface IBuilderApi
    {
        Task<string> BuildCompetitionAsync(string name, string sport, string type, string location, int organizerId = 0);
    }

    public interface ISimpleFactoryApi
    {
        Task<string> CreateParticipantAsync(string kind, string name);
    }

    // Structural
    public interface IFlyweightApi
    {
        IActionResult GetVenueInfoAsync(string venueKey);
    }

    public interface IProxyApi
    {
        IActionResult StartCompetitionAsync(int competitionId);
    }

    public interface IBridgeApi
    {
        IActionResult SendBridgeNotificationAsync(string channel, string message);
    }

    public interface IFacadeApi
    {
        
    }

    // Behavioral
    public interface IIteratorApi
    {
        IActionResult IterateMatchesAsync(int competitionId);
    }

    public interface IObserverApi
    {
        IActionResult SubscribeLiveMatchAsync(int matchId);
    }

    public interface ICommandApi
    {
        IActionResult ExecuteCommandAsync(string commandName);
    }

    public interface IStrategyApi
    {
        IActionResult ApplyStrategyAsync(string strategyName);
    }
}
