using ocr.Domain.View;
using System;

namespace ocr.CQRS.Queries
{
    public class GetTabView : IQuery<TabView>
    {
        public Guid Id { get; set; }
    }
}
