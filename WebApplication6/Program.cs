using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;
using WebApplication6;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "20/03/2024 tarihli egitim" +
    "<a href=\"https://github.com/karunland/api-event\">  Source Code</a>", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
});

app.UseRewriter(new RewriteOptions().AddRedirect("modos/(.*)", "todos/$1"));

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request: {context.Request.Path} {DateTime.Now}");
    await next();
    Console.WriteLine($"Response: {context.Response.StatusCode} {DateTime.Now}");
});

app.RegisterTodoItemsEndpoints();

app.Run();

