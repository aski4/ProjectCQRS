using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ocr.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        protected IActionResult Ok(Unit unit) =>
            Ok();

        protected IActionResult Error<TError>(TError error) =>
            new BadRequestObjectResult(error);
    }
}
