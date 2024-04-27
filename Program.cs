using SignalRChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Cors
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin",
            builder => builder
                .AllowAnyMethod()
                .SetIsOriginAllowed(host => true)
                .AllowAnyHeader()
                .AllowCredentials()
                );
    });
builder.Services
.AddSignalR();

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

app.UseCors("AllowAnyOrigin");

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Urls.Add("http://0.0.0.0:5094");

app.Run();
