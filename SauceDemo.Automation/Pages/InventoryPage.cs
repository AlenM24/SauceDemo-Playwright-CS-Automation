using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemo.Automation.Pages
{
    public class InventoryPage
    {
        private readonly IPage _page;

        //Constructor: binds the same active browser tab to this second page object.
        public InventoryPage(IPage page)
        {
            _page = page;
        }

        // Dashboard specific elements locators.
        private ILocator AddBackpackButton => _page.Locator("#add-to-cart-sauce-labs-backpack");
        private ILocator CartBadge => _page.Locator(".shopping_cart_badge");

        public async Task AddBackpackToCartAsync()
        {
            await AddBackpackButton.ClickAsync();
        }

        //Extracts the numbershowing in the shopping cart circle.
        public async Task<string> GetCartBadgeCountAsync()
        {
            return await CartBadge.TextContentAsync() ?? "";
        }
    }
}