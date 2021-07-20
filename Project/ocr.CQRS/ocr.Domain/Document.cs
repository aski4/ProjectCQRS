using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ocr.Domain
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public byte[] fileBites { get; set; }

        public string Text { get; set; }
    }
}
