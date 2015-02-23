namespace Vurdalakov.Bencode
{
    using System;
    using System.IO;

    public class Application
    {
        private CommandLineParser _commandLineParser;
        private Boolean _silent;

        public Application(String[] args)
        {
            try
            {
                _commandLineParser = new CommandLineParser(args);

                if (_commandLineParser.FileNames.Length != 1)
                {
                    throw new Exception();
                }

                _silent = _commandLineParser.IsOptionSet("silent");
            }
            catch
            {
                Help();
            }
        }

        public void Run()
        {
            try
            {
                var fileName1 = _commandLineParser.FileNames[0];
                var fileName2 = Path.ChangeExtension(fileName1, ".json");

                Print("Converting {0} to {1}", fileName1, fileName2);

                var converter = new BencodeToJsonConverter();
                converter.Convert(fileName1, fileName2);
            }
            catch (Exception ex)
            {
                Print("Error converting file: {0}", ex.Message);
                Environment.Exit(2);
            }
        }

        private void Print(String format, params Object[] args)
        {
            if (!_silent)
            {
                Console.WriteLine(String.Format(format, args));
            }
        }

        private void Help()
        {
            Console.WriteLine("Bencode to JSON converter 1.0 | (c) Vurdalakov | https://github.com/vurdalakov/bencode2json\n");
            Console.WriteLine("Converts bencode file to a JSON one\n");
            Console.WriteLine("Usage:\n\tbencode2json file [-silent]\n");
            Console.WriteLine("Exit codes:\n\t0 - file converted\n\t-1 - invalid command line syntax\n");
            Environment.Exit(-1);
        }
    }
}
