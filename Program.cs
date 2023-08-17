using Microsoft.AspNetCore.Http.Features;
using Poc.Vimeo.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    }); ;

builder
    .Services
    .AddSingleton(builder.Configuration.GetSection(nameof(VimeoConfiguration)).Get<VimeoConfiguration>()!);

builder
    .Services
    .AddSingleton(builder.Configuration.GetSection(nameof(TenantConfiguration)).Get<TenantConfiguration>()!);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin());
});

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
});

builder.WebHost.ConfigureKestrel(options =>
    options.Limits.MaxRequestBodySize = int.MaxValue);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
//app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAllOrigins");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

ServiceLocator.ServiceProvider = app.Services;

app.Run();
