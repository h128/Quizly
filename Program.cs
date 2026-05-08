using Quizly.Domain;
using Quizly.EndPoints;
using Quizly.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddQuizlyDatabase(builder.Configuration);
builder.Services.AddQuizlyIdentity();

builder.Services.AddAuthorization();
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.MapIdentityApi<QuizlyUser>();
app.MapFolderEndpoints();

app.Run();