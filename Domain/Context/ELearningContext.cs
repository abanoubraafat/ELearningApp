using Domain.Entities;
using ELearning_App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ELearning_App.Domain.Context
{
    public class ELearningContext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("data source=ABANOUB\\SQLEXPRESS;Initial Catalog=ELearningDB;integrated security=true;");

        // save changes(); does not enforce validation 
        // we will override savechanges() and enforce validation of entities manually
        public ELearningContext(DbContextOptions<ELearningContext> options) : base(options) { }

        public override int SaveChanges()
        {
            var Entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Modified || e.State == EntityState.Added
                           select e.Entity;

            foreach(var Entity in Entities)
            {
                ValidationContext validationContext = new ValidationContext(Entity);
                Validator.ValidateObject(Entity, validationContext, true);
            }
            return base.SaveChanges();
        }

        //fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //     .ToTable("Users")
            //     .HasDiscriminator<string>("Role")
            //     .HasValue<Student>("Student")
            //     .HasValue<Parent>("Parent")
            //     .HasValue<Teacher>("Teacher");
            // navigational property one to many
            //modelBuilder.Entity<Course>()
            //    .HasOne(c => c.Teacher) //independent entity
            //    .WithMany(t => t.Courses) //dependent entity that will be deleted if independent entity is  deleted
            //    .OnDelete(DeleteBehavior.Cascade);
            //// one to one
            //modelBuilder.Entity<Student>()  //doesn't work
            //    .HasOne(s => s.Parent)       //try again
            //    .WithOne(p => p.Student)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Lesson>()
            //    .HasOne(l => l.Course)
            //    .WithMany(c => c.Lessons)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Content>()
            //    .HasOne(c => c.Lesson)
            //    .WithMany(l => l.Contents)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Question>()
            //    .HasOne(Q => Q.Lesson)
            //    .WithMany(l => l.Questions)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<QuestionAnswer>()
            //    .HasOne(QA => QA.Question)
            //    .WithMany(Q => Q.QuestionAnswers)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<QuestionAnswer>()
            //    .HasOne(QA => QA.Student)
            //    .WithMany(S => S.QuestionAnswers)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LatestPassedLesson>()
            //    .HasOne(L => L.Student)
            //    .WithMany(S => S.LatestPassedLessons)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<LatestPassedLesson>()
            //    .HasOne(L => L.Course)
            //    .WithMany(c => c.LatestPassedLessons)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Note>()
            //    .HasOne(N => N.Student)
            //    .WithMany(S => S.Notes)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Note>()
            //    .HasOne(N => N.Lesson)
            //    .WithMany(l => l.Notes)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Assignment>()
            //    .HasOne(a => a.Course)
            //    .WithMany(c => c.Assignments)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<AssignmentAnswer>()
            //    .HasOne(an => an.Student)
            //    .WithMany(s => s.AssignmentAnswers)
            //    .OnDelete(DeleteBehavior.Cascade);

            ////modelBuilder.Entity<AssignmentAnswer>()
            ////    .HasOne(an => an.Assignment)
            ////    .WithMany(s => s.AssignmentAnswers)
            ////    .OnDelete(DeleteBehavior.Cascade);
            //// one to one
            ////modelBuilder.Entity<AssignmentAnswer>()
            ////    .HasOne(an => an.AssignmentGrade)
            ////    .WithOne(ag => ag.AssignmentAnswer)
            ////    .OnDelete(DeleteBehavior.Cascade);

            //// one to one
            ////modelBuilder.Entity<AssignmentAnswer>()
            ////    .HasOne(an => an.AssignmentFeedback)
            ////    .WithOne(af => af.AssignmentAnswer)
            ////    .OnDelete(DeleteBehavior.Cascade);

            ////// one to one
            ////modelBuilder.Entity<AssignmentAnswer>()
            ////    .HasOne(an => an.Badge)
            ////    .WithOne(B => B.AssignmentAnswer)
            ////    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Quiz>()
            //    .HasOne(Q => Q.Course)
            //    .WithMany(c => c.Quizzes)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<QuizAnswer>()
            //    .HasOne(qn => qn.Student)
            //    .WithMany(s => s.QuizAnswers)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<QuizAnswer>()
            //    .HasOne(qn => qn.Quiz)
            //    .WithMany(Q => Q.QuizAnswers)
            //    .OnDelete(DeleteBehavior.Cascade);
            //// one to one
            //modelBuilder.Entity<QuizAnswer>()
            //    .HasOne(qn => qn.QuizGrade)
            //    .WithOne(qg => qg.QuizAnswer)
            //    .OnDelete(DeleteBehavior.Cascade);
            
            //modelBuilder.Entity<ToDoList>()
            //    .HasOne(t => t.User)
            //    .WithMany(l => l.ToDoLists)
            //    .OnDelete(DeleteBehavior.Cascade);
            
            //modelBuilder.Entity<Feature>()
            //    .HasOne(c => c.Student)
            //    .WithMany(s => s.Features)
            //    .OnDelete(DeleteBehavior.Cascade);


            //// navigational property many to many


            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Courses)
            //    .WithMany(c => c.Students);
            

            //modelBuilder.Entity<Announcement>()
            //  .HasMany(A => A.Courses)
            //  .WithMany(C => C.Announcements);

            

            ///
            //modelBuilder.Entity<Student>()
            //    .HasMany(s => s.Gifts)
            //    .WithOne(g => g.Student);

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Gifts)
            //    .WithOne(g => g.Course);
            ///

            // Validations of email , password and type

            ///commenting them doesn't have effect, you may not write them
            ///
            //modelBuilder.Entity<LoginInfo>().Property(s => s.type).IsRequired();
            //modelBuilder.Entity<LoginInfo>().Property(s => s.emailAddress).IsRequired();
            //modelBuilder.Entity<LoginInfo>().Property(s => s.password).IsRequired();
            //modelBuilder.Entity<LoginInfo>().Property(s => s.Id).IsRequired();
            modelBuilder.Entity<User>().HasIndex(s => s.Id).IsUnique();
            //modelBuilder.Entity<LoginInfo>().HasKey(s => s.Id);
            ///

            //modelBuilder.Entity<Student>().Property(s => s.type).IsRequired();
            //modelBuilder.Entity<Student>().Property(s => s.emailAddress).IsRequired();
            //modelBuilder.Entity<Student>().Property(s => s.password).IsRequired();
            //modelBuilder.Entity<Student>().Property(s => s.Id).IsRequired();
            //modelBuilder.Entity<Student>().HasIndex(s => s.Id).IsUnique();
            //modelBuilder.Entity<Student>().HasKey(s => s.Id);

            //modelBuilder.Entity<Teacher>().Property(t => t.type).IsRequired();
            //modelBuilder.Entity<Teacher>().Property(t => t.emailAddress).IsRequired();
            //modelBuilder.Entity<Teacher>().Property(t => t.password).IsRequired();
            //modelBuilder.Entity<Teacher>().Property(t => t.Id).IsRequired();
            //modelBuilder.Entity<Teacher>().HasIndex(t => t.Id).IsUnique();
            //modelBuilder.Entity<Teacher>().HasKey(t => t.Id);

            //modelBuilder.Entity<Parent>().Property(p => p.type).IsRequired();
            //modelBuilder.Entity<Parent>().Property(p => p.emailAddress).IsRequired();
            //modelBuilder.Entity<Parent>().Property(p => p.password).IsRequired();
            //modelBuilder.Entity<Parent>().Property(p => p.Id).IsRequired();
            //modelBuilder.Entity<Parent>().HasIndex(p => p.Id).IsUnique();
            //modelBuilder.Entity<Parent>().HasKey(p => p.Id);

            //to make email address is unique in all tables
            modelBuilder.Entity<User>().HasIndex(l => l.EmailAddress ).IsUnique();
            //modelBuilder.Entity <Student> ().HasIndex(s => s.emailAddress).IsUnique();
            //modelBuilder.Entity<Teacher>().HasIndex(t => t.emailAddress).IsUnique();
            //modelBuilder.Entity<Parent>().HasIndex(p => p.emailAddress).IsUnique();
            //modelBuilder.Entity<Student_CC>().HasKey()
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        //public virtual DbSet<Note> Notes { get; set; }
        //public virtual DbSet<LatestPassedLesson> LatestPassedLessons { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<AssignmentAnswer> AssignmentAnswers{ get; set; }
        //public virtual DbSet <AssignmentFeedback> AssignmentFeedbacks { get; set; }
        //public virtual DbSet<AssignmentGrade> AssignmentGrades { get; set; }

        public virtual DbSet<Quiz> Quizzes{ get; set; }
        //public virtual DbSet<QuizAnswer> QuizAnswers { get; set; }
        public virtual DbSet<QuizGrade> QuizGrades { get; set; }

        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public virtual DbSet<QuestionChoice> QuestionChoices { get; set; }
        //public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet <Resource> Resources { get; set; }
        public virtual DbSet <ToDoList> ToDoLists { get; set; }


    }
}
