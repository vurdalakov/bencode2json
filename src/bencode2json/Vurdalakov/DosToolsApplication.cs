namespace Vurdalakov
{
    using System;

    public abstract class DosToolsApplication
    {
        protected CommandLineParser _commandLineParser;

        public DosToolsApplication(String[] args)
        {
            try
            {
                _commandLineParser = new CommandLineParser(args);

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
                var result = Execute();
                Environment.Exit(result);
            }
            catch
            {
                Environment.Exit(1);
            }
        }

        protected abstract Int32 Execute();

        protected virtual void Help()
        {
            Environment.Exit(-1);
        }

        private Boolean _silent;

        protected void Print(String format, params Object[] args)
        {
            if (!_silent)
            {
                Console.WriteLine(String.Format(format, args));
            }
        }
    }
}
