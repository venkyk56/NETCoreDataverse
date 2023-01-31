using System.Net;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace DataverseConsoleApp
{
    class program
    {
        const string clientId = "<ClientID>",
                         clientSecret = "<ClientSecret>",
                         environment = "<Environment>";
        static void Main(string[] args)
        {
            var accId = CreateAccount();
            Console.WriteLine(accId);
            var account = GetCRMAccount(accId);
            Console.WriteLine($"{account.GetAttributeValue<string>("name")}, {account.Id}");
        }
        /// <summary>
        /// Method to fetch an accountusing accountid.
        /// </summary>
        static Entity GetCRMAccount(Guid accountId)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var connectionString = @$"Url={environment};AuthType=ClientSecret;ClientId={clientId};ClientSecret={clientSecret};RequireNewInstance=true";
            using (var serviceClient = new ServiceClient(connectionString))
            {
                var account = serviceClient.Retrieve("account", accountId, new ColumnSet("name"));
                return account;
            }
        }
        /// <summary>
        /// Method to create an account in Dataverse.
        /// </summary>
        static Guid CreateAccount()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            var connectionString = @$"Url={environment};AuthType=ClientSecret;ClientId={clientId};ClientSecret={clientSecret};RequireNewInstance=true";

            using (var serviceClient = new ServiceClient(connectionString))
            {
                Entity recordToCreate = new Entity("account");//Your table schema name.
                recordToCreate["name"] = "Test account" + (new Random()).Next(); // schema name of the column and value.

                var recordId = serviceClient.Create(recordToCreate);                
                return recordId;
            }
        }
    }

}