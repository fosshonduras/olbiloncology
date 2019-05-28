using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using OLBIL.OncologyApplication.Infrastructure;

namespace OLBIL.OncologyWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public abstract class OlbilController : Controller
    {
        private IMediator _mediator;
        private IExcelFileExporter _excelFileExporter;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected IExcelFileExporter ExcelFileExporter =>
                _excelFileExporter ?? (_excelFileExporter = HttpContext.RequestServices.GetService<IExcelFileExporter>());

    }
}