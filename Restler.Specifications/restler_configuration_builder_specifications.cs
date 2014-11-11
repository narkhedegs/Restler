using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using NSpec;
using Restler.Specifications.Helpers;

namespace Restler.Specifications
{
    public class restler_configuration_builder_specifications : nspec
    {
        private IRestlerConfigurationBuilder _restlerConfigurationBuilder;
        private RestlerConfiguration _restlerConfiguration;
        private Options _commandLineOptions;

        public void when_building_restler_configuration()
        {
            before = () =>
            {
                var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
                {
                    {
                        @"C:\configuration.json",
                        new MockFileData("{\"collection\":\"collection.json\",\"parser\":\"DevHttpClientRepositoryParser\",\"environment\":\"Lab\",\"interpolater\":\"${variable}\",\"environments\":[{\"name\":\"Lab\",\"variables\":{\"URI\":\"gsnserver:8211/LabWebService\"}}],\"addIns\":[{\"name\":\"BasicAuthenticationAddIn\",\"configuration\":{\"username\":\"narkhedeg@upmc.edu\",\"password\":\"1GoodPassword!\"}}]}")
                    }
                });

                _restlerConfigurationBuilder = new RestlerConfigurationBuilder(fileSystem);
            };

            act = () => _restlerConfiguration = _restlerConfigurationBuilder.Build(_commandLineOptions);

            context["given a valid configuration file path"] = () =>
            {
                before = () =>
                {
                    _commandLineOptions = CommandLineOptionsGenerator.Default().WithAValidConfigurationFile();
                };

                it["should build RestlerConfiguration from configuration file"] = () =>
                {
                    _restlerConfiguration.CollectionFilePath.should_be("collection.json");
                    _restlerConfiguration.ParserName.should_be("DevHttpClientRepositoryParser");
                    _restlerConfiguration.EnvironmentName.should_be("Lab");
                    _restlerConfiguration.Interpolater.should_be("${variable}");

                    _restlerConfiguration.Environments.Count.should_be(1);
                    _restlerConfiguration.Environments[0].Name.should_be("Lab");
                    _restlerConfiguration.Environments[0].Variables.should_be(new Dictionary<string, string> { { "URI", "gsnserver:8211/LabWebService" } });

                    _restlerConfiguration.AddIns.Count.should_be(1);
                    _restlerConfiguration.AddIns[0].Name.should_be("BasicAuthenticationAddIn");
                    _restlerConfiguration.AddIns[0].Configuration.should_be("{\"username\":\"narkhedeg@upmc.edu\",\"password\":\"1GoodPassword!\"}");
                };
            };

            context["given valid command line options"] = () =>
            {
                const string expectedCollectionFilePath = @"C:\collection2.json";
                const string expectedParserName = "PostmanCollectionParser";
                const string expectedEnvironmentName = "QA";

                before =
                    () =>
                        _commandLineOptions =
                            CommandLineOptionsGenerator.Default()
                                .WithCollectionFilePath(expectedCollectionFilePath)
                                .WithParserName(expectedParserName)
                                .WithEnvironmentName(expectedEnvironmentName);

                it["should override RestlerConfiguration with available command line options"] = () =>
                {
                    _restlerConfiguration.CollectionFilePath.should_be(expectedCollectionFilePath);
                    _restlerConfiguration.ParserName.should_be(expectedParserName);
                    _restlerConfiguration.EnvironmentName.should_be(expectedEnvironmentName);
                };
            };

            context["given null command line options"] = () =>
            {
                before = () => _commandLineOptions = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };
        }
    }
}
