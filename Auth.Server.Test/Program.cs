using Auth.Domain.Middlewares;
using Auth.Domain.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ClaimsService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
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


var app = builder.Build();

app.UseMiddleware<RegisterClaims>();

app.UseMiddleware<AuthorizationMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("CORSPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
