using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel.Description;

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

        public static IOrganizationService GetOrganizationService(string userName, string password, string orgServiceUri)
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = userName;
                credentials.UserName.Password = password;
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(new Uri(orgServiceUri), null, credentials, null);
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