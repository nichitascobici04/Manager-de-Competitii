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
        Task<string> BuildCompetitionAsync(string name, int organizerId = 0);
    }

    public interface ISimpleFactoryApi
    {
        Task<string> CreateParticipantAsync(string kind, string name);
    }

    // Structural
    public interface IFlyweightApi
    {
        Task<string> GetVenueInfoAsync(string venueKey);
    }

    public interface IProxyApi
    {
        Task<string> StartCompetitionAsync(int competitionId);
    }

    public interface IBridgeApi
    {
        Task<string> SendBridgeNotificationAsync(string channel, string message);
    }

    public interface IFacadeApi
    {
        
    }

    // Behavioral
    public interface IIteratorApi
    {
        Task<string> IterateMatchesAsync(int competitionId);
    }

    public interface IObserverApi
    {
        Task<string> SubscribeLiveMatchAsync(int matchId);
    }

    public interface ICommandApi
    {
        Task<string> ExecuteCommandAsync(string commandName);
    }

    public interface IStrategyApi
    {
        Task<string> ApplyStrategyAsync(string strategyName);
    }
}
