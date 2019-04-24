using System.Configuration;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace STH.BiometricIdentityService.Api
{
    public class Program
    {
        static void Main(string[] args)
        {

            
            string baseAddress = ConfigurationManager.AppSettings["General.BaseAddress"];

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                // run a test at startup 
                var response = client.GetAsync(baseAddress + "api/bir/get").Result;

                System.Console.WriteLine(response);
                System.Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                System.Console.ReadLine();
            }
        }
    }
}
