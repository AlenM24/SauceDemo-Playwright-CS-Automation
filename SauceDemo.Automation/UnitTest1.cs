using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SauceDemo.Automation.Pages; // <--- Links to the newly made pages folder!
using System;
using System.Threading.Tasks;

namespace SauceDemo.Automation
{
    [TestFixture]
    public class LoginTests : PageTest
    {
        public LoginTests()
        {
            Environment.SetEnvironmentVariable("HEADED", "1");
            Environment.SetEnvironmentVariable("PLAYWRIGHT_SLOW_MO", "1500");
        }

        [Test]
        //Instantiates LoginPge object with live browser 'page'.
        public async Task SuccessfulLoginTest()
        {
            var loginPage = new LoginPage(Page);

            await loginPage.LoadAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            Assert.That(Page.Url, Does.Contain("inventory.html"));
        }

        [Test]
        public async Task FailedLoginShowsErrorMessageTest()
        {
            var loginPage = new LoginPage(Page);

            await loginPage.LoadAsync();
            await loginPage.LoginAsync("standard_user", "wrong_password");

            string errorMessage = await loginPage.GetErrorMessageAsync();
            Assert.That(errorMessage, Does.Contain("Username and password do not match any user in this service"));
        }

        [Test]
        public async Task AddItemToShoppingCartTest()
        {
            var loginPage = new LoginPage(Page);
            var inventoryPage = new InventoryPage(Page);

            await loginPage.LoadAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            // Multi-step journey transitions smoothly to the dashboard page object
            await inventoryPage.AddBackpackToCartAsync();

            string cartCount = await inventoryPage.GetCartBadgeCountAsync();
            Assert.That(cartCount, Is.EqualTo("1"), "The cart badge did not update to 1 after adding an item.");
        }

        [Test]
        public async Task BlankFieldsLoginErrorTest()
        {
            var loginPage = new LoginPage(Page);

            await loginPage.LoadAsync();
            // Passing empty strings to trigger boundary validations
            await loginPage.LoginAsync("", "");

            string? errorMessage = await loginPage.GetErrorMessageAsync();
            Assert.That(errorMessage, Does.Contain("Epic sadface: Username is required"));
        }
    }
}