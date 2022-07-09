using AutoMapper;
using Domain.Entities;
using ELearning_App.Domain.Entities;
namespace ELearning_App.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AssignmentDTO, Assignment>();
                //.BeforeMap((src, dest) => src.Id = dest.Id);
            CreateMap<AssignmentAnswerDTO, AssignmentAnswer>();
            CreateMap<Assignment, GetAssignmentDTO>().ReverseMap();
            //CreateMap<AssignmentFeedbackDTO, AssignmentFeedback>();
            //CreateMap<AssignmentGradeDTO, AssignmentGrade>().Reverse();
            //CreateMap<BadgeDTO, Badge>();
            CreateMap<ContentDTO, Content>();
            CreateMap<CourseDTO, Course>();
            //CreateMap<FeatureDTO, Feature>();
            //CreateMap<LatestPassedLessonDTO, LatestPassedLesson>();
            CreateMap<LessonDTO, Lesson>();
            //CreateMap<NoteDTO, Note>();
            CreateMap<ParentDTO, Parent>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<QuestionAnswerDTO, QuestionAnswer>();
            CreateMap<QuizDTO, Quiz>();
            //CreateMap<QuizAnswerDTO, QuizAnswer>();
            CreateMap<QuizGradeDTO, QuizGrade>();
            CreateMap<StudentDTO, Student>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<ToDoListDTO, ToDoList>();
            CreateMap<UserDTO, User>();
            //CreateMap<AnnouncementDTO, Announcement>();
            //CreateMap<ResourceDTO, Resource>();
            CreateMap<Course, CourseDetailsDTO>().ReverseMap();
            CreateMap<QuestionChoiceDTO, QuestionChoice>().ReverseMap();
            CreateMap<AssignmentAnswer, AssignmentAnswerDetailsDTO>();
            //CreateMap<QuestionAnswer, QuestionAnswerDetailsDTO>();
            //CreateMap<QuizAnswer, QuizAnswerDetailsDTO>();

            CreateMap<Assignment, AssignmentDTO>();
            CreateMap<AssignmentAnswer, AssignmentAnswerDTO>();
            //CreateMap<AssignmentFeedback, AssignmentFeedbackDTO>();
            //CreateMap<Badge, BadgeDTO>();
            CreateMap<Content, ContentDTO>();
            CreateMap<Course, CourseDTO>();
            //CreateMap<Feature, FeatureDTO>();
            //CreateMap<LatestPassedLesson, LatestPassedLessonDTO>();
            CreateMap<Lesson, LessonDTO>();
            //CreateMap<Note, NoteDTO>();
            CreateMap<Parent, ParentDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionAnswer, QuestionAnswerDTO>();
            CreateMap<Quiz, QuizDTO>();
            //CreateMap<QuizAnswer, QuizAnswerDTO>();
            CreateMap<QuizGrade, QuizGradeDTO>();
            CreateMap<Student, StudentDTO>();
            CreateMap<Teacher, TeacherDTO>();
            CreateMap<ToDoList, ToDoListDTO>();
            CreateMap<User, UserDTO>();
            //CreateMap<Announcement, AnnouncementDTO>();
            //CreateMap<Resource, ResourceDTO>();

            CreateMap<AssignmentAnswerDetailsDTO, AssignmentAnswer>();
            //CreateMap<QuestionAnswerDetailsDTO, QuestionAnswer>();
            //CreateMap<QuizAnswerDetailsDTO, QuizAnswer>();
            //CreateMap<QuizGrade, QuizGradeDetailsShortDTO>();

            //CreateMap<MovieDto, Movie>()
            //    .ForMember(src => src.Poster, opt => opt.Ignore());
            CreateMap<Assignment, AssignmentDetailsDTO>()
                .ForMember(d => d.AssignedGrade, o => o.MapFrom(s => s.AssignmentAnswers.Select(c => c.AssignedGrade).FirstOrDefault()))
                .ForMember(d => d.AssignmentAnswerId, o => o.MapFrom(s => s.AssignmentAnswers.Select(c => c.Id).FirstOrDefault()));
            CreateMap<QuizGradeDetailsDTO, QuizGrade>().ReverseMap();
            CreateMap<Question, QuestionDetailsDTO>().ReverseMap();
            CreateMap<Assignment, AssignmentDetailsShortDTO>()
                .ForMember(d => d.AssignedGrade, o => o.MapFrom(s => s.AssignmentAnswers.Select(c => c.AssignedGrade).FirstOrDefault()))
                .ForMember(d => d.AssignmentAnswerId, o => o.MapFrom(s => s.AssignmentAnswers.Select(c => c.Id).FirstOrDefault()))
                .ForMember(d => d.AssignmentId, o => o.MapFrom(s => s.Id));

            CreateMap<Quiz, GetQuizDTO>();
            CreateMap<Quiz, QuizDetailsShortDTO>()
                .ForMember(d => d.AssignedGrade, o => o.MapFrom(s => s.QuizGrades.Select(c => c.AssignedGrade).FirstOrDefault()))
                .ForMember(d => d.QuizId, o => o.MapFrom(s => s.Id));
            CreateMap<Assignment, GetAssignmentWithSubmitted>()
                .ForMember(d => d.AssignmentAnswerId, o => o.MapFrom(s => s.AssignmentAnswers.Select(a => a.Id).FirstOrDefault()));
            CreateMap<ParentStudent, ParentStudentsUnVerifiedRequestDTO>();
            CreateMap<CourseStudent, CourseStudentUnVerifiedRequestsDTO>()
                 .ForMember(d => d.CourseName, o => o.MapFrom(s => s.Course.CourseName))
                 .ForMember(d => d.CourseImage, o => o.MapFrom(s => s.Course.CourseImage));

        }
    }
}
