using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Contexts;
using ErrorSystem.Models;
using ErrorSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorSystem.Services
{
    internal class CustomerService
    {
        private static readonly DataContext _context = new();

        public static async Task AddCustomerAsync(Customer customer)
        {
            Console.Clear();
            Console.Write("First name: ");
            customer.FirstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            customer.LastName = Console.ReadLine() ?? "";

            Console.Write("E-mail: ");
            customer.Email = Console.ReadLine() ?? "";

            Console.Write("Phone number: ");
            customer.PhoneNumber = Console.ReadLine() ?? "";

            Console.Write("Street name: ");
            customer.StreetName = Console.ReadLine() ?? "";

            Console.Write("Zip code: ");
            customer.ZipCode = Console.ReadLine() ?? "";

            Console.Write("City: ");
            customer.City = Console.ReadLine() ?? "";

            var _customerEntity = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                Email = customer.Email,
            };

            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName) || string.IsNullOrEmpty(customer.Email) || string.IsNullOrEmpty(customer.PhoneNumber) || string.IsNullOrEmpty(customer.StreetName) || string.IsNullOrEmpty(customer.ZipCode) || string.IsNullOrEmpty(customer.City))
            {
                Console.Clear();
                Console.WriteLine("You have to fill in all the fields to add a customer.\nPlease try again");
                return;
            }

            var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == customer.StreetName && x.ZipCode == customer.ZipCode && x.City == customer.City);
            if (addressEntity != null)
                _customerEntity.AddressId = addressEntity.AddressId;
            else
                _customerEntity.Address = new AddressEntity
                { 
                    StreetName = customer.StreetName,
                    ZipCode = customer.ZipCode,
                    City = customer.City,
                };


            Console.Clear();
            Console.WriteLine("Your information has been submitted.");
            _context.Add(_customerEntity);
            await _context.SaveChangesAsync();
        }

    }
}
