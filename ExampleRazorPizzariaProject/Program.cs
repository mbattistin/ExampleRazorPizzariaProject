using ExampleRazorPizzariaProject.Data;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

var connection = new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IDataRepository>(s => new DataRepository(connection));
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

app.Run();
