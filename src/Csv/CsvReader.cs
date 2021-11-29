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
        private const char space = (char)0x20;

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
            // control booleans
            bool inText = false;
            bool inEscape = false;
            bool peek = false;

            var values = new List<CsvValue>();
            string? value = null;
            char c;
            int i;

            while ((i = reader.Read()) != -1)
            {
                c = (char)i;

                // Peek indicates the last char was a delimeter and we were in a text field
                if (peek)
                {
                    // Was the last delimiter an end of text field or an escape?
                    if (c == textDelimiter)
                    {
                        value += c;
                        inEscape = true;
                    }
                    else
                    {
                        inText = false;
                    }

                    peek = false;
                }

                // End of row?
                if (!inText && c == lineFeed)
                {
                    values.Add(value ?? string.Empty);

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
                        if (inText)
                        {
                            if (inEscape)
                            {
                                inEscape = false;
                            }
                            else
                            {
                                // We need to know the next character to decide whether it is the end of text or an escaped delimiter
                                peek = true;
                            }
                        }
                        else
                        {
                            inText = true;
                        }
                    }
                    else
                    {
                        // skip carriage returns and spaces unless inside text
                        if (inText || ((c != carrRet) && (value != null || c != space)))
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