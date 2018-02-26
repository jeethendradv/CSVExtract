using csvextract.Exception;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace csvextract
{
    public static class CSVHelper
    {
        private const string CSV_EXTENSION = "csv";

        public static void Group(string fileName, string columnName)
        {
            CSVReader reader = new CSVReader();
            CSV csv = reader.Read(FileHelper.GetFullPath(fileName));
            if (!csv.ContainsColumn(columnName))
            {
                throw new ColumnNotFoundException();
            }
            
            var grouping = csv.GetRows().Where(r => !r.IsHeaderRow).GroupBy(r => r.Columns.Single(x => x.Header.TrimDoubleQuotes().ToLower() == columnName.ToLower()).Value);
            foreach (var group in grouping)
            {
                create(generateFileName(fileName, group.Key.TrimDoubleQuotes()), group.Select(x => x).ToList(), Encoding.GetEncoding("iso-8859-1"));
            }
        }

        private static string GetFilePath(string columnName)
        {
            return string.Format("{0}\\{1}.{2}", FileHelper.GetExecutingDirectory(), columnName, CSV_EXTENSION);
        }

        private static string generateFileName(string filename, string columnValue)
        {
            string name = Path.GetFileNameWithoutExtension(FileHelper.GetFullPath(filename));
            return string.Format("{0}-{1}", name, columnValue);
        }

        private static void create(string fileName, List<CSVRow> rows, Encoding encoding)
        {
            string filePath = GetFilePath(fileName.ReplaceInvalidFileNameChars());
            using (StreamWriter writer = new StreamWriter(filePath, false, encoding))
            {
                writer.WriteLine(rows.First().GetHeader());
                foreach (CSVRow row in rows)
                {
                    writer.WriteLine(row.ToString());
                }
            }
        }
    }
}
