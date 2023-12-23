﻿using Application.Claims;
using Application.Queries.Receipt.GetReceipts;
using CrossCutting.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    [Authorize]
    public class ReceiptController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReceiptController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// GET receipts based on previously informeted filters.
        /// Required at least a filter as parameters.
        /// </summary>
        /// <returns>Return a list of receipts based on pre determined filters.</returns>
        [HttpGet]
        [Route("getReceipts", Name = nameof(GetReceipts))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ClaimsAuthorize(ClaimTypes.Receipt, "Read")]
        public async Task<IActionResult> GetReceipts([FromRoute] GetReceiptsRequest getReceiptsRequest, CancellationToken cancellationToken)
        {
            var receipts = await _mediator.Send(new GetReceiptsQuery(getReceiptsRequest), cancellationToken);
            return Ok(receipts);
        }
    }
}
