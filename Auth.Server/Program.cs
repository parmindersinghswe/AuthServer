using Auth.Domain.Middlewares;
using Auth.Server.Data.Context;
using Auth.Server.Data.Entities.Custom;
using Auth.Server.Extensions;
using Auth.Server.Helpers;
using Auth.Server.Models.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");


//builder.WebHost.ConfigureAppConfiguration((context, config) => {
//	config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
//});
// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddCors();
builder.Services.AddMvc();
builder.Services.AddControllers().AddJsonOptions(options =>
			options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddAuthentication().AddGoogle(options =>
{
	options.ClientId = "721600412288-9mnq4d434aih5i2rsl3qarncordc4ttl.apps.googleusercontent.com";
	options.ClientSecret = "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";
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
				 {
					 {
						   new OpenApiSecurityScheme
							 {
								 Reference = new OpenApiReference
								 {
									 Type = ReferenceType.SecurityScheme,
									 Id = "Bearer"
								 }
							 },
							 new string[] {}
					 }
				 });
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartState_Api", Version = "v1" });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
 // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
 // options.UseMySQL("server=localhost;database=SharePoint;user=root;password=test123;CharSet=utf8;")
 //options.UseSqlServer("Server=DESKTOP-AJ035CM\\MSSQLSERVER01;Database=MicroservicesTest;Trusted_Connection=True;MultipleActiveResultSets=true")
 options.UseSqlite(@"Data Source=../mydatabase.db;")
);
builder.Services.AddIdentity<AspnetUser, AspNetRole>(options =>
{
	options.SignIn.RequireConfirmedEmail = AuthorizationConfiguration.RequireConfirmedEmail;
	options.Password.RequiredLength = AuthorizationConfiguration.RequiredLength;
	options.Password.RequireNonAlphanumeric = AuthorizationConfiguration.RequireNonAlphanumeric;
	options.Password.RequireUppercase = AuthorizationConfiguration.RequireUppercase;
	options.Password.RequireLowercase = AuthorizationConfiguration.RequireLowercase;
	//options.Password.RequiredUniqueChars = 3;
	//options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
	//options.Lockout.MaxFailedAccessAttempts = 2;
	//  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.InjectSingleton();
builder.Services.InjectTransiant();
builder.Services.InjectScopped();
var app = builder.Build();

app.UseMiddleware<AuthorizationMiddleware>();
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
			 .AllowAnyOrigin()
			 .AllowAnyMethod()
			 .AllowAnyHeader());

app.Run();
