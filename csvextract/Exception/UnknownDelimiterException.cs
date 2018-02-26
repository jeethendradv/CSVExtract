namespace csvextract.Exception
{
    public class UnknownDelimiterException : System.Exception
    {
        private const string MESSAGE = "CSV file contains unknown delimiter. Enter 'csvextract ?' to find the list of supported delimiter's.";
        public UnknownDelimiterException() : base(MESSAGE) { }
    }
}
