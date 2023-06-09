﻿using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.GetReceipts
{
    public record GetReceiptsRequest 
    {
        public GetReceiptsRequest()
        {
            this.PageFilter = new PageFilterRequest { Page = 1, PageSize = 60, };
        }

        [BindProperty(Name = "")]
        public PageFilterRequest PageFilter { get; set; }

        [FromQuery]
        public IEnumerable<Guid> ReceiptIds { get; set; } = new List<Guid>();
        [FromQuery]
        public IEnumerable<Guid> ReceiptItemIds { get; set; } = new List<Guid>();
        [FromQuery]
        public IEnumerable<string> EstablishmentNames { get; set; } = new List<string>();
        [FromQuery]
        public IEnumerable<string> ItemNames { get; set; } = new List<string>();
        [FromQuery]
        public DateTime ReceiptDate { get; set; }
        [FromQuery]
        public DateTime ReceiptDateFinal { get; set; }
    }

    public record PageFilterRequest
    {
        private const int LowerBoundPageNumber = 1;

        private int page;

        private int pageSize = 60;

        [FromQuery(Name = "page")]
        public int Page
        {
            get => this.page;
            set => this.page = (value < LowerBoundPageNumber) ? LowerBoundPageNumber : value;
        }

        [FromQuery(Name = "pageSize")]
        public int PageSize
        {
            get => this.pageSize;
            set => this.pageSize = (value < 1) ? 1 : value;
        }
    }
}
