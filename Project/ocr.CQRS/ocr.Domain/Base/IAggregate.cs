using System;

namespace ocr.Domain.Base
{
    public interface IAggregate
    {
        Guid Id { get; set; }
    }
}
