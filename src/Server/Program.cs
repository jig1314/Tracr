using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Tracr.Server.Data;
using Tracr.Server.Models;
using Tracr.Server.Services;

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

builder.Services.AddHttpClient("mash", c =>{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("MashAPI"));
    c.DefaultRequestHeaders.Add("x-rapidapi-host", "mashvisor-api.p.rapidapi.com");
    c.DefaultRequestHeaders.Add("x-rapidapi-key", Environment.GetEnvironmentVariable("RapidApiKey") ?? "missing-key"); 
});

builder.Services.AddHttpClient("realestate", c => {
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RealEstateAPI"));
    c.DefaultRequestHeaders.Add("x-rapidapi-host", "us-real-estate.p.rapidapi.com");
    c.DefaultRequestHeaders.Add("x-rapidapi-key", Environment.GetEnvironmentVariable("RapidApiKey") ?? "missing-key");
});

builder.Services.AddScoped<IRealEstateAnalyzerService, RealEstateAnalyzerService>();
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
builder.Services.AddScoped<IRealEstateRepo, RealEstateRepo>();
builder.Services.AddScoped<IAnalyzerRepo, AnalyzerRepo>(); 

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


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
