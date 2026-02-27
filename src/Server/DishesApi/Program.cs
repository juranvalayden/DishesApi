using DishApi.Application;
using DishesApi.Extensions;
using DishesApi.Infrastructure;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// configure the http request pipeline
app.UseExceptionHandler(applicationBuilder => applicationBuilder.Run(async context =>
{
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync("An unexpected problem occurred.");
}));

app.UseHttpsRedirection();

app.RegisterDishesEndpoints();
app.RegisterIngredientsEndpoints();
app.RegisterDbContexts();

app.Run();