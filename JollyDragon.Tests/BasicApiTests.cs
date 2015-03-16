using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using Microsoft.Owin.Hosting;
using NUnit.Framework;

namespace JollyDragon.Tests
{
    [TestFixture]
    public class BasicApiTests
    {
        private IDisposable _app;
        private HttpClient _client;
        private const string BaseAddress = "http://localhost:9000/";

        [SetUp]
        public void TestSetUp()
        {
            _app = WebApp.Start<Startup>(url: BaseAddress);
            _client = new HttpClient()
            {
                BaseAddress = new Uri(BaseAddress)
            };
        }

        [TearDown]
        public void TearDown()
        {
            if (_app != null)
            {
                _app.Dispose();
            }
        }

        [Test]
        public async void SessionsCanBeCreated()
        {
            _client.PostAsync("session/001/create", null).ShouldSucceed(); 
        }

        [Test]
        public async void SessionsDetailsCanBeRetrieved()
        {
            await _client.PostAsync("session/002/create", null);
            _client.GetAsync("session/002/details").ShouldSucceed();
        }

        [Test]
        public async void PlayersCanBeAdded()
        {
            await _client.PostAsync("session/003/create", null);
            _client.PostAsync("session/003/AddPlayer",
                new
                {
                    Name = "Dinklebot"
                },
                new JsonMediaTypeFormatter()).ShouldSucceed();
        }

        [Test]
        public async void EncountersCanBeCreated()
        {
            await _client.PostAsync("session/004/create", null);
            _client.PostAsync("session/004/encounter/cave/create",
                new{},
                new JsonMediaTypeFormatter()).ShouldSucceed();
        }
    }
}
