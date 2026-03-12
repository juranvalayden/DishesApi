using DishApi.Application;
using DishesApi.Extensions;
using DishesApi.Infrastructure;
using System.Net;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddAuthorization();

// Add custom policy
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdminFromBelgium", policy =>
        policy.RequireRole("admin")
              .RequireClaim("country", "Belgium"));


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Dishes Api")
            .WithTheme(ScalarTheme.Mars)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

if (!app.Environment.IsDevelopment())
{
    // Exception handling middleware
    app.UseExceptionHandler(applicationBuilder => applicationBuilder.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"error\":\"An unexpected problem occurred.\"}");
    }));
}

app.UseHttpsRedirection();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();
app.RegisterDbContexts();

app.Run();