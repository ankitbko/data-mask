using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataMask.CLI.Options
{
    [Verb("text", HelpText="Masks delimited text files.")]
    public class TextVerb
    {
        [Option('d', "delimiter", Required = false, HelpText = "Provide Delimiter. Default is comma (,)")]
        public char Delimiter { get; set; } = ',';

        [Option('i', "input", Required = true, HelpText = "Path to input file.")]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to output file.")]
        public string OutputFilePath { get; set; }

        [Option('c', "column", Separator = ',', Required = true, Min = 1, HelpText ="Comma separated columns number to mask. Starts with 0.")]
        public IEnumerable<int> ColumnsToMask { get; set; }

        public void Validate()
        {
            if(!File.Exists(InputFilePath))
            {
                throw new ArgumentException("Input file does not exist");
            }
            if (OutputFilePath.EndsWith('/') || OutputFilePath.EndsWith('\\'))
            {
                throw new ArgumentException("Output file must be a file path not directory.");
            }
            if (ColumnsToMask.Count() == 0)
            {
                throw new ArgumentException("Column cannot be empty.");
            }
        }
    }
}
