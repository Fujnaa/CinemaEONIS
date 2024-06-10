using Stripe;

namespace CinemaBackend.Models
{
    public class CheckoutSessionRequest
    {
        public List<Product>? Products { get; set; }
        public String? ScreeningId {  get; set; }
        public String? CustomerEmail { get; set; }
    }
}
