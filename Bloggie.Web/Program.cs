using Bloggie.Web.Data;
using Bloggie.Web.Repositories.Interfaces;
using Bloggie.Web.Repositories.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Load configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add configuration to the container
builder.Services.AddSingleton(builder.Configuration);

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

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    //Enable email confirmation
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    //Defualt settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.HttpOnly = HttpOnlyPolicy.None;
    options.Secure = CookieSecurePolicy.Always; // Set to None if not using HTTPS
});

// Google Authentication
builder.Services.AddAuthentication()
.AddGoogle(options =>
{
    var configuration = builder.Configuration;
    options.ClientId = configuration["Google:GoogleClientId"];
    options.ClientSecret = configuration["Google:GoogleClientSecret"];
});

//Dependecy Container
builder.Services.AddScoped<ITagInterface, TagService>();
builder.Services.AddScoped<IBlogPostInterface, BlogPostService>();
builder.Services.AddScoped<IImagesInterface, ImagesService>();
builder.Services.AddScoped<IBlogPostLikeInterface, BlogPostLikeService>();
builder.Services.AddScoped<IBlogPostCommentInterface, BlogPostCommentService>();
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddTransient<IEmailSender, EmailSenderService>();

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

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Add the following route for email confirmation
    endpoints.MapControllerRoute(
        name: "confirmEmail",
        pattern: "{controller=Account}/{action=ConfirmEmail}/{userId?}/{code?}");
});

app.Run();
