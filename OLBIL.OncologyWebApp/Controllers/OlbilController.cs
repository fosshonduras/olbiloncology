using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace OLBIL.OncologyWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public abstract class OlbilController : Controller
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}