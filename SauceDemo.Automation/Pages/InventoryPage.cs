using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SauceDemo.Automation.Pages
{
    public class InventoryPage
    {
        private readonly IPage _page;

        public InventoryPage(IPage page)
        {
            _page = page;
        }

        // Dashboard specific elements
        private ILocator AddBackpackButton => _page.Locator("#add-to-cart-sauce-labs-backpack");
        private ILocator CartBadge => _page.Locator(".shopping_cart_badge");

        public async Task AddBackpackToCartAsync()
        {
            await AddBackpackButton.ClickAsync();
        }

        public async Task<string> GetCartBadgeCountAsync()
        {
            return await CartBadge.TextContentAsync() ?? "";
        }
    }
}