using System;
using System.Collections.Generic;
using System.Linq;
using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class CustomerMapper
   {
       /// <summary>
       /// Serializes a Customer Data Model into a CustomerViewModel 
       /// </summary>
       /// <param name="customer"></param>
       /// <returns></returns>
        public static CustomerViewModel SerializeCustomer(Customer customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                CreatedOn = customer.CreatedOn,
                UpdatedOn = customer.UpdatedOn,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

       /// <summary>
       /// Serializes a Customer View Model into a Customer Data Model 
       /// </summary>
       /// <param name="customer"></param>
       /// <returns></returns>
       public static Customer SerializeCustomer(CustomerViewModel customer)
       {
           return new Customer
           {
               Id = customer.Id,
               CreatedOn = customer.CreatedOn,
               UpdatedOn = customer.UpdatedOn,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
           };
       }
       
       /// <summary>
       /// Serializes all the customers in a  Customer Data Model into a Customer View Model 
       /// </summary>
       /// <param name="customers"></param>
       /// <returns></returns>
       public static List<CustomerViewModel> SerializeAllCustomers(IEnumerable<Customer> customers)
       {
           return customers.Select(customer => new CustomerViewModel
           {
               Id = customer.Id,
               CreatedOn = customer.CreatedOn,
               UpdatedOn = customer.UpdatedOn,
               FirstName = customer.FirstName,
               LastName = customer.LastName,
               PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress),
           })
           .OrderByDescending(customer => customer.CreatedOn)
           .ToList();
       }

       /// <summary>
       /// Maps a CustomerAddress Data Model to a CustomerAddress View Model
       /// </summary>
       /// <param name="address"></param>
       /// <returns></returns>
       public static CustomerAddressViewModel MapCustomerAddress(CustomerAddress address)
       {
           var now = DateTime.UtcNow;
           return new CustomerAddressViewModel
           {
               Id = address.Id,
               AddressLine1 = address.AddressLine1,
               AddressLine2 = address.AddressLine2,
               City = address.City,
               County = address.County,
               Country = address.Country,
               PostalCode = address.PostalCode,
               CreatedOn = now,
               UpdatedOn = now
           };
       }
       
       /// <summary>
       /// Maps a CustomerAddress View Model to a CustomerAddress Data Model
       /// </summary>
       /// <param name="address"></param>
       /// <returns></returns>
       public static CustomerAddress MapCustomerAddress(CustomerAddressViewModel address)
       {
           var now = DateTime.UtcNow;
           return new CustomerAddress
           {
               Id = address.Id,
               AddressLine1 = address.AddressLine1,
               AddressLine2 = address.AddressLine2,
               City = address.City,
               County = address.County,
               Country = address.Country,
               PostalCode = address.PostalCode,
               CreatedOn = now,
               UpdatedOn = now
           };
       }
   }
}