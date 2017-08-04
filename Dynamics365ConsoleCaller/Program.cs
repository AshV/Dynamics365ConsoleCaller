using Microsoft.Xrm.Sdk;
using System;

namespace Dynamics365ConsoleCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrganizationService service = Connect.GetOrganizationService("UID", "PWD", "https://org.api.crm.dynamics.com/XRMServices/2011/Organization.svc");
            Console.WriteLine("Connected to Organization Service!");
        }
    }
}