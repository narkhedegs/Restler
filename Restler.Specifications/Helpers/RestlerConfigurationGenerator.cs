using System.Collections.Generic;
using RestApiTester.Common;

namespace Restler.Specifications.Helpers
{
    public static class RestlerConfigurationGenerator
    {
        public static RestlerConfiguration Default()
        {
            return new RestlerConfiguration
            {
                Interpolater = "${variable}",
                EnvironmentName = "Lab",
                Environments = new List<Environment>
                {
                    new Environment
                    {
                        Name = "Lab",
                        Variables = new Dictionary<string, string>
                        {
                            {"URI","gsnserver:8211/LabWebService"}
                        }
                    }
                }
            };
        }
    }
}
