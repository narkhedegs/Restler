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

        [Option("collection", HelpText = "Specify path to REST Request Collection file.", MutuallyExclusiveSet = "Main")]
        public string CollectionFilePath { get; set; }

        [Option("parser", HelpText = "Specify parser for parsing collection file.", MutuallyExclusiveSet = "Main")]
        public string ParserName { get; set; }

        [Option("configuration", HelpText = "Specify configuration as a JSON file.", MutuallyExclusiveSet = "Main")]
        public string ConfigurationFilePath { get; set; }

        [Option("environment", HelpText = "Specify one of the environments in your configuration.", MutuallyExclusiveSet = "Main")]
        public string EnvironmentName { get; set; }

        [Option("installAddIn", HelpText = "Specify name of the AddIn to be installed.", MutuallyExclusiveSet = "InstallAddInCommand")]
        public string AddInToBeInstalled { get; set; }

        [HelpOption("help", HelpText = "Displays this help screen.")]
        public string GetUsage()
        {
            var help = new HelpText
            {
                Heading = new HeadingInfo("\nRestler", "0.1.0"),
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
