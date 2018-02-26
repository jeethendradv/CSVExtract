using csvextract.Exception;
using System;

namespace csvextract
{
    public class Start
    {
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    displayHelp();
                }
                else
                {
                    if (args.Length < 2 || args.Length > 2)
                    {
                        throw new InvalidArgumentException();
                    }
                    string filename = args[0];
                    string columnName = args[1];
                    CSVHelper.Group(filename, columnName);
                    Console.WriteLine(string.Format("File has been split successfully. Path: {0}", FileHelper.GetExecutingDirectory()));
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        private static void displayHelp()
        {
            Console.WriteLine("USAGE:");
            Console.WriteLine("     csvextract filename columnname");
            Console.WriteLine("             filename - name of the csv file.");
            Console.WriteLine("             columnname - name of the column in the csv file.");
            Console.WriteLine();
            Console.WriteLine("EXAMPLE:");
            Console.WriteLine("     csvextract clientlist.csv ID");
            Console.WriteLine("     csvextract clientlist.csv \"BP Code\"");
            Console.WriteLine("     csvextract H:\\FolderPath\\clientlist.csv \"BP Code\"");
            Console.WriteLine("NOTE: ");
            Console.WriteLine("     supported delimiters ; , \\t | ^");
        }
    }
}