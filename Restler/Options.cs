using CommandLine;
using CommandLine.Text;

namespace Restler
{
    public class Options
    {
        private readonly IAdditionalHelpTextBuilder _additionalHelpTextBuilder;

        public Options(IAdditionalHelpTextBuilder additionalHelpTextBuilder)
        {
            _additionalHelpTextBuilder = additionalHelpTextBuilder;
        }

        [Option('c', "collection", HelpText = "Specify path to REST Request Collection file.")]
        public string CollectionFilePath { get; set; }

        [Option("parser", HelpText = "Specify parser for parsing collection file.")]
        public string ParserName { get; set; }

        [Option("configuration", HelpText = "Specify configuration as a JSON file.")]
        public string ConfigurationFilePath { get; set; }

        [Option('e', "environment", HelpText = "Specify one of the environments in your configuration.")]
        public string EnvironmentName { get; set; }

        [HelpOption('h', "help")]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("\nRestler", "0.0.0"),
                Copyright = new CopyrightInfo("Gaurav Narkhede <narkhedegs@gmail.com>", 2014),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };

            help.AddPreOptionsLine(_additionalHelpTextBuilder.BuildDescription());

            help.AddPreOptionsLine("\n");
            help.AddPreOptionsLine("Options:");
            help.AddOptions(this);

            help.AddPostOptionsLine(_additionalHelpTextBuilder.BuildUsage());
            help.AddPostOptionsLine(_additionalHelpTextBuilder.BuildParserList());
            help.AddPostOptionsLine(_additionalHelpTextBuilder.BuildRunnerList());
            help.AddPostOptionsLine(_additionalHelpTextBuilder.BuildAddInList());

            return help;
        }
    }
}
