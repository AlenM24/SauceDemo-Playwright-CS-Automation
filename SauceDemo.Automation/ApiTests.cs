using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
namespace SauceDemo.Automation
{
    [TestFixture]
    public class ApiTests : PlaywrightTest
    {
        [Test]
        public async Task GetSingleUser_Returns200AndValidName()
        {
            var requestContext = await Playwright.APIRequest.NewContextAsync();
            var response = await requestContext.GetAsync("https://jsonplaceholder.typicode.com/users/1");
            Assert.That(response.Status, Is.EqualTo(200));
            var jsonBody = await response.JsonAsync();
            string userName = jsonBody?.GetProperty("name").GetString() ?? "";
            Assert.That(userName, Is.EqualTo("Leanne Graham"));
        }
        [Test]
        public async Task CreateNewPost_Returns201Created()
        {
            var requestContext = await Playwright.APIRequest.NewContextAsync();
            var payloadData = new Dictionary<string, object>
        {
                { "title", "QA Automation Test" },
                { "body", "Testing POST endpoint via C# Playwright" },
                    { "userId", 1 }
        };
            var response = await requestContext.PostAsync(
            "https://jsonplaceholder.typicode.com/posts",
            new APIRequestContextOptions { DataObject = payloadData }
            );
            Assert.That(response.Status, Is.EqualTo(201));
            var jsonBody = await response.JsonAsync();
            int createdId = jsonBody?.GetProperty("id").GetInt32() ?? 0;
            Assert.That(createdId, Is.EqualTo(101));
        }
    }
}