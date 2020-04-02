using DataMask.CLI.Algorithm;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMask.CLI.Handler
{
    public class DelimiterTextHandler
    {
        private readonly IMaskAlgorithm algorithm;

        public DelimiterTextHandler(IMaskAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public async Task Mask(
            string inputFilePath,
            string outputFilePath,
            IEnumerable<int> columnIndexToMask,
            char delimiter = ',',
            bool isFirstRowHeader = true,
            Func<IEnumerable<string>, bool> skipRow = null)
        {
            TextFieldParser parser = null;
            StreamWriter outputFile = null;
            try
            {
                parser = ReadFileWithParser(inputFilePath, delimiter);
                outputFile = OpenFile(outputFilePath);

                while (!parser.EndOfData)
                {
                    if (isFirstRowHeader && parser.LineNumber == 1)
                    {
                        var header = parser.ReadFields();
                        await WriteOutputFile(header, delimiter, outputFile);
                    }

                    var tokens = parser.ReadFields();

                    if (skipRow?.Invoke(tokens) == true)
                        continue;

                    foreach (var col in columnIndexToMask)
                    {
                        var token = tokens.ElementAt(col);
                        var masked = algorithm.Mask(token);
                        tokens.SetValue(masked, col);
                    }
                    await WriteOutputFile(tokens, delimiter, outputFile);
                }
            }
            finally
            {
                if (parser != null)
                {
                    parser.Close();
                }
                if (outputFile != null)
                {
                    await outputFile.DisposeAsync();
                }
            }
        }

        private TextFieldParser ReadFileWithParser(string filePath, char delimiter)
        {
            var parser = new TextFieldParser(filePath);
            parser.SetDelimiters(new string[] { delimiter.ToString() });
            parser.HasFieldsEnclosedInQuotes = true;
            return parser;
        }

        private StreamWriter OpenFile(string path)
        {
            var dir = Path.GetDirectoryName(path);
            Directory.CreateDirectory(dir);

            return new StreamWriter(path);
        }

        private async Task WriteOutputFile(IEnumerable<string> tokens, char delimiter, StreamWriter file)
        {
            var line = string.Join(delimiter, tokens);
            await file.WriteLineAsync(line);
        }

    }
}
