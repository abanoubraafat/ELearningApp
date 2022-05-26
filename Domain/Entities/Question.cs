﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning_App.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }

        //public int Grade { get; set; }
        //public virtual ICollection<Choice> Choices { get; set; } = new HashSet<Choice>();

        //// one to many (lesson -> Questions)
        //public int QuizId { get; set; }
        //public virtual Quiz Quiz { get; set; }
        public string FirstChoise { get; set; } // mcq choises
        public string SecondChoise { get; set; } // mcq choises
        public string? ThirdChoise { get; set; } // mcq choises
        public string? FourthChoise { get; set; } // mcq choises
        public string? Lastchoise { get; set; } // mcq choises
        public string CorrectAnswer { get; set; }
        public string? CorrectAnswer2 { get; set; }
        public string? CorrectAnswer3 { get; set; }
        public string? CorrectAnswer4 { get; set; }

        public DateTime ShowDate { get; set; } // available on ..

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        //// RelationShips:



        // one to many (Question-> QuestionAnswer)
        //public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new HashSet<QuestionAnswer>();
    }
}
