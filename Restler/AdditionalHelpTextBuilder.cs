using System.Linq;
using RestApiTester.Common;

namespace Restler
{
    public interface IAdditionalHelpTextBuilder
    {
        string BuildDescription();
        string BuildUsage();
        string BuildParserList();
        string BuildRunnerList();
        string BuildAddInList();
    }

    public class AdditionalHelpTextBuilder : IAdditionalHelpTextBuilder
    {
        private readonly IServiceProvider _serviceProvider;

        public AdditionalHelpTextBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string BuildDescription()
        {
            return "\n" +
                   "Description:\n" +
                   "Restler is a command-line collection runner for applications like DevHttpClient and Postman. It allows you to effortlessly run and test a rest request collection directly from the command-line. It is built with extensibility in mind so that you can easily integrate it with your continuous integration servers and build systems.";
        }

        public string BuildUsage()
        {
            return "Usage:\n" +
                   "restler -c collection.json --parser DevHttpClientRepositoryParser";
        }

        public string BuildParserList()
        {
            var parsers = _serviceProvider.GetAll<IRestRequestCollectionParser>();

            return "\n" +
                   "Available Parsers:\n" +
                   string.Join("\n", parsers.Select(parser => parser.GetType().Name));
        }

        public string BuildRunnerList()
        {
            var runners = _serviceProvider.GetAll<IRestRequestCollectionRunner>();

            return "\n" +
                   "Available Runners:\n" +
                   string.Join("\n", runners.Select(runner => runner.GetType().Name));
        }

        public string BuildAddInList()
        {
            var addIns = 
                (from beforeCollectionRunAddIn in _serviceProvider.GetAll<IBeforeCollectionRunAddIn>()
                 select beforeCollectionRunAddIn.GetType().Name).Union(
                    from beforeRequestRunAddIn in _serviceProvider.GetAll<IBeforeRequestRunAddIn>()
                    select beforeRequestRunAddIn.GetType().Name).Union(
                        from afterRequestRunAddIn in _serviceProvider.GetAll<IAfterRequestRunAddIn>()
                        select afterRequestRunAddIn.GetType().Name).Union(
                            from afterCollectionRunAddIn in _serviceProvider.GetAll<IAfterCollectionRunAddIn>()
                            select afterCollectionRunAddIn.GetType().Name);

            return "\n" +
                   "Available Add Ins:\n" +
                   string.Join("\n", addIns);
        }
    }
}
