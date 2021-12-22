using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StreamCommunity.Application.ViewerGames.Enlistments;

namespace StreamCommunity.Api.ViewerGames
{
    [ApiController]
    [Route("api/viewer-games/enlistments")]
    public class EnlistmentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public EnlistmentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnlistmentsAsync(CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetEnlistmentsRequest(), cancellationToken);
            var enlistmentModels = response.Enlistments
                .Select(x => new EnlistmentModel
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    State = (EnlistmentState)x.State,
                    Timestamp = x.Timestamp - TimeSpan.Zero
                });
            return Ok(enlistmentModels);
        }

        [HttpPut("{enlistmentId}/draw")]
        public async Task<IActionResult> DrawEnlistmentAsync(int enlistmentId)
        {
            await mediator.Send(new DrawEnlistmentsCommand(new[] { enlistmentId }));
            return Ok();
        }

        [HttpPut("{enlistmentId}/close")]
        public async Task<IActionResult> CloseEnlistmentAsync(int enlistmentId)
        {
            await mediator.Send(new CloseEnlistmentsCommand(new[] { enlistmentId }));
            return Ok();
        }
    }
}