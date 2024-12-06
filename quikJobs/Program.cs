using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using quikJobs.Components;
using quikJobs.Components.Account;
using quikJobs.Data;
using quikJobs.Services;

//using quikJobs.Services;
using System.Configuration;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

//My custom services
builder.Services.AddScoped<JobService>();
builder.Services.AddScoped<SavedJobService>();






builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();


var connectionString = builder.Configuration.GetConnectionString("KwicJobsConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register KwicJobsContext
var kwicJobsConnectionString = builder.Configuration.GetConnectionString("KwicJobsConnectionString")
                              ?? throw new InvalidOperationException("Connection string 'KwicJobsConnectionString' not found.");

builder.Services.AddDbContext<KwicJobsContext>(options =>
    options.UseSqlServer(kwicJobsConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Configure Identity
builder.Services.Configure<IdentityOptions>(options =>
{
    // Disable email confirmation requirement for login
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;


    // Customize password requirements
    //Reminder If I want to change error message I need to update the razor page file text
    // TODO:
    options.Password.RequireDigit = false; // Disable requirement for a number
    options.Password.RequireUppercase = false; // Disable requirement for uppercase letter
    options.Password.RequireNonAlphanumeric = false; // Disable requirement for special characters
    options.Password.RequiredLength = 6; // Set minimum password length (can be adjusted)

    

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
