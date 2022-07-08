using ELearning_App.Domain.Context;
using ELearning_App.Repository.Repositories;
using ELearning_App.Repository.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepositories;
using Repository.Repositories;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()));

builder.Services.AddDbContext<DbContext, ELearningContext>(options =>

        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ELearningContext).Assembly.FullName)));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

//builder.Services.AddAuthentication().AddGoogle(options =>
//{
//    IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");

//    options.ClientId = googleAuthSection["ClientId"];
//    options.ClientSecret = googleAuthSection["ClientSecret"];
//});
//builder.Services.AddDbContext<ELearningContext>(options => options.UseSqlServer("data source=ABANOUB\\SQLEXPRESS;Initial Catalog=ELearningDB;integrated security=true;", b => b.MigrationsAssembly("ELearning_App")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IAssignmentAnswerRepository, AssignmentAnswerRepository>();
//builder.Services.AddScoped<IAssignmentFeedbackRepository, AssignmentFeedbackRepository>();
//builder.Services.AddScoped<IAssignmentGradeRepository, AssignmentGradeRepository>();
//builder.Services.AddScoped<IBadgeRepository, BadgeRepository>();
builder.Services.AddScoped<IContentRepository, ContentRepository>();
//builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
//builder.Services.AddScoped<IQuizAnswerRepository, QuizAnswerRepository>();
builder.Services.AddScoped<IQuizGradeRepository, QuizGradeRepository>();
builder.Services.AddScoped<IToDoListRepository, ToDoListRepository>();
//builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
//builder.Services.AddScoped<ILatestPassedLessonRepository, LatestPassedLessonRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
//builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuestionAnswerRepository, QuestionAnswerRepository>();
builder.Services.AddScoped<IQuestionChoiceRepository, QuestionChoiceRepository>();
//builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
builder.Services.AddScoped<IParentStudentRepository, ParentStudentRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
