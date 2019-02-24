using System;

namespace Company.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public decimal NetAmount { get; set; }

        public bool VatApplied { get; set; }

        public bool Active { get; set; }
    }
}
