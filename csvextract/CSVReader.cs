using csvextract.Exception;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace csvextract
{
    public class CSVReader
    {
        private const string FILE_EXTENSION = ".csv";
        private string[] delimiter_list = new string[] { ";", ",", "\t", "^", "|" };
        private string filePath;
        private char[] delimiter;

        public CSV Read(string path)
        {
            filePath = path;
            validateFile();
            return read();
        }

        private bool isCsvFile()
        {
            FileInfo info = new FileInfo(filePath);
            return info.Extension.ToLower() == FILE_EXTENSION;
        }

        private CSV read()
        {
            CSV csv = new CSV();
            using (StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("iso-8859-1")))
            {
                string line = string.Empty;
                delimiter = getDelimiter().ToCharArray();
                csv.Delimiter = new string(delimiter);
                string[] headers = reader.ReadLine().Split(delimiter);
                csv.AddRow(createHeaderRow(headers));
                int numberOfColumns = headers.Length;
                while ((line = reader.ReadLine()) != null)
                {
                    List<CSVColumn> columns = getColumns(headers);
                    string[] values = line.Split(delimiter);
                    if (values.Length != numberOfColumns)
                    {
                        throw new InvalidRowLengthException();
                    }
                    for (int index = 0; index < numberOfColumns; index++)
                    {
                        columns[index].Value = values[index];
                    }
                    CSVRow row = new CSVRow
                    {
                        Columns = columns
                    };
                    csv.AddRow(row);
                }
            }
            return csv;
        }

        private CSVRow createHeaderRow(string[] headers)
        {
            CSVRow headerRow = new CSVRow
            {
                IsHeaderRow = true
            };
            foreach (string header in headers)
            {
                CSVColumn column = new CSVColumn
                {
                    Header = header
                };
                headerRow.Columns.Add(column);
            }
            return headerRow;
        }

        private void validateFile()
        {
            if (!isCsvFile())
            {
                throw new InvalidFileTypeException();
            }
            if (!File.Exists(filePath))
            {
                throw new InvalidFilePathException();
            }
        }

        private List<CSVColumn> getColumns(string[] headers)
        {
            int index = 0;
            List<CSVColumn> columns = new List<CSVColumn>();
            foreach (string header in headers)
            {
                CSVColumn column = new CSVColumn();
                column.Header = header;
                column.Index = index;
                columns.Add(column);
                index += 1;
            }
            return columns;
        }

        private string getDelimiter()
        {
            string delimiter = string.Empty;
            using (StreamReader rw = new StreamReader(filePath))
            {
                string headers = rw.ReadLine();
                if (string.IsNullOrEmpty(headers))
                {
                    throw new EmptyHeaderException();
                }
                delimiter = delimiter_list.First(d => headers.Contains(d));
            }

            if (string.IsNullOrEmpty(delimiter))
            {
                throw new UnknownDelimiterException();
            }
            return delimiter;
        }
    }
}
