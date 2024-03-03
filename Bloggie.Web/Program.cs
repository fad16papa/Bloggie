using Bloggie.Web.Data;
using Bloggie.Web.Repositories.Interfaces;
using Bloggie.Web.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.OAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BloggieDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("BloggieDbConntectionString"),
    new MySqlServerVersion(new Version(8, 3, 0)));
});

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("BloggieAuthConnectionString"),
    new MySqlServerVersion(new Version(8, 3, 0)));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Defualt settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Google Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Google:GoogleClientId"];
    options.ClientSecret = builder.Configuration["Google:GoogleClientSecret"];
});

//Dependecy Container
builder.Services.AddScoped<ITagInterface, TagService>();
builder.Services.AddScoped<IBlogPostInterface, BlogPostService>();
builder.Services.AddScoped<IImagesInterface, ImagesService>();
builder.Services.AddScoped<IBlogPostLikeInterface, BlogPostLikeService>();
builder.Services.AddScoped<IBlogPostCommentInterface, BlogPostCommentService>();
builder.Services.AddScoped<IUserInterface, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
