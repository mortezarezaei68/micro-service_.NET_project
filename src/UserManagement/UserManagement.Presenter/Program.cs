using Framework.Buses;
using UserManagement.Core;
using UserManagement.Core.ServiceExtensions;
using UserManagement.Core.UserManagementContextConcept;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICapEventBus, CapEventBus>();
builder.Services.AddControllersWithViews();
builder.Services.ContextInjection(builder.Configuration);
builder.Services.AddOpenIdDictConfiguration(builder.Configuration);
builder.Services.AddCapConfigureServices<UserManagementContext>(builder.Configuration);
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.WithOrigins("http://localhost:3000", "https://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
}));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.InitializeUserDatabase().Wait();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();