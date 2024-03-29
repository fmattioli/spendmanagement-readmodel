﻿using Application.Converters;
using Domain.Interfaces;
using MediatR;
using SpendManagement.WebContracts.Common;
using SpendManagement.WebContracts.Receipt;

namespace Application.Queries.Receipt.GetRecurringReceipts
{
    public class GetRecurringReceiptsQueryHandler(IReceiptRepository receiptRepository) : IRequestHandler<GetRecurringReceiptsQuery, PagedResult<RecurringReceiptResponse>>
    {
        private readonly IReceiptRepository recurringReceiptRepository = receiptRepository;

        public async Task<PagedResult<RecurringReceiptResponse>> Handle(GetRecurringReceiptsQuery request, CancellationToken cancellationToken)
        {
            var domainFilters = request.GetRecurringReceiptsRequest.ToDomainFilters();
            var recurringReceiptQueryResult = await recurringReceiptRepository.GetRecurringReceiptsAsync(domainFilters);
            return recurringReceiptQueryResult.ToResponse(request.GetRecurringReceiptsRequest.PageFilter);
        }
    }
}
