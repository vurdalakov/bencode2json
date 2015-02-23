namespace Vurdalakov
{
    using System;
    using System.Collections.Generic;

    // in any order:
    // filename -option_without_param -option_with_param= option_param

    public class CommandLineParser
    {
        public String[] FileNames { get; private set; }

        public CommandLineParser(String[] args, Boolean caseInsensitive = true)
        {
            _options = new Dictionary<String, String>(caseInsensitive ? StringComparer.InvariantCultureIgnoreCase : null);

            var fileNames = new List<String>();

            var optionName = "";

            foreach (var arg in args)
            {
                var optionParameterExpected = !String.IsNullOrEmpty(optionName);

                if (arg.StartsWith("/") || arg.StartsWith("-"))
                {
                    if (optionParameterExpected)
                    {
                        throw new ArgumentException("Expected option parameter");
                    }

                    if (arg.EndsWith("="))
                    {
                        optionName = arg.Substring(1, arg.Length - 2);
                    }
                    else
                    {
                        _options.Add(arg.Substring(1, arg.Length - 1), "");
                    }
                }
                else if (optionParameterExpected)
                {
                    _options.Add(optionName, arg);
                    optionName = "";
                }
                else
                {
                    fileNames.Add(arg);
                }
            }

            if (!String.IsNullOrEmpty(optionName))
            {
                throw new ArgumentException("Expected option parameter");
            }

            FileNames = fileNames.ToArray();
        }

        private Dictionary<String, String> _options;

        public Boolean IsOptionSet(String optionName)
        {
            return _options.ContainsKey(optionName);
        }

        public String GetOptionParameter(String optionName)
        {
            return _options[optionName];
        }
    }
}
