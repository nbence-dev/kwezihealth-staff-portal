// https://www.youtube.com/watch?v=qhBF3eMpX0E&t=16s
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Data;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Repositories;
using ITEHA3_B33___Formative___Tygervalley___EDUV4846233.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// configuring in-memory db
builder.Services.AddDbContext<KweziHealthDbContext>(options =>
{
    options.UseInMemoryDatabase("KweziHealthDb");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Access/Login";
    options.LogoutPath = "/Access/Logout";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<StaffRepository>();
builder.Services.AddScoped<StaffService>();
builder.Services.AddScoped<SystemAdminRepository>();
builder.Services.AddScoped<AuthService>();

// Pre-creating records 

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KweziHealthDbContext>();
    var seeder = new DataSeeder(context);
    seeder.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Access}/{action=Login}/{id?}")
    .WithStaticAssets();


app.Run();