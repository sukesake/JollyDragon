using System.Collections.Generic;

namespace JollyDragon.Domain
{
    public class Session
    {
        private readonly string _sessionName;
        private readonly List<Player> _players = new List<Player>();
        private readonly List<Encounter> _encounters = new List<Encounter>();

        public Session(string sessionName)
        {
            _sessionName = sessionName;
        }

        public Session()
        {
        }

        public string Describe()
        {
            return string.Format("[Session:{0}][Players:{1}]", _sessionName, 0);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }

        public void CreateEncounter(Encounter encounter)
        {
            _encounters.Add(encounter);
        }
    }
}