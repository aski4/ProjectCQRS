using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ocr.CQRS.Commands;
using ocr.CQRS.Queries;
using ocr.Web.Controllers.Base;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ocr.Web.Controllers
{
    [Route("api/[controller]")]
    public class TabController : BaseController
    {
        private readonly IMediator _mediator;

        public TabController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("open/{id}")]
        public async Task<IActionResult> OpenTab(Guid id, [FromBody] string clientName) =>
                    (await _mediator.Send(new OpenTab { TabId = id, InitiatorName = clientName }))
                        .Match(Ok, Error);

        [HttpGet("documents")]
        public async Task<IActionResult> GetInStockBeverages() =>
                    (await _mediator.Send(new GetSavedDocuments()))
                        .Match(Ok, Error);

        [HttpPost("document/save")]
        public async Task<IActionResult> SaveDocument([FromBody] SaveDocument document) =>
            (await _mediator.Send(document))
                 .Match(Ok, Error);

        [HttpPost("document/{id}")]
        public async Task<IActionResult> PostDocument(Guid id, [FromForm] IFormFile file)
        {
            byte[] fileBytes = null;

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }

                var document = new PostDocument
                {
                    TabId = id,
                    Document = new Domain.Document
                    {
                        Id = Guid.NewGuid(),
                        fileBites = fileBytes,
                        Name = file.FileName
                    }
                };

                (await _mediator.Send(document))
                    .Match(Ok, Error);
            }

            return Error(id);
        }

        [HttpGet("document/processed/{id}")]
        public async Task<IActionResult> GetProcessedDocument(Guid id) =>
            (await _mediator.Send(new ProcessDocument { TabId = id}))
                .Match(Ok, Error);
    }
}
