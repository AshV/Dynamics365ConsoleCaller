This code has been written with only intention to save developer's time while testing Dynamics 365 code which requires connection to CRM organization & **IOrganizationService** and/or **ITracingService** instances.

To usse it just download this project, open in Visual Studio and build to restore nugget packages, open **Program.cs**(below code) and provide credentials to GetOrganizationService method
```csharp
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
        
        // Write your code tobe tested here.
    }
}
```

* To test code snippet just write/paste your code at mentioned place and simply run/debug.

* You can even add references to DLLs in this project and call public methods.
