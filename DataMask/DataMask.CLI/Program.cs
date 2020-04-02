using CommandLine;
using DataMask.CLI.Algorithm;
using DataMask.CLI.Handler;
using DataMask.CLI.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataMask.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<TextVerb>(args)
                .MapResult(
                (TextVerb textVerb) => WithTextVerbParsed(textVerb),
                (errors) => OnFailure(errors));
            //await (new DelimiterTextHandler(new Sha256MaskAlgorithm())).Mask("./a.csv", "./ab/b.csv", new[] { 1, 2 });
        }

        public static async Task WithTextVerbParsed(TextVerb textVerb)
        {
            textVerb.Validate();
            var handler = new DelimiterTextHandler(new Sha256MaskAlgorithm());
            await handler.Mask(textVerb.InputFilePath, textVerb.OutputFilePath, textVerb.ColumnsToMask, textVerb.Delimiter);
        }

        private static async Task OnFailure(IEnumerable<Error> errors)
        {
            if (errors.IsHelp() || errors.IsVersion())
            {
                return;
            }

            foreach (var error in errors)
            {
                Console.WriteLine(error.ToString());
            }
        }
    }
}
