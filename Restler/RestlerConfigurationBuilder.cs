using System;
using System.IO.Abstractions;
using Newtonsoft.Json;

namespace Restler
{
    public interface IRestlerConfigurationBuilder
    {
        RestlerConfiguration Build(Options commandLineOptions);
    }

    public class RestlerConfigurationBuilder : IRestlerConfigurationBuilder
    {
        private readonly IFileSystem _fileSystem;

        public RestlerConfigurationBuilder(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public RestlerConfiguration Build(Options commandLineOptions)
        {
            var restlerConfiguration = new RestlerConfiguration();

            if(commandLineOptions == null) throw new ArgumentNullException("commandLineOptions");

            if (!string.IsNullOrEmpty(commandLineOptions.ConfigurationFilePath) &&
                _fileSystem.File.Exists(commandLineOptions.ConfigurationFilePath))
            {
                restlerConfiguration =
                    JsonConvert.DeserializeObject<RestlerConfiguration>(
                        _fileSystem.File.ReadAllText(commandLineOptions.ConfigurationFilePath));
            }

            if (!string.IsNullOrEmpty(commandLineOptions.CollectionFilePath))
                restlerConfiguration.CollectionFilePath = commandLineOptions.CollectionFilePath;

            if (!string.IsNullOrEmpty(commandLineOptions.ParserName))
                restlerConfiguration.ParserName = commandLineOptions.ParserName;            
            
            if (!string.IsNullOrEmpty(commandLineOptions.EnvironmentName))
                restlerConfiguration.EnvironmentName = commandLineOptions.EnvironmentName;

            return restlerConfiguration;
        }
    }
}
