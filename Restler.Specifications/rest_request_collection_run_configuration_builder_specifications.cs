using NSpec;
using RestApiTester.Common;
using Restler.Specifications.Helpers;

namespace Restler.Specifications
{
    public class rest_request_collection_run_configuration_builder_specifications : nspec
    {
        private IRestRequestCollectionRunConfigurationBuilder _collectionRunConfigurationBuilder;
        private RestRequestCollectionRunConfiguration _collectionRunConfiguration;
        private RestlerConfiguration _restlerConfiguration;

        public void when_building_rest_request_collection_run_configuration()
        {
            before = () =>
            {
                _collectionRunConfigurationBuilder = new RestRequestCollectionRunConfigurationBuilder();
            };

            act = () => _collectionRunConfiguration = _collectionRunConfigurationBuilder.Build(_restlerConfiguration);

            context["given valid RestlerConfiguration"] = () =>
            {
                var validRestlerConfiguration = RestlerConfigurationGenerator.Default();

                before = () => _restlerConfiguration = validRestlerConfiguration;

                it["should populate RestRequestCollectionRunConfiguration from RestlerConfiguration"] = () =>
                {
                    _collectionRunConfiguration.Interpolater.should_be(validRestlerConfiguration.Interpolater);
                    _collectionRunConfiguration.Environment.Name.should_be(
                        validRestlerConfiguration.Environments[0].Name);
                    _collectionRunConfiguration.Environment.Variables.should_be(
                        validRestlerConfiguration.Environments[0].Variables);
                };
            };

            context["given null RestlerConfiguration"] = () =>
            {
                before = () => _restlerConfiguration = null;

                it["should return null RestRequestCollectionRunConfiguration"] =
                    () => _collectionRunConfiguration.should_be_null();
            };
        }
    }
}
