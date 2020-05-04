using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    /// <summary>
    /// Handles mapping Order Data Models to and from related View Models
    /// </summary>
    public class OrderMapper
    {
        /// <summary>
        /// Maps an Invoice View Model to a SalesOrder Data Model.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceToOrder(InvoiceViewModel invoice)
        {
            var now = DateTime.UtcNow;
            var salesOrderItems = invoice.LineItems
                .Select(x => new SalesOrderItem
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    Product = ProductMapper.SerializeProductViewModel(x.Product)
                }).ToList();
            
            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = now,
                UpdatedOn = now
            };
        }

        /// <summary>
        /// Maps a collection of SalesOrderModels to SalesOrderViewModels
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderViewModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            var now = DateTime.UtcNow;

            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                CreatedOn = now,
                UpdatedOn = now,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }


        /// <summary>
        /// Maps a collection of SalesOrderItemsModel to SalesOrderItemsViewModel
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemViewModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemViewModel
            {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductViewModel(item.Product)
            }).ToList();
        }
        
    }
}