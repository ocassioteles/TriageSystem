using System.Text.Json;
using TypedGuardrail.Domain.Records;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapPost("/triage", async (HttpRequest request) =>
{
    if (request.ContentType is not null && !request.HasJsonContentType())
    {
        return Results.StatusCode(StatusCodes.Status415UnsupportedMediaType);
    }

    IncomingPayload? payload;
    try
    {
        payload = await request.ReadFromJsonAsync<IncomingPayload>();
    }
    catch (JsonException)
    {
        return Results.BadRequest(new { error = "Request body must contain valid JSON." });
    }
    catch (BadHttpRequestException)
    {
        return Results.BadRequest(new { error = "Request body must contain valid JSON." });
    }

    if (payload is null)
    {
        return Results.BadRequest(new { error = "Request payload is required." });
    }

    var errors = new Dictionary<string, string[]>();

    if (string.IsNullOrWhiteSpace(payload.Title))
    {
        errors[nameof(payload.Title)] = ["Title is required."];
    }

    if (string.IsNullOrWhiteSpace(payload.Description))
    {
        errors[nameof(payload.Description)] = ["Description is required."];
    }

    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    return Results.StatusCode(StatusCodes.Status501NotImplemented);
})
.WithName("PostTriage")
.Accepts<IncomingPayload>("application/json")
.WithOpenApi();

app.Run();
