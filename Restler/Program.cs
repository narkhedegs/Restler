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

            Console.WriteLine("Executing Collection - " + beforeCollectionRunEventArgs.Collection.Name);
        }

        private static void CollectionRunnerOnBeforeRequestRun(object sender,
            BeforeRequestRunEventArgs beforeRequestRunEventArgs)
        {
            var beforeRequestRunAddIns = _serviceProvider.GetAll<IBeforeRequestRunAddIn>().ToList();
            if (beforeRequestRunAddIns.Any())
            {
                foreach (var beforeRequestRunAddIn in beforeRequestRunAddIns)
                {
                    var addInConfigurationSection =
                        _restlerConfiguration.AddIns.FirstOrDefault(
                            section => String.Equals(section.Name, beforeRequestRunAddIn.GetType().Name, StringComparison.CurrentCultureIgnoreCase));
                    if (addInConfigurationSection != null)
                    {
                        beforeRequestRunAddIn.Execute(
                                addInConfigurationSection.Configuration,
                                beforeRequestRunEventArgs.Request,
                                beforeRequestRunEventArgs.Environment,
                                beforeRequestRunEventArgs.RestClient);
                    }
                }
            }

            Console.WriteLine("Executing Request - " + beforeRequestRunEventArgs.Request.Url.Path);
        }

        private static void CollectionRunnerOnAfterRequestRun(object sender,
            AfterRequestRunEventArgs afterRequestRunEventArgs)
        {
            var afterRequestRunAddIns = _serviceProvider.GetAll<IAfterRequestRunAddIn>().ToList();
            if (afterRequestRunAddIns.Any())
            {
                foreach (var afterRequestRunAddIn in afterRequestRunAddIns)
                {
                    var addInConfigurationSection =
                        _restlerConfiguration.AddIns.FirstOrDefault(
                            section => String.Equals(section.Name, afterRequestRunAddIn.GetType().Name, StringComparison.CurrentCultureIgnoreCase));
                    if (addInConfigurationSection != null)
                    {
                        afterRequestRunAddIn.Execute(
                                addInConfigurationSection.Configuration,
                                afterRequestRunEventArgs.Response,
                                afterRequestRunEventArgs.Environment,
                                afterRequestRunEventArgs.RestClient);
                    }
                }
            }

            Console.WriteLine("Finished Executing Request - " + afterRequestRunEventArgs.Response.Request.Url.Path + " - With status " + afterRequestRunEventArgs.Response.StatusCode);
        }

        private static void CollectionRunnerOnAfterCollectionRun(object sender,
            AfterCollectionRunEventArgs afterCollectionRunEventArgs)
        {
            var afterCollectionRunAddIns = _serviceProvider.GetAll<IAfterCollectionRunAddIn>().ToList();
            if (afterCollectionRunAddIns.Any())
            {
                foreach (var afterCollectionRunAddIn in afterCollectionRunAddIns)
                {
                    var addInConfigurationSection =
                        _restlerConfiguration.AddIns.FirstOrDefault(
                            section => String.Equals(section.Name, afterCollectionRunAddIn.GetType().Name, StringComparison.CurrentCultureIgnoreCase));
                    if (addInConfigurationSection != null)
                    {
                        afterCollectionRunAddIn.Execute(
                                addInConfigurationSection.Configuration,
                                afterCollectionRunEventArgs.CollectionRunResult,
                                afterCollectionRunEventArgs.Environment,
                                afterCollectionRunEventArgs.RestClient);
                    }
                }
            }

            Console.WriteLine("Finished Executing Collection - " + afterCollectionRunEventArgs.CollectionRunResult.Collection.Name);
        }
    }
}
