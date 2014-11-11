using System;
using System.Linq;
using RestApiTester.Common;

namespace Restler
{
    public interface IRestRequestCollectionRunConfigurationBuilder
    {
        RestRequestCollectionRunConfiguration Build(RestlerConfiguration restlerConfiguration);
    }

    public class RestRequestCollectionRunConfigurationBuilder : IRestRequestCollectionRunConfigurationBuilder
    {
        public RestRequestCollectionRunConfiguration Build(RestlerConfiguration restlerConfiguration)
        {
            RestRequestCollectionRunConfiguration collectionRunConfiguration = null;

            if (restlerConfiguration != null)
            {
                collectionRunConfiguration = new RestRequestCollectionRunConfiguration();
                collectionRunConfiguration.Interpolater = restlerConfiguration.Interpolater;
                if (restlerConfiguration.Environments.Count > 0 &&
                                !string.IsNullOrEmpty(restlerConfiguration.EnvironmentName) &&
                                restlerConfiguration.Environments.Any(
                                    environment =>
                                        String.Equals(environment.Name, restlerConfiguration.EnvironmentName,
                                            StringComparison.CurrentCultureIgnoreCase)))
                {
                    collectionRunConfiguration.Environment =
                        restlerConfiguration.Environments.SingleOrDefault(
                            environment => String.Equals(environment.Name, restlerConfiguration.EnvironmentName,
                                StringComparison.CurrentCultureIgnoreCase));
                }
            }

            return collectionRunConfiguration;
        }
    }
}
