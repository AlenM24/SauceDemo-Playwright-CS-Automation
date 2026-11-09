using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace SauceDemo.Automation
{
    [TestFixture]
    public class LoginTests : PageTest
    {
        // Constructor keeps Headed mode and your 1.5-second speed control active
        public LoginTests()
        {
            Environment.SetEnvironmentVariable("HEADED", "1");
            Environment.SetEnvironmentVariable("PLAYWRIGHT_SLOW_MO", "1500");
        }

        [Test]
        public async Task SuccessfulLoginTest()
        {
            await Page.GotoAsync("https://saucedemo.com");
            await Page.Locator("#user-name").FillAsync("standard_user");
            await Page.Locator("#password").FillAsync("secret_sauce");
            await Page.ClickAsync("#login-button");

            var currentUrl = Page.Url;
            Assert.That(currentUrl, Does.Contain("inventory.html"));
        }

        [Test]
        public async Task FailedLoginShowsErrorMessageTest()
        {
            await Page.GotoAsync("https://saucedemo.com");
            await Page.Locator("#user-name").FillAsync("standard_user");

            // 1. Enter an intentionally WRONG password
            await Page.Locator("#password").FillAsync("wrong_password");
            await Page.ClickAsync("#login-button");

            // 2. Locate the red error text container that pops up on the screen
            var errorMessageLocator = Page.Locator("data-test=error");

            // 3. Extract the text value inside that element
            string actualErrorMessage = await errorMessageLocator.TextContentAsync();

            // 4. Assert that the error matches the official system warning
            Assert.That(actualErrorMessage, Does.Contain("Username and password do not match any user in this service"));
        }

        [Test]
        public async Task AddItemToShoppingCartTest()
        {
            // 1. Log in cleanly to get to the dashboard
            await Page.GotoAsync("https://saucedemo.com");
            await Page.Locator("#user-name").FillAsync("standard_user");
            await Page.Locator("#password").FillAsync("secret_sauce");
            await Page.ClickAsync("#login-button");

            // 2. Click the 'Add to Cart' button for the first item (Sauce Labs Backpack)
            // This locator points directly to the item's unique button ID
            await Page.ClickAsync("#add-to-cart-sauce-labs-backpack");

            // 3. Locate the shopping cart badge icon in the top right corner
            var cartBadgeLocator = Page.Locator(".shopping_cart_badge");

            // 4. Grab the visible text count inside the badge
            string cartCount = await cartBadgeLocator.TextContentAsync();

            // 5. Verification: Assert that the cart badge text value is exactly "1"
            Assert.That(cartCount, Is.EqualTo("1"), "The cart badge did not update to 1 after adding an item.");
        }
        [Test]
        public async Task BlankFieldsLoginErrorTest()
        {
            await Page.GotoAsync("https://saucedemo.com"); 
            await Page.Locator("#user-name").InputValueAsync();
            await Page.Locator("#password").InputValueAsync();
            await Page.ClickAsync("#login-button");
            var errorMessageLocator = Page.Locator("[data-test='error']");
            string actualErrorMessage = await errorMessageLocator.TextContentAsync();
            Assert.That(actualErrorMessage, Does.Contain("Epic sadface: Username is required"));

        }
    }
}