﻿using TestProjectWeb.Data.Enums;

namespace TestProjectWeb.Data.DbModels
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string LearningLanguage { get; set; }
        public LanguageLevel LanguageLevel { get; set; }


        public string Login { get; set; }
        public string Password { get; set; }

        public virtual List<Word> Words { get; set; }
        public virtual List<Quiz> Quizzes { get; set; }
    }
}
