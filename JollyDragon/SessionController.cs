using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using JollyDragon.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JollyDragon
{
    [RoutePrefix("session")]
    public class SessionController : ApiController
    {
        private static ConcurrentDictionary<string, Session> _sessions;

        public SessionController(ConcurrentDictionary<string, Session> sessions)
        {
            _sessions = sessions;
        }

        [HttpPost]
        [Route("{sessionName}/Create")]
        public async Task<HttpResponseMessage> Create(string sessionName)
        {
            return _sessions.TryAdd(sessionName, new Session(sessionName))
                ? new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Session created", Encoding.UTF8, "appplication/json") }
                : new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Session {0} already exists", sessionName), Encoding.UTF8, "appplication/json")
                };
        }

        [HttpPost]
        [Route("{sessionName}/AddPlayer")]
        public async Task<HttpResponseMessage> AddPlayer(string sessionName, [FromBody]Player player)
        {
            EnsureSessionExists(sessionName);
          
            _sessions[sessionName].AddPlayer(player);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Player added", Encoding.UTF8, "appplication/json")
            };
        }

        [HttpGet]
        [Route("{sessionName}/Details")]
        public async Task<HttpResponseMessage> Details(string sessionName)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(_sessions[sessionName].Describe()), Encoding.UTF8, "appplication/json"),
            };
        }

        private static void EnsureSessionExists(string sessionName)
        {
           if(!_sessions.ContainsKey(sessionName))
           {
               throw new ArgumentException(string.Format("Session {0} does not exist.", sessionName));
           }
        }

      
    }
}