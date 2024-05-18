using System;

namespace Majenka.Csv
{
    public class CsvRow
    {
        private string[]? header;

        internal CsvRow(string[]? header, CsvValue[] values)
        {
            this.header = header;
            this.Values = values;
        }

        public CsvValue[] Values { get; }

        public CsvValue this[string columnName]
        {
            get
            {
                if (header == null)
                {
                    throw new ArgumentNullException("Header row has not been set");
                }

                var index = Array.IndexOf(header, columnName);

                if (index == -1)
                {
                    throw new ArgumentOutOfRangeException(string.Format("Column {0} not found", columnName));
                }

                if (index > Values.Length - 1)
                {
                    return new CsvValue(string.Empty);
                }

                return Values[index];
            }

        }

        public CsvValue this[int index]
        {
            get
            {
                return Values[index];
            }
        }
    }
}