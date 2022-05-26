//using ELearning_App.Repository.IRepositories;
//using ELearning_App.Repository.Repositories;
//using ELearning_App.Repository.UnitOfWork;
//using ELearning_App.Service.IServices;
//using ELearning_App.Service.Services;

//namespace ELearning_App.Helpers
//{
//    public static class RegisterDependencyInjection
//    {
//        public static void RegisterServices (this IServiceCollection services)
//        {
//            services.AddScoped<IUnitOfWork, UnitOfWork>();
//            services.AddScoped<ILoginInfoRepository, LoginInfoRepository>();
//            services.AddScoped<ILoginInfoService, LoginInfoService>();
//            services.AddScoped<IParentRepository, ParentRepository>();
//            services.AddScoped<IParentService, ParentService>();
//            services.AddScoped<IStudentRepository, StudentRepository>();
//            services.AddScoped<IStudentService, StudentService>();
//            services.AddScoped<ITeacherRepository, TeacherRepository>();
//            services.AddScoped<ITeacherService, TeacherService>();
//            services.AddScoped<ICourseRepository, CourseRepository>();
//            services.AddScoped<ICourseService, CourseService>();
//            //services.AddScoped<IGradeRepository, GradeRepository>();
//            //services.AddScoped<IGradeService, GradeService>();
//        }
//    }
//}
