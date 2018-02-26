using System.Collections.Generic;
using System.Linq;

namespace csvextract
{
    public class CSVRow
    {
        private string delimiter;

        public CSVRow()
        {
            Columns = new List<CSVColumn>();
        }
        public bool IsHeaderRow { get; set; }
        public List<CSVColumn> Columns { get; set; }

        public override string ToString()
        {
            return string.Join(delimiter, Columns.Select(c => c.Value));
        }

        public string GetHeader()
        {
            return string.Join(delimiter, Columns.Select(c => c.Header));
        }

        public void SetDelimiter(string delimiter)
        {
            this.delimiter = delimiter;
        }
    }
}
