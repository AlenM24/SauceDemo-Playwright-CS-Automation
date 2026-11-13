using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using SauceDemo.Automation.Pages;
using System;
using System.Threading.Tasks;

namespace SauceDemo.Automation
{
    [TestFixture]//Marks this class as a container forNunit test
    public class LoginTests : PageTest
    {
        //Constructor: runs to show the browser window in slow-mo.
        public LoginTests()
        {
            Environment.SetEnvironmentVariable("HEADED", "1");
            Environment.SetEnvironmentVariable("PLAYWRIGHT_SLOW_MO", "1500");
        }

        // --- POSITIVE SCENARIOS ---

        [Test]//Marks a single executable test case.
        public async Task SuccessfulLoginTest()
        {
            //Creates the page helper.
            var loginPage = new LoginPage(Page);

            await loginPage.LoadAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            //Assert that the page URL has changed to the inventory one.
            Assert.That(Page.Url, Does.Contain("inventory.html"));
        }

        [Test]
        public async Task AddItemToShoppingCartTest()
        {
            var loginPage = new LoginPage(Page);
            var inventoryPage = new InventoryPage(Page);

            await loginPage.LoadAsync();
            await loginPage.LoginAsync("standard_user", "secret_sauce");

            await inventoryPage.AddBackpackToCartAsync();

            string cartCount = await inventoryPage.GetCartBadgeCountAsync();
            Assert.That(cartCount, Is.EqualTo("1"));
        }


        // --- DATA-DRIVEN NEGATIVE TEST SCENARIOS ---

        // Test Case 1: Wrong password input
        [TestCase("standard_user", "wrong_password", "Username and password do not match any user in this service")]

        // Test Case 2: Blank field boundary validation input
        [TestCase("", "", "Epic sadface: Username is required")]

        // New Test Case 3: Locked out user vector (SauceDemo baseline validation)
        [TestCase("locked_out_user", "secret_sauce", "Epic sadface: Sorry, this user has been locked out.")]
        public async Task InvalidLoginScenariosMatrixTest(string inputUser, string inputPassword, string expectedErrorMessage)
        {
            var loginPage = new LoginPage(Page);

            await loginPage.LoadAsync();

            // The method dynamically receives the variable inputs from the attributes above
            await loginPage.LoginAsync(inputUser, inputPassword);

            string errorMessage = await loginPage.GetErrorMessageAsync();

            // Verifies the specific error matches the target row expectations
            Assert.That(errorMessage, Does.Contain(expectedErrorMessage));
        }
    }
}