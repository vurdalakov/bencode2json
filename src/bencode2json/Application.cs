namespace Vurdalakov.Bencode
{
    using System;
    using System.IO;

    public class Application : DosToolsApplication
    {
        public Application(String[] args) : base(args)
        {
            if (_commandLineParser.FileNames.Length < 1)
            {
                Help();
            }
        }

        protected override Int32 Execute()
        {
            try
            {
                var fileName1 = _commandLineParser.FileNames[0];
                var fileName2 = 1 == _commandLineParser.FileNames.Length ? Path.ChangeExtension(fileName1, ".json") : _commandLineParser.FileNames[1];

                Print("Converting {0} to {1}", fileName1, fileName2);

                var converter = new BencodeToJsonConverter();
                converter.Convert(fileName1, fileName2);

                return 0;
            }
            catch (Exception ex)
            {
                Print("Error converting file: {0}", ex.Message);
                throw;
            }
        }

        protected override void Help()
        {
            Console.WriteLine("Bencode to JSON converter 1.0 | (c) Vurdalakov | https://github.com/vurdalakov/bencode2json\n");
            Console.WriteLine("Converts a bencode file to a JSON file\n");
            Console.WriteLine("Usage:\n\tbencode2json bencode_file [json_file] [-silent]\n");
            Console.WriteLine("Exit codes:\n\t0 - file conversion succeeded\n\t1 - file conversion failed\n\t-1 - invalid command line syntax\n");
            Environment.Exit(-1);
        }
    }
}
