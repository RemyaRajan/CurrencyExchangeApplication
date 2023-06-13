
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/CurrencyExchange_Log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("connString");


builder.Services.AddDbContext<CurrencyExachangedbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

string authConnectionString = builder.Configuration.GetConnectionString("connStringAuth");
builder.Services.AddDbContext<IdentityAuthdbContext>(options =>
{
    options.UseSqlServer(authConnectionString);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
options.AddPolicy(name: MyAllowSpecificOrigins,
                  policy =>
                  {
                      policy.WithOrigins(builder.Configuration["ReactAPIUrl"])
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                  });
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer Authetication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference= new OpenApiReference
                    {
                        Id= JwtBearerDefaults.AuthenticationScheme,
                        Type=ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
});

builder.Services.AddIdentityCore<IdentityUser>()
       .AddRoles<IdentityRole>()
       .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("CurrencyExchange")
       .AddEntityFrameworkStores<IdentityAuthdbContext>()
       .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
  { 
      options.Password.RequireLowercase = true;
      options.Password.RequireLowercase = true;
      options.Password.RequiredLength = 5;
    }
) ; 

//Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
