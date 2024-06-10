using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Data.Entity.Validation;
using Google.Protobuf.WellKnownTypes;
using CinemaBackend.Models.DTOs.TicketDTOs;
using CinemaBackend.Services;

namespace CinemaBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ITicketService _ticketService;
        private ICustomerService _customerService;

        public StripeController(IConfiguration configuration, ITicketService ticketService, ICustomerService customerService)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            _ticketService = ticketService;
            _customerService = customerService;
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CheckoutSessionRequest request)
        {

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var product in request.Products!)
            {
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = product.Amount * 100,
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name,
                        },
                    },
                    Quantity = product.Quantity,
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = _configuration["Stripe:SuccessUrl"],
                CancelUrl = _configuration["Stripe:CancelUrl"],
                Metadata = new Dictionary<string, string>
        {
                    { "screeningId", request.ScreeningId! },
                    { "customerEmail", request.CustomerEmail! }
        }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Ok(new { sessionId = session.Id });
        }

        [HttpPost("Webhook")]
        public async Task<IActionResult> HandleStripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _configuration["Stripe:WebhookSecret"]);

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                // Fulfill the order
                await FulfillOrder(session);
            }

            return Ok();
        }

        private async Task FulfillOrder(Session session)
        {
            // Retrieve the session. If you require line items in the response, you may include them by expanding line_items.
            var options = new SessionGetOptions { Expand = new List<string> { "line_items" } };
            var service = new SessionService();
            var sessionWithLineItems = await service.GetAsync(session.Id, options);

            var lineItems = sessionWithLineItems.LineItems;
            var customerEmail = session.Metadata["customerEmail"];
            var screeningID = session.Metadata["screeningId"];

            foreach (var item in lineItems.Data)
            {


                System.Diagnostics.Debug.WriteLine("Customer Email: " + customerEmail);


                Guid customerId = _customerService.GetCustomerByEmail(customerEmail).Result.CustomerId;

                Ticket ticket = new Ticket
                {
                    TicketType = item.Description.Contains("VIP") ? "VIP" : "Standard",
                    TicketSeat = item.Description.Substring(0, 2),
                    TicketPrice = item.AmountTotal / 100,
                    ScreeningId = new Guid(screeningID),
                    CustomerId = customerId
                };

                await _ticketService.CreateTicket(ticket);
            }
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var options = new BalanceTransactionListOptions
                {
                    Limit = 10, // Limit the number of transactions to retrieve
                };

                var service = new BalanceTransactionService();
                var transactions = await service.ListAsync(options);

                var simplifiedTransactions = new List<object>();
                foreach (var transaction in transactions)
                {
                    var simplifiedTransaction = new
                    {
                        Amount = transaction.Amount,
                        Customer = transaction.Id,
                        Currency = transaction.Currency,
                        Date = transaction.Created
                    };
                    simplifiedTransactions.Add(simplifiedTransaction);
                }
                
                return Ok(simplifiedTransactions);
            }
            catch (StripeException e)
            {
                return StatusCode((int)e.HttpStatusCode, new { error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while fetching transactions." });
            }
        }
    }
}
