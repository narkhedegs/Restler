using System;
using System.IO;
using System.Linq;
using RestApiTester.Common;

namespace Restler
{
    internal class Program
    {
        private static ServiceProvider _serviceProvider;
        private static RestlerConfiguration _restlerConfiguration;

        private static void Main(string[] args)
        {
            _serviceProvider = new ServiceProvider();

            var additionalHelpTextBuilder = new AdditionalHelpTextBuilder(_serviceProvider);
            var restlerConfigurationBuilder = _serviceProvider.Get<IRestlerConfigurationBuilder>();
            var collectionRunConfigurationBuilder = _serviceProvider.Get<IRestRequestCollectionRunConfigurationBuilder>();

            var options = new Options(additionalHelpTextBuilder);
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                _restlerConfiguration = restlerConfigurationBuilder.Build(options);

                var collectionParser =
                    _serviceProvider.GetAll<IRestRequestCollectionParser>()
                        .SingleOrDefault(
                            parser =>
                                String.Equals(parser.GetType().Name, _restlerConfiguration.ParserName,
                                    StringComparison.CurrentCultureIgnoreCase));

                var collectionRunner = _serviceProvider.Get<IRestRequestCollectionRunner>();

                if (collectionParser != null && collectionRunner != null)
                {
                    var collection = collectionParser.Parse(File.ReadAllText(options.CollectionFilePath));

                    collectionRunner.BeforeCollectionRun += CollectionRunnerOnBeforeCollectionRun;
                    collectionRunner.BeforeRequestRun += CollectionRunnerOnBeforeRequestRun;
                    collectionRunner.AfterRequestRun += CollectionRunnerOnAfterRequestRun;
                    collectionRunner.AfterCollectionRun += CollectionRunnerOnAfterCollectionRun;

                    var collectionRunConfiguration = collectionRunConfigurationBuilder.Build(_restlerConfiguration);

                    collectionRunner.Run(collection, collectionRunConfiguration);
                }
            }
        }

        private static void CollectionRunnerOnBeforeCollectionRun(object sender,
            BeforeCollectionRunEventArgs beforeCollectionRunEventArgs)
        {
            var beforeCollectionRunAddIns = _serviceProvider.GetAll<IBeforeCollectionRunAddIn>().ToList();
            if (beforeCollectionRunAddIns.Any())
            {
                foreach (var beforeCollectionRunAddIn in beforeCollectionRunAddIns)
                {
                    var addInConfigurationSection =
                        _restlerConfiguration.AddIns.FirstOrDefault(
                            section => String.Equals(section.Name, beforeCollectionRunAddIn.GetType().Name, StringComparison.CurrentCultureIgnoreCase));
                    if (addInConfigurationSection != null)
                    {
                        beforeCollectionRunAddIn.Execute(
                                addInConfigurationSection.Configuration,
                                beforeCollectionRunEventArgs.Collection,
                                beforeCollectionRunEventArgs.Environment,
                                beforeCollectionRunEventArgs.RestClient);
                    }
                }
            }
        }

        private static void CollectionRunnerOnBeforeRequestRun(object sender,
            BeforeRequestRunEventArgs beforeRequestRunEventArgs)
        {
            Console.WriteLine(beforeRequestRunEventArgs.Request.Url.Path);
        }

        private static void CollectionRunnerOnAfterRequestRun(object sender,
            AfterRequestRunEventArgs afterRequestRunEventArgs)
        {
        }

        private static void CollectionRunnerOnAfterCollectionRun(object sender,
            AfterCollectionRunEventArgs afterCollectionRunEventArgs)
        {
        }
    }
}
