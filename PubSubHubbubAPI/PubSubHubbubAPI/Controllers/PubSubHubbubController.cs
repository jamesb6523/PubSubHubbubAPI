using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PubSubHubbubAPI.Data;

namespace PubSubHubbubAPI.Controllers
{
    public class PubSubHubbubController : Controller
    {
        [HttpGet]
        [Route("api/PubSubHubbub")]
        public string Get([FromQuery(Name = "hub.challenge")] string challenge)
        {
            return challenge;
        }

        [HttpPost]
        [Route("api/PubSubHubbub")]
        public void Post()
        {
            string bodycontents = string.Empty;

            bodycontents = new StreamReader(Request.Body).ReadToEnd();

            if (!string.IsNullOrEmpty(bodycontents))
            {
                MariaDB.InsertToMariaDB(bodycontents);
            }
        }
    }
}
