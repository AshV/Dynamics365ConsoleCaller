using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics365ConsoleCaller
{
    class Program // BUILD BEFORE RUN TO RESTORE NuGet PACKAGES
    {
        static void Main(string[] args)
        {
            IOrganizationService orgService;
            // Option 1 : Connect using Client Secret
            orgService = Connect.GetOrganizationServiceClientSecret(
                 "<Client Id obtained from Azure AD App>",
                 "<Client Secret obtained from Azure AD App>",
                 "<Organization Uri>");
            /*
            // Option 2 : Connect using Password
            IOrganizationService orgService = Connect.GetOrganizationService(
                 "<UserID>@<OrganizationName>.onmicrosoft.com",
                 "<Your Crush's Name i.e. Your Password>",
                 "<CRM login URI>");
            */
            Console.WriteLine("Connected to Organization Service!");

            ITracingService tracingService = Connect.GetTracingService("AshV_Log");

            // Write Your Testing Code here.

            var response = orgService.Execute(new WhoAmIRequest());
        }
    }
}