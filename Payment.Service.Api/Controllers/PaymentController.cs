using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment.Service.Application.Dto.Payment;
using Payment.Service.Application.Services.Payment;

namespace Payment.Service.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("pay")]
        public IActionResult pay()
        {
            _paymentService.Pay(User.Identity.Name);
            return Ok();
        }

        [HttpGet("getAll")]
        public IActionResult get()
        {
            return Ok(_paymentService.GetPayments(User.Identity.Name));
        }
    }
}
