using Manager_de_Competitii.Models.AggregateScoreCalculator;

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

app.Run();
