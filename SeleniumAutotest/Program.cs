using SeleniumAutotest.Extensions;
using SeleniumAutotest.Core.Scenarios;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SeleniumAutotest.Data;
using System;
using Newtonsoft.Json;
using SeleniumAutotest.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()/*.AddRazorRuntimeCompilation()*/;
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
builder.Services
    .AddControllersWithViews()    
    .AddNewtonsoftJson(opt =>
    {
        opt.UseMemberCasing();
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    })
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddMemoryCache();
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["Data:DefaultConnection:ConnectionString"]);
}, ServiceLifetime.Scoped);

builder.Services.AddSingleton(provider =>
{
    var cfg = provider.CreateScope().ServiceProvider.GetService<IConfiguration>();
    return new ApplicationDbContextFactory(cfg);
});

builder.Services.AddScoped(provider =>
{
    var ctx = provider.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
    return new Logger(ctx);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.UseRouting();
app.UseMvc();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();      
}

app.Run();