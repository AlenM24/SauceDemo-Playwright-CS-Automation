using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemo.Automation.Pages
{
    public class LoginPage
    {
        //Stores the active browser tab passed in from the text file
        private readonly IPage _page;

        // The Constructor receives the NUnit 'Page' instance and saves it locally
        public LoginPage(IPage page)
        {
            _page = page;
        }

        // The Page Map(Lazy locators): acts as saved GPS address for Html elements.
        private ILocator UsernameField => _page.Locator("#user-name");
        private ILocator PasswordField => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#login-button");
        private ILocator ErrorBanner => _page.Locator("[data-test='error']");

        // The Page Actions: navigates the browser to the login url.
        public async Task LoadAsync()
        {
            await _page.GotoAsync("https://saucedemo.com");
        }

        //Combines typing credentials and clicking submit into a songle work flow.
        public async Task LoginAsync(string username, string password)
        {
            await UsernameField.FillAsync(username);
            await PasswordField.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        //Reads the red warning banner text and returns as a safe string(never null).
        public async Task<string> GetErrorMessageAsync()
        {
            return await ErrorBanner.TextContentAsync() ?? "";
        }
    }
}