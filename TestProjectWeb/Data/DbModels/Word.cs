﻿using TestProjectWeb.Data.Enums;

namespace TestProjectWeb.Data.DbModels
{
    public class Word : BaseModel
    {
        public string Value { get; set; }
        public string Translation { get; set; }
        public string Category { get; set; }
        public PartsOfSpeech PartOfSpeech { get; set; }

        public virtual User Creater { get; set; }
    }
}
