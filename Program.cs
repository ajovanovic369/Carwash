using CarWash;
using CarWash.Filters;
using CarWash.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors(options =>
{    options.AddPolicy("AllowAPIRequestIO",
        builder => builder.SetIsOriginAllowed(origin => true).WithExposedHeaders("totalAmountPages").WithExposedHeaders("count").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
});
builder.Services.AddDataProtection();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
                        ClockSkew = TimeSpan.Zero
}
                );
builder.Services.AddTransient<IFileStorageService, InAppStorageService>();
builder.Services.AddTransient<IHostedService, CheckingWorkingHours>();
builder.Services.AddTransient<IHostedService, CheckReservationStatus>();
builder.Services.AddTransient<IHostedService, AddingEarnings>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(LoggerExceptionFilter));
})
                .AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAPIRequestIO");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowAPIRequestIO");

app.MapControllers();

app.Run();
