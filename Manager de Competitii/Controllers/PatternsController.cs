using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manager_de_Competitii.Interfaces.Api;

namespace Manager_de_Competitii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatternsController : ControllerBase,
        IAbstractFactoryApi,
        IFactoryMethodApi,
        IBuilderApi,
        ISimpleFactoryApi,
        IFlyweightApi,
        IProxyApi,
        IBridgeApi,
        IFacadeApi,
        IIteratorApi,
        IObserverApi,
        ICommandApi,
        IStrategyApi
    {
        // NOTE: These are stub endpoints. Replace with injected services (constructor DI) to call real implementations.

        // Creational
        [HttpGet("abstractfactory/configure")]
        public Task<string> ConfigureTournamentAsync([FromQuery] string tournamentType)
        {
            // Example: create ITournamentFactory by type and run TournamentConfig
            return Task.FromResult($"AbstractFactory: configured tournament type = '{tournamentType}'");
        }

        [HttpGet("factorymethod/generate")]
        public Task<string> GenerateRoundsAsync([FromQuery] string roundsType)
        {
            // Example: use RoundsGenerator -> CompetitionRoundsService
            return Task.FromResult($"FactoryMethod: generated rounds for type = '{roundsType}'");
        }

        [HttpGet("builder/build")]
        public Task<string> BuildCompetitionAsync([FromQuery] string name)
        {
            // Example: CompetitionDirector + ICompetitionBuilder
            return Task.FromResult($"Builder: built competition with name = '{name}'");
        }

        [HttpGet("simplefactory/create-participant")]
        public Task<string> CreateParticipantAsync([FromQuery] string kind, [FromQuery] string name)
        {
            // Example: simple factory produces Participant/User/Guest
            return Task.FromResult($"SimpleFactory: created participant kind = '{kind}', name = '{name}'");
        }

        // Structural
        [HttpGet("flyweight/venue")]
        public Task<string> GetVenueInfoAsync([FromQuery] string venueKey)
        {
            // Example: VenueFactory -> reuse venue flyweights
            return Task.FromResult($"Flyweight: returned venue info for key = '{venueKey}'");
        }

        [HttpGet("proxy/start")]
        public Task<string> StartCompetitionViaProxyAsync([FromQuery] int competitionId)
        {
            // Example: CompetitionManagerProxy -> access control / lazy load
            return Task.FromResult($"Proxy: started competition via proxy id = {competitionId}");
        }

        [HttpGet("bridge/notify")]
        public Task<string> SendBridgeNotificationAsync([FromQuery] string channel, [FromQuery] string message)
        {
            // Example: InviteNotification abstraction + concrete implementors
            return Task.FromResult($"Bridge: sent notification via '{channel}' message = '{message}'");
        }

        [HttpGet("facade/start")]
        public Task<string> StartCompetitionFacadeAsync([FromQuery] int organizerId)
        {
            // Example: CompetitionFacade orchestrates multiple subsystems
            return Task.FromResult($"Facade: started competition for organizer id = {organizerId}");
        }

        // Behavioral
        [HttpGet("iterator/iterate-matches")]
        public Task<string> IterateMatchesAsync([FromQuery] int competitionId)
        {
            // Example: MatchList + StadiumMatchIterator iteration
            return Task.FromResult($"Iterator: iterated matches for competition id = {competitionId}");
        }

        [HttpGet("observer/subscribe")]
        public Task<string> SubscribeLiveMatchAsync([FromQuery] int matchId)
        {
            // Example: LiveMatch subject and observers
            return Task.FromResult($"Observer: subscribed to live match id = {matchId}");
        }

        [HttpGet("command/execute")]
        public Task<string> ExecuteCommandAsync([FromQuery] string commandName)
        {
            // Example: ICommand + CommandInvoker pattern invocation
            return Task.FromResult($"Command: executed command '{commandName}'");
        }

        [HttpGet("strategy/apply")]
        public Task<string> ApplyStrategyAsync([FromQuery] string strategyName)
        {
            // Example: IFormat/IStageGenerator/IStrategy implementations swapped
            return Task.FromResult($"Strategy: applied strategy '{strategyName}'");
        }
    }
}