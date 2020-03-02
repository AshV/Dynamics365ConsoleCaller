using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Dynamics365ConsoleCaller
{
    public class Connect
    {
        static bool isFileTraceLogOn = false;
        static string filePath = "";

        static Connect()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        public static IOrganizationService GetOrganizationServiceClientSecret(string clientId, string clientSecret, string organizationUri)
        {
            try
            {
                var conn = new CrmServiceClient($@"AuthType=ClientSecret;url={organizationUri};ClientId={clientId};ClientSecret={clientSecret}");

                return conn.OrganizationWebProxyClient != null ? conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
                return null;
            }
        }

        public static IOrganizationService GetOrganizationService(string userName, string password, string loginUri)
        {
            try
            {
                var conn = new CrmServiceClient($@"Url={loginUri}; Username={userName}; Password={password}; authtype=Office365");

                // Cast the proxy client to the IOrganizationService interface.
                return conn.OrganizationWebProxyClient != null ? conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
                Console.ReadKey();
                return null;
            }
        }

        public static ITracingService GetTracingService()
        {
            return new FakeTracingService();
        }

        public static ITracingService GetTracingService(string fileToWriteTracelogs)
        {
            isFileTraceLogOn = true;
            filePath = Path.GetFullPath(fileToWriteTracelogs + ".txt");
            return new FakeTracingService(filePath);
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

        static void OnProcessExit(object sender, EventArgs e)
        {
            if (isFileTraceLogOn)
            {
                Console.WriteLine($"Your tracing logs has been written to {filePath}");
                Process.Start(filePath);
            }
        }
    }
}