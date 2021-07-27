using System;
using System.IO;

namespace CSVHashing
{
    internal class CsvReader
    {
        private readonly StreamReader _reader;

        public CsvReader(StreamReader reader)
        {
            _reader = reader;
        }

        public string? ReadNextLineAtColumn(int columnIndex)
        {
            string? line = ReadNextLine();
            if (line is null)
            {
                return null;
            }

            // Another possible solution could be manually locating the start of the column by counting the
            // number of commas we see, then read until the next comma or until the end of line.
            var columns = line.Split(',');
            if (columnIndex >= columns.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex),
                    $"Cannot read the data at index {columnIndex}. Number of columns in the line is {columns.Length}.");
            }

            return columns[columnIndex];
        }

        public string? ReadNextLine()
            => _reader.ReadLine();
    }
}
