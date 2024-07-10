using InvoiceSystem.Models;
using InvoiceSystem.Services;
using System;
using Xunit;

namespace InvoiceSystem.Tests
{
    public class InvoiceServiceTests
    {
        private readonly InvoiceService _invoiceService;

        public InvoiceServiceTests()
        {
            _invoiceService = new InvoiceService();
        }

        [Fact]
        public void CanCreateInvoice()
        {
            var invoice = _invoiceService.CreateInvoice(100m, DateTime.Now.AddDays(30));
            Assert.NotNull(invoice);
            Assert.Equal(100m, invoice.Amount);
            Assert.Equal("pending", invoice.Status);
        }

        [Fact]
        public void CanPayInvoice()
        {
            var invoice = _invoiceService.CreateInvoice(100m, DateTime.Now.AddDays(30));
            _invoiceService.PayInvoice(invoice.Id, 100m);
            Assert.Equal("paid", invoice.Status);
            Assert.Equal(100m, invoice.PaidAmount);
        }

        [Fact]
        public void CanProcessOverdueInvoices()
        {
            var invoice = _invoiceService.CreateInvoice(100m, DateTime.Now.AddDays(-40));
            _invoiceService.ProcessOverdueInvoices(10m, 30);
            Assert.Equal("void", invoice.Status);
        }
    }
}

