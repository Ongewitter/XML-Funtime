using Microsoft.AspNetCore.Mvc.RazorPages;
using OrderViewer.Models;

namespace OrderViewer.Pages {
    public class IndexModel : PageModel {
        private readonly ILogger<IndexModel> _logger;
        public List<Order> orders;

        public IndexModel(ILogger<IndexModel> logger) {
            _logger = logger;
        }

        public void OnGet() {
            this.orders = Order.GetOrders();
        }
    }
}