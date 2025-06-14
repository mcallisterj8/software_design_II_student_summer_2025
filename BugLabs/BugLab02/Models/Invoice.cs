﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace BugLab02.Models {
    [DebuggerDisplay("InvoiceId = {InvoiceId}")]
    public class Invoice {
        [Key]
        public int InvoiceId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [MaxLength(70)]
        public string? BillingAddress { get; set; }

        [MaxLength(40)]
        public string? BillingCity { get; set; }

        [MaxLength(40)]
        public string? BillingState { get; set; }

        [MaxLength(40)]
        public string? BillingCountry { get; set; }

        [MaxLength(10)]
        public string? BillingPostalCode { get; set; }

        [Required]
        public Decimal Total { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
