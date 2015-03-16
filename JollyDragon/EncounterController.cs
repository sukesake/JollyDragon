using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using JollyDragon.Domain;
using Newtonsoft.Json;

namespace JollyDragon
{
    [RoutePrefix("session/{sessionName}/encounter")]
    public class EncounterController : ApiController
    {
        private static ConcurrentDictionary<string, Session> _sessions;

        public EncounterController(ConcurrentDictionary<string, Session> sessions)
        {
            _sessions = sessions;
        }

        [HttpPost]
        [Route("{encounterName}/Create")]
        public async Task<HttpResponseMessage> Create(string sessionName, string encounterName)
        {
            EnsureSessionExists(sessionName);

            _sessions[sessionName].CreateEncounter(new Encounter() { Name = encounterName });

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Encounter created", Encoding.UTF8, "appplication/json")
            };
        }

        [HttpPost]
        [Route("{encounterName}/AddNpc")]
        public async Task<HttpResponseMessage> Create(string sessionName, string encounterName, Npc npc)
        {
            throw new NotImplementedException();
        }

        private static void EnsureSessionExists(string sessionName)
        {
            if (!_sessions.ContainsKey(sessionName))
            {
                throw new ArgumentException(string.Format("Session {0} does not exist.", sessionName));
            }
        }


    }
}