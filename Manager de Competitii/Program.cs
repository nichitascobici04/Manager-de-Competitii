using Manager_de_Competitii.Models;
using Manager_de_Competitii.Models.AggregateScoreCalculator;
using Manager_de_Competitii.Models.Flyweight;
using Manager_de_Competitii.Models.Decorator;
using Manager_de_Competitii.Models.Bridge;
using Manager_de_Competitii.Services.Proxy;
using Manager_de_Competitii.Models.Strategy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Testare Aggregate Score Calculator
Console.WriteLine("\n=== Aggregate Score Calculator Test ===");
ScoreComponent match1 = new MatchScore("Group Stage Match 1", 3);
ScoreComponent match2 = new MatchScore("Group Stage Match 2", 1);
ScoreComponent match3 = new MatchScore("Group Stage Match 3", 3);

AggregateScore groupStage = new AggregateScore("Group Stage Total");
groupStage.Add(match1);
groupStage.Add(match2);
groupStage.Add(match3);

ScoreComponent semiFinal = new MatchScore("Semi-Final Match", 3);
ScoreComponent final = new MatchScore("Final Match", 0);

AggregateScore knockoutStage = new AggregateScore("Knockout Stage Total");
knockoutStage.Add(semiFinal);
knockoutStage.Add(final);

AggregateScore seasonTotal = new AggregateScore("Season Total Score for Team A");
seasonTotal.Add(groupStage);
seasonTotal.Add(knockoutStage);

seasonTotal.Display(0);
Console.WriteLine();
Console.WriteLine("Final Aggregate Score: " + seasonTotal.GetScore() + " puncte");
Console.WriteLine("=======================================\n");
#endregion

#region Testare Adapter Pattern
Console.WriteLine("\n=== Adapter Pattern Test ===");
// Adaptee
Participant myParticipant = new Participant { Id = 1, Name = "Team Alpha", IsBye = false, TournamentId = 100 };

// Target via Adapter
Manager_de_Competitii.Interfaces.ICompetitor adaptedCompetitor = new Manager_de_Competitii.Models.CompetitorAdapter(myParticipant);

Console.WriteLine($"[External System] Participant Name via Adapter: {adaptedCompetitor.GetCompetitorName()}");
Console.WriteLine($"[External System] Participant Status via Adapter: {adaptedCompetitor.GetCompetitorStatus()}");
Console.WriteLine("============================\n");
#endregion

#region Testare Flyweight Pattern
Console.WriteLine("\n=== Flyweight Pattern Test ===");
VenueFactory venueFactory = new VenueFactory();
MatchVenue venue1 = venueFactory.GetVenue("Stadionul National", "Bucuresti", 55000);
MatchVenue venue2 = venueFactory.GetVenue("Stadionul National", "Bucuresti", 55000);

MatchLocationContext matchCtx1 = new MatchLocationContext("Finala Cupei", DateTime.Now.AddDays(10), venue1);
MatchLocationContext matchCtx2 = new MatchLocationContext("Amical", DateTime.Now.AddDays(5), venue2);

matchCtx1.Display();
matchCtx2.Display();
Console.WriteLine($"Are venues identically reference-equal? {Object.ReferenceEquals(venue1, venue2)}");
Console.WriteLine("============================\n");
#endregion

#region Testare Decorator Pattern
Console.WriteLine("\n=== Decorator Pattern Test ===");
IScoreCalculator calc = new BaseScoreCalculator();
Console.WriteLine($"Base score for 10 points: {calc.CalculateScore(10)}");

calc = new FairPlayBonusDecorator(calc, 2.5); // Add 2.5 bonus
Console.WriteLine($"After FairPlay bonus: {calc.CalculateScore(10)}");

calc = new HomeAdvantageDecorator(calc, 1.2); // Multiply by 1.2
Console.WriteLine($"After Home Advantage: {calc.CalculateScore(10)}");
Console.WriteLine("============================\n");
#endregion

#region Testare Bridge Pattern
Console.WriteLine("\n=== Bridge Pattern Test ===");
IMessageSender emailSender = new EmailSender();
IMessageSender smsSender = new SmsSender();

Manager_de_Competitii.Models.Bridge.Notification resultEmail = new MatchResultNotification(emailSender);
Manager_de_Competitii.Models.Bridge.Notification inviteSms = new InviteNotification(smsSender);

resultEmail.Notify("Derby", "Team A won 2-1 against Team B.");
inviteSms.Notify("Team Alpha", "Join our tournament this weekend!");
Console.WriteLine("============================\n");
#endregion

#region Testare Proxy Pattern
Console.WriteLine("\n=== Proxy Pattern Test ===");
ICompetitionManager unauthorizedManager = new CompetitionManagerProxy("Spectator");
unauthorizedManager.CreateCompetition("Spectator League", 1);

ICompetitionManager authorizedManager = new CompetitionManagerProxy("Organizer");
authorizedManager.CreateCompetition("Official League", 2);
Console.WriteLine("============================\n");
#endregion

#region Testare Strategy Pattern
Console.WriteLine("\n=== Strategy Pattern Test ===");
MatchResultProcessor matchProcessor = new MatchResultProcessor(new StandardSoccerStrategy());
Console.WriteLine($"Soccer points (Win 2-1): {matchProcessor.ProcessMatchResult(2, 1)} pt(s)");
Console.WriteLine($"Soccer points (Draw 1-1): {matchProcessor.ProcessMatchResult(1, 1)} pt(s)");

matchProcessor.SetStrategy(new CustomEsportsStrategy());
Console.WriteLine($"Esports points (Loss 0-2): {matchProcessor.ProcessMatchResult(0, 2)} pt(s)");
Console.WriteLine("============================\n");
#endregion

app.Run();
