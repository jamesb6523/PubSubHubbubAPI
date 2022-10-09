using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using PubSubHubbubAPI.Data;
using PubSubHubbubAPI.Data.Models;

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
            YoutubeEventResponse youtubeEvent = new YoutubeEventResponse();

            bodycontents = new StreamReader(Request.Body).ReadToEnd();

            if (!string.IsNullOrEmpty(bodycontents))
            {
                //var serializer = new XmlSerializer(typeof(YoutubeEventResponse.feed));

                //using (StringReader reader = new StringReader(bodycontents))
                //{
                //    var res = (YoutubeEventResponse.feed)serializer.Deserialize(reader);
                //}
                MariaDB.InsertToMariaDB(bodycontents);
            }
        }
    }
}
