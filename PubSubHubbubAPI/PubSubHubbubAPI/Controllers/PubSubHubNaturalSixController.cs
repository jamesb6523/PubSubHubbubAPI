using Microsoft.AspNetCore.Mvc;
using PubSubHubbubAPI.Data;

namespace PubSubHubbubAPI.Controllers
{
    public class PubSubHubNaturalSixController : Controller
    {
        [HttpGet]
        [Route("api/PubSubHubbub/NaturalSix")]
        public string Get([FromQuery(Name = "hub.challenge")] string challenge)
        {
            return challenge;
        }

        [HttpPost]
        [Route("api/PubSubHubbub/NaturalSix")]
        public void Post()
        {
            string bodycontents = new StreamReader(Request.Body).ReadToEnd();
            if (!string.IsNullOrEmpty(bodycontents))
            {
                //MariaDB.InsertToMariaDB(bodycontents);
                MongoDBOperations.InsertToMongoDB(bodycontents);
            }
        }
    }
}
