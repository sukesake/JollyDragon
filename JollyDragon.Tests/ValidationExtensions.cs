using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace JollyDragon.Tests
{
    public static class ValidationExtensions
    {
        public static async void ShouldSucceed(this Task<HttpResponseMessage> task)
        {
            var response = await task;
            try
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            catch (AssertionException e)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                throw;
            }
        }

        public static void ShouldSucceed(this HttpResponseMessage response)
        {
            try
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
            catch (AssertionException e)
            {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                throw;
            }
        }
    }
}