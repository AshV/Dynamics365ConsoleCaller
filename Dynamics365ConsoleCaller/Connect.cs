using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;

namespace Dynamics365ConsoleCaller
{
    public class Connect
    {
        public static IOrganizationService GetOrganizationService(string UserName, string Password, string SoapOrgServiceUri)
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = UserName;
                credentials.UserName.Password = Password;
                Uri serviceUri = new Uri(SoapOrgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);
                proxy.EnableProxyTypes();
                return (IOrganizationService)proxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
                return null;
            }
        }

        public static OrganizationResponse CallAction(IOrganizationService service, string actionName, EntityReference target, Dictionary<string, object> parameters = null)
        {
            var request = new OrganizationRequest(actionName);

            if (!(parameters == null))
            {
                parameters.ToList().ForEach(kv =>
                {
                    request.Parameters.Add(kv.Key, kv.Value);
                });
            }

            request.Parameters.Add("Target", target);

            var response = service.Execute(request);
            return response;
        }
    }
}