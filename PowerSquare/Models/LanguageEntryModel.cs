namespace PowerSquare.Models
{
    class LanguageEntryModel
    {
        public string Language { get; set; }

        public string Display { get; set; }

        public LanguageEntryModel(string language, string display)
        {
            Language = language;
            Display = display;
        }

        public override string ToString()
        {
            return Language;
        }
    }
}
