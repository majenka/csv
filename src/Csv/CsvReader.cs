using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Majenka.Csv
{
    public class CsvReader : IDisposable
    {
        // End of line characters (Environment.NewLine doesn't cut it when files are shared between OSs)
        private const char carrRet = '\r';
        private const char lineFeed = '\n';

        private bool firstRowHeader;
        private char delimiter;
        private char textDelimiter;
        private TextReader reader;
        private string[]? header = null;

        public CsvReader(string filename, int? codePage = null, char delimiter = ',', char textDelimiter = '"', bool firstRowHeader = true)
        {
            this.firstRowHeader = firstRowHeader;
            this.delimiter = delimiter;
            this.textDelimiter = textDelimiter;

            if (codePage.HasValue)
            {
                reader = new StreamReader(filename, Encoding.GetEncoding(codePage.Value));
            }
            else
            {
                reader = new StreamReader(filename);
            }
        }

        public CsvReader(Stream stream, char delimiter = ',', char textDelimiter = '"', bool firstRowHeader = true)
        {
            this.firstRowHeader = firstRowHeader;
            this.delimiter = delimiter;
            this.textDelimiter = textDelimiter;

            reader = new StreamReader(stream);
        }

        public string[]? ColumnNames
        {
            get
            {
                if (header == null && firstRowHeader)
                {
                    ReadRow();
                }

                return header;
            }
        }

        // ReadRow allows for end-of-line characters and escaped text delimiters within text fields
        public CsvRow? ReadRow()
        {
            var values = new List<CsvValue>();
            var inText = false;
            var inEscape = false;
            string? value = null;
            char c;
            int i, p;

            while ((i = reader.Read()) != -1)
            {
                c = (char)i;

                // End of row?
                if (!inText && c == lineFeed)
                {
                    if (value != null)
                    {
                        values.Add(value);
                    }

                    // Header row?
                    if (firstRowHeader && header == null)
                    {
                        header = values.Select(x => (string)x).ToArray();
                        return ReadRow();
                    }

                    return new CsvRow(header, values.ToArray());
                }

                // End of field?
                if (!inText && c == delimiter)
                {
                    values.Add(value ?? string.Empty);
                    value = null;
                }
                else
                {
                    // Start or end of text, or escape?
                    if (c == textDelimiter)
                    {
                        if (!inText)
                        {
                            inText = true;
                        }
                        else
                        {
                            if (inEscape)
                            {
                                inEscape = false;
                            }
                            else
                            {
                                p = reader.Peek();

                                if (p == (int)textDelimiter)
                                {
                                    value += c;
                                    inEscape = true;
                                }
                                else
                                {
                                    inText = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // skip carriage returns unless inside text
                        if (inText || c != carrRet)
                        {
                            value += c;
                        }
                    }
                }
            }

            if (value != null)
            {
                values.Add(value);
            }

            if (values != null &&  values.Any())
            {
                return new CsvRow(header, values.ToArray());
            }

            return null;
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}