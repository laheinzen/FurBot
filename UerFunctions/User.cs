using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace UerFunctions
{
    public static class User
    {
        [FunctionName("Username")]
        public static async Task<IActionResult> GetUsernameFromPersonId(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string personId = req.Query["PersonID"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            personId = personId ?? data?.personId;

            string responseMessage = string.IsNullOrEmpty(personId)
                ? "I need the Person ID to return the username."
                : $"Hello there. The username for PersonID {personId} is lah@furb.br";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("PersonID")]
        public static async Task<IActionResult> GetPersonIdFromUsername(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string username = req.Query["Username"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            username = username ?? data?.username;

            string responseMessage = string.IsNullOrEmpty(username)
                ? "I need the username to return the Person ID."
                : $"Hello there. The Person ID for {username} is 9912";

            var myObj = new { name = "thomas", location = "Denver" };
            var jsonToReturn = JsonConvert.SerializeObject(myObj);

        
            return new OkObjectResult(jsonToReturn);

        }



    }
}
