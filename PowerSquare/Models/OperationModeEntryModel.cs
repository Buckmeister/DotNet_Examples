namespace PowerSquare.Models
{
    class OperationModeEntryModel
    {
        public string Mode { get; set; }

        public string Display { get; set; }

        public OperationModeEntryModel(string mode, string display)
        {
            Mode = mode;
            Display = display;
        }

        public override string ToString()
        {
            return Mode;
        }
    }
}
