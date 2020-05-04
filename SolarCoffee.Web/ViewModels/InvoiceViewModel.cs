using System;
using System.Collections.Generic;

namespace SolarCoffee.Web.ViewModels
{
    /// <summary>
    /// View model for open SalesOrder
    /// </summary>
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int CustomerId { get; set; }
        public List<SalesOrderItemViewModel> LineItems { get; set; }
    }

    /// <summary>
    /// Viuew model for sales order items
    /// </summary>
    public class SalesOrderItemViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ProductViewModel Product { get; set; }
    }
}