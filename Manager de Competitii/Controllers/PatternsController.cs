using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Manager_de_Competitii.Interfaces.Api;
using Manager_de_Competitii.Models;
using Manager_de_Competitii.Models.AbstractFactory;
using Manager_de_Competitii.Models.FactoryMethod;
using Manager_de_Competitii.Models.Flyweight;
using Manager_de_Competitii.Models.Bridge;
using Manager_de_Competitii.Models.Iterator;
using System.Collections.Concurrent;
using Manager_de_Competitii.Models.Observer;
using Manager_de_Competitii.Models.AggregateScoreCalculator;
using Manager_de_Competitii.Models.AppConfig;
using Manager_de_Competitii.Models.Decorator;
using Manager_de_Competitii.Models.Command;

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
        public Task<string> BuildCompetitionAsync([FromQuery] string name, [FromQuery] int organizerId = 0)
        {
            // Example: CompetitionDirector + ICompetitionBuilder inside a Facade
            return Task.FromResult($"Builder & Facade: built and created competition with name = '{name}', organizer = {organizerId}");
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
        public Task<string> StartCompetitionAsync([FromQuery] int competitionId)
        {
            // Example: CompetitionManagerProxy -> access control / lazy load orchestrating via Facade
            return Task.FromResult($"Proxy & Facade: started competition id = {competitionId}");
        }

        [HttpGet("bridge/notify")]
        public Task<string> SendBridgeNotificationAsync([FromQuery] string channel, [FromQuery] string message)
        {
            // Example: InviteNotification abstraction + concrete implementors
            return Task.FromResult($"Bridge: sent notification via '{channel}' message = '{message}'");
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

        // Tournament configuration endpoints
        [HttpGet("configure")]
        public IActionResult Configure([FromQuery] string tournamentType)
        {
            if (string.IsNullOrWhiteSpace(tournamentType))
                return BadRequest("tournamentType is required.");

            ITournamentFactory factory = tournamentType.Equals("knockout", StringComparison.OrdinalIgnoreCase)
                ? new KnockoutTournamentFactory()
                : new RoundRobinTournamentFactory();

            var config = new TournamentConfig(factory.CreateFormat(), factory.CreateMatchType());
            return Ok(new { Message = $"Configured tournament type = '{tournamentType}'", Format = config.Format.GetType().Name });
        }


    }

    [ApiController]
    [Route("api/[controller]")]
    public class RoundsController : ControllerBase
    {
        [HttpGet("generate")]
        public IActionResult Generate([FromQuery] string roundsType)
        {
            if (string.IsNullOrWhiteSpace(roundsType)) return BadRequest("roundsType required.");

            var generator = new RoundsGenerator();
            try
            {
                var service = generator.GetService(roundsType.ToLowerInvariant());
                service.Competition();
                return Ok(new { Message = $"Generated rounds for type = '{roundsType}'" });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] CreateParticipantRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Kind) || string.IsNullOrWhiteSpace(req.Name))
                return BadRequest("kind and name are required.");

            Participant p = new Participant { Name = req.Name };
            // In a real app map Kind to RegisteredUser/Guest via factory
            return Ok(new { Message = $"Created participant kind='{req.Kind}' name='{req.Name}'", Participant = p });
        }

        public record CreateParticipantRequest(string Kind, string Name);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class VenuesController : ControllerBase
    {
        private static readonly VenueFactory _factory = new VenueFactory();

        [HttpGet("list")]
        public IActionResult List()
        {
            var v1 = _factory.GetVenue("Stadionul National", "Bucuresti", 55000);
            var v2 = _factory.GetVenue("Stadionul Municipal", "Cluj", 30000);
            var v3 = _factory.GetVenue("Arena Centrala", "Iasi", 15000);
            return Ok(new[] { v1, v2, v3 });
        }

        [HttpGet("{venueKey}")]
        public IActionResult Info(string venueKey)
        {
            if (string.IsNullOrWhiteSpace(venueKey)) return BadRequest("venueKey required.");

            // For demo reuse by name
            var v = _factory.GetVenue(venueKey, "Unknown", 0);
            return Ok(new { Venue = v, Note = "Flyweight reuse demonstrated by factory internal cache." });
        }

        [HttpGet("add")]
        public IActionResult Add([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("name required.");
            var v = _factory.GetVenue(name, "Default Location", 100);
            return Ok(new { Message = "Venue added", Venue = v });
        }

        [HttpGet("edit")]
        public IActionResult Edit([FromQuery] string oldName, [FromQuery] string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName)) return BadRequest("names required.");
            // Logic to rename would go here, for now just return success
            return Ok(new { Message = "Venue renamed (stub)", OldName = oldName, NewName = newName });
        }

        [HttpGet("delete")]
        public IActionResult Delete([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("name required.");
            // Logic to delete would go here, for now just return success
            return Ok(new { Message = "Venue deleted (stub)", Name = name });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult Send([FromBody] NotificationRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Channel) || string.IsNullOrWhiteSpace(req.Message))
                return BadRequest("channel and message required.");

            IMessageSender sender = req.Channel.Equals("sms", System.StringComparison.OrdinalIgnoreCase)
                ? new SmsSender()
                : (IMessageSender)new EmailSender();

            var notification = new InviteNotification(sender);
            notification.Notify(req.Target ?? "All", req.Message);
            return Ok(new { Message = $"Sent via {req.Channel}", Channel = req.Channel, Payload = req.Message });
        }

        public record NotificationRequest(string Channel, string Message, string? Target);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        [HttpGet("list")]
        public IActionResult List()
        {
            return Ok(new[] { 
                new { Id = 1, Name = "Final Match: Team A vs Team B", CompetitionId = 1, CompetitionName = "Champions Cup" },
                new { Id = 2, Name = "Semi-final: Team C vs Team D", CompetitionId = 1, CompetitionName = "Champions Cup" },
                new { Id = 3, Name = "Friendly Match: Team E vs Team F", CompetitionId = 2, CompetitionName = "Summer Friendly" }
            });
        }

        [HttpGet("iterate")]
        public IActionResult Iterate([FromQuery] int competitionId, [FromQuery] string? stadium)
        {
            var schedule = new MatchList();
            schedule.AddMatch(new ScheduledMatch("Match 1", "Stadion A"));
            schedule.AddMatch(new ScheduledMatch("Match 2", "Stadion B"));
            schedule.AddMatch(new ScheduledMatch("Match 3", "Stadion A"));

            if (!string.IsNullOrWhiteSpace(stadium))
            {
                var it = schedule.CreateStadiumIterator(stadium);
                var items = new List<ScheduledMatch>();
                while (it.HasNext())
                {
                    items.Add(it.Next());
                }
                return Ok(new { CompetitionId = competitionId, Stadium = stadium, Matches = items });
            }

            return Ok(new { CompetitionId = competitionId, Matches = schedule.GetItems() });
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class LiveMatchController : ControllerBase
    {
        private static readonly ConcurrentDictionary<int, LiveMatch> _matches = new();

        [HttpPost("subscribe")]
        public IActionResult Subscribe([FromBody] SubscribeRequest req)
        {
            if (req == null) return BadRequest("body required.");

            var lm = _matches.GetOrAdd(req.MatchId, id => new LiveMatch($"Team {id}A", $"Team {id}B"));
            // For stub: do not add observers across HTTP boundary; return current state
            return Ok(new { Message = "Subscribed (stub)", MatchId = req.MatchId, ScoreA = lm.ScoreA, ScoreB = lm.ScoreB });
        }

        [HttpPost("score")]
        public IActionResult Score([FromBody] ScoreRequest req)
        {
            if (req == null) return BadRequest("body required.");
            if (!_matches.TryGetValue(req.MatchId, out var lm)) return NotFound("Match not found. Subscribe first.");

            lm.ScoreGoal(req.TeamName);
            return Ok(new { Message = "Goal scored", MatchId = req.MatchId, ScoreA = lm.ScoreA, ScoreB = lm.ScoreB, LastEvent = lm.LastEvent });
        }

        public record SubscribeRequest(int MatchId);
        public record ScoreRequest(int MatchId, string TeamName);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CommandsController : ControllerBase
    {
        [HttpPost("execute")]
        public IActionResult Execute([FromBody] CommandRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.CommandName)) return BadRequest("commandName required.");

            var matchController = new MatchController(req.MatchId ?? "default");
            ICommand cmd = req.CommandName.Equals("StartMatch", System.StringComparison.OrdinalIgnoreCase)
                ? new StartMatchCommand(matchController)
                : (ICommand)new CancelMatchCommand(matchController);

            var invoker = new CommandInvoker();
            invoker.SetCommand(cmd);
            invoker.ExecuteCommand();

            return Ok(new { Message = $"Executed {req.CommandName}", MatchId = matchController.MatchId, Status = matchController.Status });
        }

        public record CommandRequest(string CommandName, string? MatchId);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        [HttpPost("aggregate")]
        public IActionResult Aggregate([FromBody] AggregateRequest req)
        {
            var seasonTotal = new AggregateScore("Season Total Score");
            if (req?.MatchScores != null)
            {
                foreach (var s in req.MatchScores)
                {
                    seasonTotal.Add(new MatchScore(s.MatchName, s.Points));
                }
            }
            return Ok(new { Total = seasonTotal.GetScore() });
        }

        public record AggregateRequest(MatchScoreDto[]? MatchScores);
        public record MatchScoreDto(string MatchName, int Points);
    }

    [ApiController]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private static AppConfig _config = new AppConfig { DefaultTournamentType = "roundrobin" };

        [HttpGet]
        public IActionResult Get() => Ok(_config);

        [HttpPut]
        public IActionResult Update([FromBody] AppConfig cfg)
        {
            if (cfg == null) return BadRequest();
            _config = cfg;
            return Ok(_config);
        }
    }
}