namespace csvextract.Exception
{
    public class EmptyHeaderException : System.Exception
    {
        public EmptyHeaderException() : base("File does not contain any headers.") { }
    }
}
