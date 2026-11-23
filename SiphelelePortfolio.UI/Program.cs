using Microsoft.AspNetCore.Mvc;
using SiphelelePortfolio.Data.DataAccess;
using SiphelelePortfolio.Data.Repositories.Implementations;
using SiphelelePortfolio.Data.Repositories.Interfaces;
using SiphelelePortfolio.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// ── SMTP mapping (EmailSettings → SmtpOptions) ────────────────────
var emailSection = builder.Configuration.GetSection("EmailSettings");
builder.Services.Configure<SmtpOptions>(options =>
{
    options.Host = emailSection["SmtpHost"] ?? "smtp.gmail.com";
    options.Port = int.TryParse(emailSection["SmtpPort"], out var p) ? p : 587;
    options.UseStartTls = true;
    options.Username = emailSection["SmtpUser"] ?? "";
    options.Password = emailSection["SmtpPass"] ?? "";
    options.FromName = emailSection["SenderName"] ?? "Siphelele Portfolio";
    options.FromEmail = emailSection["SenderEmail"] ?? "";
});

// ── DI ───────────────────────────────────────────────────────────
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IEngagementRepository, EngagementRepository>();
builder.Services.AddScoped<ICollaborationRepository, CollaborationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IPdfService, PdfService>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(o =>
{
    o.IdleTimeout = TimeSpan.FromHours(8);
    o.Cookie.Name = ".Siphelele.Session";
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else { app.UseDeveloperExceptionPage(); }

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();