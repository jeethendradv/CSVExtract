using System.Collections.Generic;

namespace csvextract
{
    public class CSV
    {
        private List<CSVRow> rows;
        public CSV()
        {
            rows = new List<CSVRow>();
        }

        public string Delimiter { get; set; }

        public List<CSVRow> GetRows()
        {
            return rows;
        }        

        public bool ContainsColumn(string columnName)
        {
            return rows.Find(r => r.IsHeaderRow && r.Columns.Find(c => c.Header.TrimDoubleQuotes().ToLower() == columnName.ToLower()) != null) != null;
        }

        public void AddRow(CSVRow row)
        {
            row.SetDelimiter(Delimiter);
            rows.Add(row);
        }
    }
}
