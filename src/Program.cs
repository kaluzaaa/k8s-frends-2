using System;
using System.Diagnostics;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Console.WriteLine($"The configuration value is {app.Configuration["test"]}");
app.Urls.Add("http://*:3000");
app.MapGet("/test", () => app.Configuration["test"]);

app.MapGet("/", () => "Hello World!");

app.MapGet("/cpu", () => {
    Stopwatch stopwatch = Stopwatch.StartNew();
    Thread.SpinWait(100*1000000);
    return stopwatch.Elapsed.TotalMilliseconds;
});

app.Run();
