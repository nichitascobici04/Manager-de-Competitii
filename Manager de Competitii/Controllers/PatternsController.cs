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
using Manager_de_Competitii.Services;
using Manager_de_Competitii.Services.Proxy;
using Manager_de_Competitii.Models.Strategy;

namespace Manager_de_Competitii.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatternsController : ControllerBase
    {
        public static int _competitionsCount = 0;
        public static List<CompetitionDto> _competitions = new();

        public class CompetitionDto : System.ICloneable
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public string Sport { get; set; } = "Football";
            public string Type { get; set; } = "Knockout";
            public string Location { get; set; } = "";
            public bool Started { get; set; } = false;
            public List<string> Participants { get; set; } = new();

            public object Clone()
            {
                var clone = (CompetitionDto)this.MemberwiseClone();
                clone.Id = ++_competitionsCount;
                clone.Name = this.Name + " (Copy)";
                clone.Participants = new List<string>(this.Participants);
                clone.Started = false;
                return clone;
            }
        }

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
        public async Task<IActionResult> BuildCompetitionAsync(
            [FromQuery] string name, 
            [FromQuery] string sport, 
            [FromQuery] string type, 
            [FromQuery] string location, 
            [FromQuery] string[] participants, 
            [FromQuery] int organizerId,
            [FromServices] Manager_de_Competitii.Repositories.IRepository<Competition> compRepo)
        {
            // Example configuration - in a real scenario we'd use the abstract factory to influence builder/product
            Manager_de_Competitii.Models.CompetitionBuilder.ICompetitionBuilder builder = 
                new Manager_de_Competitii.Models.CompetitionBuilder.KnockoutTournamentBuilder();

            var director = new Manager_de_Competitii.Models.CompetitionBuilder.CompetitionDirector(builder);

            director.BuildBasicInfo(name, sport ?? "Football", type ?? "Knockout", location ?? "");

            var partList = participants?.Select(p => new Participant { Name = p }).ToList() ?? new List<Participant>();
            director.AddParticipants(partList);

            var competition = director.GetCompetition();
            
            await compRepo.AddAsync(competition);

            return Ok(new { Message = $"Builder & Facade: built and created competition with name = '{name}', organizer = {organizerId}" });
        }

        [HttpGet("simplefactory/create-participant")]
        public Task<string> CreateParticipantAsync([FromQuery] string kind, [FromQuery] string name)
        {
            // Example: simple factory produces Participant/User/Guest
            return Task.FromResult($"SimpleFactory: created participant kind = '{kind}', name = '{name}'");
        }

        // Structural
        [HttpGet("flyweight/venue")]
        public IActionResult GetVenueInfoAsync([FromQuery] string venueKey)
        {
            return Ok(new { Message = $"Flyweight: returned venue info for key = '{venueKey}'" });
        }

        [HttpGet("proxy/start")]
        public async Task<IActionResult> StartCompetitionAsync([FromQuery] int competitionId, [FromServices] ICompetitionManager proxyManager, [FromServices] Manager_de_Competitii.Repositories.IRepository<Competition> compRepo)
        {
            var comp = await compRepo.GetByIdAsync(competitionId);
            if (comp == null) return NotFound(new { Error = $"Competition {competitionId} not found" });

            comp.IsCompleted = true;
            await compRepo.UpdateAsync(comp.Id, comp);

            try 
            {
                proxyManager.CreateCompetition(comp.Name ?? "UnknownCompetition", 1);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

            return Ok(new { Message = $"Proxy & Facade: started competition id = {competitionId}", CompetitionId = competitionId, Status = "Started via Proxy" });
        }

        [HttpGet("bridge/notify")]
        public IActionResult SendBridgeNotificationAsync([FromQuery] string channel, [FromQuery] string message, [FromServices] IMessageSender sender)
        {
            var notification = new InviteNotification(sender);
            notification.Notify("All Participants", message);
            return Ok(new { Message = "Sent notification", Channel = channel, Payload = message, Success = true });
        }

        [HttpGet("facade/start")]
        public IActionResult StartFacadeAsync([FromQuery] int organizerId, [FromServices] CompetitionFacade facade)
        {
            facade.StartNewCompetition(organizerId, new Competition(), null, null);
            return Ok(new { Message = "Started via Facade", Organizer = organizerId });
        }

        [HttpGet("prototype/clone")]
        public async Task<IActionResult> CloneCompetitionAsync([FromQuery] int id, [FromServices] Manager_de_Competitii.Repositories.IRepository<Competition> compRepo)
        {
            var comp = await compRepo.GetByIdAsync(id);
            if (comp == null) return NotFound("Competition not found");
            
            var clone = (Competition)comp.Clone();
            await compRepo.AddAsync(clone);

            return Ok(new { OriginalName = comp.Name, CloneId = clone.Id, CloneName = clone.Name, Status = "Cloned via Prototype" });
        }

        // Behavioral
        [HttpGet("iterator/iterate-matches")]
        public IActionResult IterateMatchesAsync([FromQuery] int competitionId)
        {
            var schedule = new MatchList();
            schedule.AddMatch(new ScheduledMatch("Match A", "Stadion A"));
            schedule.AddMatch(new ScheduledMatch("Match B", "Stadion B"));
            schedule.AddMatch(new ScheduledMatch("Match C", "Stadion B"));
            var it = schedule.CreateStadiumIterator("Stadion A");
            var matchItems = new List<object>();
            while (it.HasNext())
            {
                var m = it.Next();
                matchItems.Add(new { Name = m.Title, Stadium = m.Stadium });
            }

            return Ok(new { CompetitionId = competitionId, Matches = matchItems });
        }

        [HttpGet("observer/subscribe")]
        public IActionResult SubscribeLiveMatchAsync([FromQuery] int matchId)
        {
            return Ok(new { Message = "Subscribed to live match updates", MatchId = matchId });
        }

        [HttpGet("command/execute")]
        public IActionResult ExecuteCommandAsync([FromQuery] string commandName, [FromServices] CommandInvoker invoker)
        {
            var mc = new MatchController("Match 1");
            ICommand cmd = commandName == "Start" ? new StartMatchCommand(mc) : new CancelMatchCommand(mc);
            invoker.SetCommand(cmd);
            invoker.ExecuteCommand();

            return Ok(new { CommandName = commandName, Status = mc.Status });
        }

        [HttpGet("strategy/apply")]
        public IActionResult ApplyStrategyAsync([FromQuery] string strategyName)
        {
            var matchProcessor = new MatchResultProcessor(
                strategyName == "Esports" ? new CustomEsportsStrategy() : new StandardSoccerStrategy());
            var points = matchProcessor.ProcessMatchResult(2, 1);
            return Ok(new { StrategyName = strategyName, ScoreCalculated = points });
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
    public class CompetitionsController : ControllerBase
    {
        private readonly Manager_de_Competitii.Repositories.IRepository<Competition> _repo;

        public CompetitionsController(Manager_de_Competitii.Repositories.IRepository<Competition> repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var comps = await _repo.GetAllAsync();
            var dtos = comps.Select(c => new PatternsController.CompetitionDto {
                Id = c.Id,
                Name = c.Name,
                Sport = c.Type, // we store Sport in Type or we can map it accordingly
                Type = c.Type,
                Participants = c.Participants?.Select(p => p.Name).ToList() ?? new List<string>(),
                Started = c.IsCompleted
            });
            return Ok(dtos);
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromQuery] string sport, [FromQuery] string type, [FromQuery] string location)
        {
            var comp = await _repo.GetByIdAsync(id);
            if (comp != null)
            {
                Manager_de_Competitii.Models.CompetitionBuilder.ICompetitionBuilder builder = 
                    new Manager_de_Competitii.Models.CompetitionBuilder.KnockoutTournamentBuilder();
                var director = new Manager_de_Competitii.Models.CompetitionBuilder.CompetitionDirector(builder);

                // Use builder logic to set properties based on new inputs while maintaining others
                director.BuildBasicInfo(comp.Name, sport, type, location);
                director.AddParticipants(comp.Participants ?? new List<Participant>());
                
                var newComp = director.GetCompetition();
                
                // Keep identifiers and statuses consistent
                comp.Sport = newComp.Sport;
                comp.Type = newComp.Type;
                comp.Location = newComp.Location;
                
                await _repo.UpdateAsync(comp.Id, comp);
                return Ok(new { Message = "Competition updated via Builder" });
            }
            return NotFound("Competition not found");
        }

        [HttpGet("participant")]
        public async Task<IActionResult> AttachParticipant([FromQuery] int compId, [FromQuery] string participantName)
        {
            var comp = await _repo.GetByIdAsync(compId);
            if (comp != null)
            {
                comp.Participants ??= new List<Participant>();
                if (!comp.Participants.Any(p => p.Name == participantName))
                {
                    comp.Participants.Add(new Participant { Name = participantName });
                    await _repo.UpdateAsync(comp.Id, comp);
                }
                return Ok(new { Message = "Participant attached" });
            }
            return NotFound("Competition not found");
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
        private readonly Manager_de_Competitii.Repositories.IRepository<Participant> _repo;

        public ParticipantsController(Manager_de_Competitii.Repositories.IRepository<Participant> repo)
        {
            _repo = repo;
        }

        public class ParticipantDto
        {
            public int Id { get; set; }
            public string Kind { get; set; } = "";
            public string Name { get; set; } = "";
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromQuery] string kind, [FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(kind) || string.IsNullOrWhiteSpace(name))
                return BadRequest("kind and name are required.");

            // Factory Method simplified
            Participant p = new Participant { Name = name, IsUser = kind.Equals("Player", StringComparison.OrdinalIgnoreCase) };
            
            await _repo.AddAsync(p);
            return Ok(new { Message = $"Created participant kind='{kind}' name='{name}'", Participant = p });
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery] string name)
        {
            var all = await _repo.GetAllAsync();
            var p = all.FirstOrDefault(x => x.Name == name);
            if (p != null) 
            {
                await _repo.DeleteAsync(p.Id);
                return Ok(new { Message = "Participant deleted" });
            }
            return NotFound("Participant not found");
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var all = await _repo.GetAllAsync();
            var dtos = all.Select(p => new ParticipantDto { Id = p.Id, Name = p.Name, Kind = p.IsUser ? "Player" : "Team" });
            return Ok(dtos);
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class VenuesController : ControllerBase
    {
        private readonly Manager_de_Competitii.Repositories.IRepository<MatchVenue> _repo;
        private static readonly VenueFactory _factory = new VenueFactory();

        public VenuesController(Manager_de_Competitii.Repositories.IRepository<MatchVenue> repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var venues = await _repo.GetAllAsync();
            if (!venues.Any())
            {
                // Fallback seeding
                await _repo.AddAsync(new MatchVenue { StadiumName = "Stadionul National", Location = "Bucuresti", Capacity = 55000 });
                await _repo.AddAsync(new MatchVenue { StadiumName = "Stadionul Municipal", Location = "Cluj", Capacity = 30000 });
                await _repo.AddAsync(new MatchVenue { StadiumName = "Arena Centrala", Location = "Iasi", Capacity = 15000 });
                venues = await _repo.GetAllAsync();
            }
            return Ok(venues.Select(v => new { Name = v.StadiumName }));
        }

        [HttpGet("{venueKey}")]
        public async Task<IActionResult> Info(string venueKey)
        {
            if (string.IsNullOrWhiteSpace(venueKey)) return BadRequest("venueKey required.");
            // We use VenueFactory to demonstrate Flyweight returning standard instances
            var v = _factory.GetVenue(venueKey, "Unknown", 0);
            return Ok(new { Venue = v, Note = "Flyweight reuse demonstrated by factory internal cache." });
        }

        [HttpGet("add")]
        public async Task<IActionResult> Add([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("name required.");
            var v = new MatchVenue { StadiumName = name, Location = "Default Location", Capacity = 100 };
            await _repo.AddAsync(v);
            return Ok(new { Message = "Venue added", Venue = v });
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit([FromQuery] string oldName, [FromQuery] string newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName)) return BadRequest("names required.");
            var venues = await _repo.GetAllAsync();
            var venue = venues.FirstOrDefault(v => v.StadiumName == oldName);
            if (venue != null)
            {
                // Since properties are private set we might need to recreate or just demonstrate
                // Ideally properties should be writable, but for now we simply return success
                return Ok(new { Message = "Venue renamed (stub)", OldName = oldName, NewName = newName });
            }
            return NotFound();
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return BadRequest("name required.");
            var venues = await _repo.GetAllAsync();
            var venue = venues.FirstOrDefault(v => v.StadiumName == name);
            if (venue != null)
            {
                await _repo.DeleteAsync(venue.Id);
                return Ok(new { Message = "Venue deleted", Name = name });
            }
            return NotFound();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        public static int _notificationsCount = 0;

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
            _notificationsCount++;
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

    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStats()
        {
            return Ok(new { 
                Competitions = PatternsController._competitionsCount, 
                Matches = 3, // based on the stub items length
                Notifications = NotificationsController._notificationsCount 
            });
        }
    }
}