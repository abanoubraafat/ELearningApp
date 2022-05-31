using AutoMapper;
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
            CreateMap<AssignmentFeedbackDTO, AssignmentFeedback>();
            CreateMap<AssignmentGradeDTO, AssignmentGrade>();
            CreateMap<BadgeDTO, Badge>();
            CreateMap<ContentDTO, Content>();
            CreateMap<CourseDTO, Course>();
            CreateMap<FeatureDTO, Feature>();
            CreateMap<LatestPassedLessonDTO, LatestPassedLesson>();
            CreateMap<LessonDTO, Lesson>();
            CreateMap<NoteDTO, Note>();
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
            CreateMap<AnnouncementDTO, Announcement>();
            CreateMap<ResourceDTO, Resource>();
            CreateMap<Course, CourseDetailsDTO>().ReverseMap();

            CreateMap<AssignmentAnswer, AssignmentAnswerDetailsDTO>();
            CreateMap<QuestionAnswer, QuestionAnswerDetailsDTO>();
            //CreateMap<QuizAnswer, QuizAnswerDetailsDTO>();

            CreateMap<Assignment, AssignmentDTO>();
            CreateMap<AssignmentAnswer, AssignmentAnswerDTO>();
            CreateMap<AssignmentFeedback, AssignmentFeedbackDTO>();
            CreateMap<AssignmentGrade, AssignmentGradeDTO>();
            CreateMap<Badge, BadgeDTO>();
            CreateMap<Content, ContentDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<Feature, FeatureDTO>();
            CreateMap<LatestPassedLesson, LatestPassedLessonDTO>();
            CreateMap<Lesson, LessonDTO>();
            CreateMap<Note, NoteDTO>();
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
            CreateMap<Announcement, AnnouncementDTO>();
            CreateMap<Resource, ResourceDTO>();

            CreateMap<AssignmentAnswerDetailsDTO, AssignmentAnswer>();
            CreateMap<QuestionAnswerDetailsDTO, QuestionAnswer>();
            //CreateMap<QuizAnswerDetailsDTO, QuizAnswer>();


            //CreateMap<MovieDto, Movie>()
            //    .ForMember(src => src.Poster, opt => opt.Ignore());
        }
    }
}
