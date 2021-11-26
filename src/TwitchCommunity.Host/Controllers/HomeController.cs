using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TwitchCommunity.Application.Enlistments;
using TwitchCommunity.Application.ViewerGames.Enlistments;
using TwitchCommunity.Host.Models;
using TwitchCommunity.Host.ViewModels;

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

            return View(response.Enlistments.Select(x => new EnlistmentViewModel(x)));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(DrawApiParameters drawApiParameters)
        {
            if (drawApiParameters.Selected != null)
            {
                await mediator.Send(new DrawEnlistmentsCommand(drawApiParameters.Selected));
            }

            if (drawApiParameters.ActiveSelected != null)
            {
                await mediator.Send(new CloseEnlistmentsCommand(drawApiParameters.ActiveSelected));
            }

            var response = await mediator.Send(new GetEnlistmentsRequest());
            return View(response.Enlistments.Select(x => new EnlistmentViewModel(x)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
