using MySqlConnector;
using Nl.Stats.Api.Database;
using Nl.Stats.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>(_ => new ConnectionFactory(
    builder.Configuration.GetConnectionString("Cod2ZomMysql")!
));

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/get-stats", async (
    IConnectionFactory connectionFactory,
    [AsParameters] GetStatsRequest getStatsRequest
) =>
    {
        await using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync();

        using var command = new MySqlCommand("SELECT login, rank, kills FROM cod2_zom_players_view WHERE login = @login", connection);
        command.Parameters.AddWithValue("@login", getStatsRequest.Login);
        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return Results.NotFound();
        }

        var login = reader.GetString(0);
        var rank = reader.GetInt32(1);
        var kills = reader.GetInt32(2);

        var response = new GetStatsResponse(login, rank, kills);
        return Results.Ok(response);
    }
).WithOpenApi();

app.Run();
