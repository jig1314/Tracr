using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Tracr.Server.Data;
using Tracr.Server.Models;
using Tracr.Server.Repositories;
using Tracr.Server.Hubs;
using Tracr.Server.Services;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("TracrDatabaseConnection") ?? "missing-string"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient("realestate", c => {
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RealEstateAPI"));
    c.DefaultRequestHeaders.Add("x-rapidapi-host", "us-real-estate.p.rapidapi.com");
    c.DefaultRequestHeaders.Add("x-rapidapi-key", Environment.GetEnvironmentVariable("RapidApiKey") ?? "missing-key");
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<IRealEstateRepo, RealEstateRepo>();
builder.Services.AddHostedService<AlertService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapHub<AlertHub>("/alerthub");
app.MapRazorPages();
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
