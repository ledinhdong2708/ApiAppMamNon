using API_WebApplication.Helpers;
using API_WebApplication.Interfaces.AppIDs;
using API_WebApplication.Interfaces.Classs;
using API_WebApplication.Interfaces.GiaTiens;
using API_WebApplication.Interfaces.DanThuocs;
using API_WebApplication.Interfaces.DiemDanhs;
using API_WebApplication.Interfaces.DinhDuongs;
using API_WebApplication.Interfaces.HoatDongs;
using API_WebApplication.Interfaces.HocPhiModels;
using API_WebApplication.Interfaces.HocPhis;
using API_WebApplication.Interfaces.KhoaHocs;
using API_WebApplication.Interfaces.Logins;
using API_WebApplication.Interfaces.MaterBieuDos;
using API_WebApplication.Interfaces.MaterHocPhis;
using API_WebApplication.Interfaces.NhatKy;
using API_WebApplication.Interfaces.PhanCongGiaoVienModels;
using API_WebApplication.Interfaces.Reports;
using API_WebApplication.Interfaces.SoBeNgoans;
using API_WebApplication.Interfaces.Students;
using API_WebApplication.Interfaces.TableBinhLuanInterface;
using API_WebApplication.Interfaces.TableLikeInterface;
using API_WebApplication.Interfaces.ThoiKhoaBieu;
using API_WebApplication.Interfaces.ThoiKhoaBieus;
using API_WebApplication.Interfaces.TinTuc;
using API_WebApplication.Interfaces.XinNghiPheps;
using API_WebApplication.Models;
using API_WebApplication.Responses.HocPhis;
using API_WebApplication.Services.AppIDs;
using API_WebApplication.Services.GiaTiens;
using API_WebApplication.Services.Classs;
using API_WebApplication.Services.DanThuocs;
using API_WebApplication.Services.DiemDanhs;
using API_WebApplication.Services.DinhDuongs;
using API_WebApplication.Services.HoatDongs;
using API_WebApplication.Services.HocPhiModels;
using API_WebApplication.Services.HocPhis;
using API_WebApplication.Services.KhoaHocs;
using API_WebApplication.Services.Logins;
using API_WebApplication.Services.MaterBieuDos;
using API_WebApplication.Services.MaterHocPhis;
using API_WebApplication.Services.NhatKys;
using API_WebApplication.Services.PhanCongGiaoViens;
using API_WebApplication.Services.Reports;
using API_WebApplication.Services.SoBeNgoans;
using API_WebApplication.Services.Students;
using API_WebApplication.Services.TableBinhLuanServices;
using API_WebApplication.Services.TableLikeServices;
using API_WebApplication.Services.ThoiKhoaBieu;
using API_WebApplication.Services.ThoiKhoaBieus;
using API_WebApplication.Services.TinTucs;
using API_WebApplication.Services.XinNghiPheps;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using API_WebApplication.Interfaces.SendEmail;
using API_WebApplication.Services.SendEmail;
using API_WebApplication.Interfaces.SMS;
using API_WebApplication.Services.SMS;
using API_WebApplication.Requests.SendEmail;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader()
        //builder.AllowAnyOrigin()
        );
});
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API Application",
        Description = "Authorized API using JWT Authentication and refresh Tokens",
        TermsOfService = new Uri("http://codingsonata.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Contact us",
            Url = new Uri("http://codingsonata.com/contact"),
            Email = "aram@codingsonata.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "License",
            Url = new Uri("http://codingsonata.com/")
        }
    });
    options.AddSecurityDefinition("JWT Bearer", new OpenApiSecurityScheme
    {
        Description = "This is a JWT bearer authentication scheme",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme{
            Reference = new OpenApiReference{
                Id = "JWT Bearer",
                Type = ReferenceType.SecurityScheme
            }
        }, new List<string>()
        }
});
});

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<TwiloSettings>( builder.Configuration.GetSection("Twilio") );


builder.Services.AddDbContext<API_Application_V1Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = TokenHelper.Issuer,
        ValidAudience = TokenHelper.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))
    };

});
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ISendEmailService, SendEmailSerivce>();
builder.Services.AddTransient<ISendSMSService, SenSMSSerivce>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IThoiKhoaBieuServiceExample, ThoiKhoaBieuServiceExample>();
builder.Services.AddTransient<IThoiKhoaBieuService, ThoiKhoaBieuService>();
builder.Services.AddTransient<ISoBeNgoanService, SoBeNgoanService>();
builder.Services.AddTransient<IDinhDuongService, DinhDuongService>();
builder.Services.AddTransient<IHocPhiService, HocPhiService>();
builder.Services.AddTransient<ITinTucService, TinTucService>();
builder.Services.AddTransient<IGiaTienService, GiaTiensService>();
builder.Services.AddTransient<IDiemDanhService, DiemDanhService>();
builder.Services.AddTransient<IXinNghiPhepService, XinNghiPhepService>();
builder.Services.AddTransient<IMaterBieuDoService, MaterBieuDoService>();
builder.Services.AddTransient<IDanThuocService, DanThuocService>();
builder.Services.AddTransient<IKhoaHocService, KhoaHocService>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<INhatKyService, NhatKyService>();
builder.Services.AddTransient<IAppIDService, AppIDService>();
builder.Services.AddTransient<IMaterHocPhiService, MaterHocPhiService>();
builder.Services.AddTransient<IHocPhiModelService, HocPhiModelService>();
builder.Services.AddTransient<IHoatDongService, HoatDongService>();
builder.Services.AddTransient<IPhanCongGiaoVienService,PhanCongGiaoVienService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<INhatKyService, NhatKyService>();
builder.Services.AddTransient<ITableLikeService, TableLikeService>();
builder.Services.AddTransient<ITableBinhLuanService, TableBinhLuanService>();
builder.Services.AddTransient<ISendEmailService, SendEmailSerivce>();
builder.Services.AddTransient<ISendSMSService, SenSMSSerivce>();
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    //app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers["Access-Control-Allow-Origin"] = "*";
        context.Context.Response.Headers["Access-Control-Allow-Headers"] = "*";
    }
});

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();


app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "upload")),
    RequestPath = "/upload",
    EnableDefaultFiles = true,
});
app.UseAuthorization();

app.MapControllers();

app.Run();
