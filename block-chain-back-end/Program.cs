using API1.Context;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
//builder.Services.AddDbContext<AppDbContext>(options=> options.UseMongoDb);
//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains());
});
//JSON serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(OptionsBuilderConfigurationExtensions => OptionsBuilderConfigurationExtensions.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());
var app = builder.Build();

// Configure the HTTP request pipeline.
// app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains());
app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();


