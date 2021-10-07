using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using TwitchCommunity.Application.Enlistments;
using TwitchCommunity.Host.Models;

namespace TwitchCommunity.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            this.mediator = mediator;
            _logger = logger;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var response = await mediator.Send(new GetEnlistmentsRequest());

            return View(response.Enlistments);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
