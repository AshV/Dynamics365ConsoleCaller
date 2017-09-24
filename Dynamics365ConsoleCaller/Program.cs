using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics365ConsoleCaller
{
    class Program
    { 
        static void Main(string[] args)
        {
            //IOrganizationService orgService = Connect.GetOrganizationService(
            //    "<UserID>@<OrganizationName>.onmicrosoft.com",
            //    "<Your Crush's Name i.e. Your Password>",
            //    "https://<OrganizationName>.api.<instance region>.dynamics.com/XRMServices/2011/Organization.svc");
            //Console.WriteLine("Connected to Organization Service!");

            ITracingService tracingService = Connect.GetTracingService("File");

            tracingService.Trace("चित्रकूट");
        }
    }
}