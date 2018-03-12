using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics365ConsoleCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService orgService = Connect.GetOrganizationService(
                 "<UserID>@<OrganizationName>.onmicrosoft.com",
                 "<Your Crush's Name i.e. Your Password>",
                 "<CRM login URI>");
            Console.WriteLine("Connected to Organization Service!");

            ITracingService tracingService = Connect.GetTracingService("AshV_Log");

            // Write Your Testing Code here.
        }
    }
}