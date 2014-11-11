using Moq;

namespace Restler.Specifications.Helpers
{
    public static class CommandLineOptionsGenerator
    {
        public static Options Default()
        {
            return new Options(new Mock<IAdditionalHelpTextBuilder>().Object);
        }

        public static Options WithCollectionFilePath(this Options commandLineOptions, string collectionFilePath)
        {
            commandLineOptions.CollectionFilePath = collectionFilePath;
            return commandLineOptions;
        }

        public static Options WithParserName(this Options commandLineOptions, string parserName)
        {
            commandLineOptions.ParserName = parserName;
            return commandLineOptions;
        }

        public static Options WithEnvironmentName(this Options commandLineOptions, string environmentName)
        {
            commandLineOptions.EnvironmentName = environmentName;
            return commandLineOptions;
        }

        public static Options WithAValidConfigurationFile(this Options commandLineOptions)
        {
            commandLineOptions.ConfigurationFilePath = @"C:\configuration.json";
            return commandLineOptions;
        }
    }
}
