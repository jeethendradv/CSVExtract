namespace csvextract.Exception
{
    public class InvalidRowLengthException : System.Exception
    {
        public InvalidRowLengthException() : base("Row values should be equal to number of columns.") { }
    }
}
