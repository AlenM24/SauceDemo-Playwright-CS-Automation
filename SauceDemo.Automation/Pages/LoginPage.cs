using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemo.Automation.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        // The Constructor receives the NUnit 'Page' instance and saves it locally
        public LoginPage(IPage page)
        {
            _page = page;
        }

        // The Page Map: Centralised string locator strategies
        private ILocator UsernameField => _page.Locator("#user-name");
        private ILocator PasswordField => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#login-button");
        private ILocator ErrorBanner => _page.Locator("[data-test='error']");

        // The Page Actions: Direct reusable workflows
        public async Task LoadAsync()
        {
            await _page.GotoAsync("https://saucedemo.com");
        }

        public async Task LoginAsync(string username, string password)
        {
            await UsernameField.FillAsync(username);
            await PasswordField.FillAsync(password);
            await LoginButton.ClickAsync();
        }

        public async Task<string> GetErrorMessageAsync()
        {
            return await ErrorBanner.TextContentAsync() ?? "";
        }
    }
}