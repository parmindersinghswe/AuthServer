using Auth.Gateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => {
	options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Configuration.AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.MapControllers();
var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:5273/");
app.UseMiddleware<RegisterClaims>(httpClient);
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
