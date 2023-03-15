using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Contexts;
using ErrorSystem.Models.Entities;
using ErrorSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ErrorSystem.Services
{
    internal class TechnicianService
    {
        private static readonly DataContext _context = new();
        public static async Task AddTechAsync(Technician technician)
        {
            Console.Clear();
            Console.Write("First name: ");
            technician.TechFirstName = Console.ReadLine() ?? "";

            Console.Write("Last name: ");
            technician.TechLastName = Console.ReadLine() ?? "";

            Console.Write("Phone number: ");
            technician.TechPhoneNumber = Console.ReadLine() ?? "";


            if (string.IsNullOrEmpty(technician.TechFirstName) || string.IsNullOrEmpty(technician.TechLastName) || string.IsNullOrEmpty(technician.TechPhoneNumber))
            {
                Console.Clear();
                Console.WriteLine("You have to fill in all the fields to add a technician.\nPlease try again");
                return;
            }


            var _technicianEntity = new TechnicianEntity
            {
               TechFirstName = technician.TechFirstName,
               TechLastName = technician.TechLastName,
               TechPhoneNumber = technician.TechPhoneNumber,
            };

            Console.Clear();
            Console.WriteLine("Your information has been submitted.");
            _context.Add(_technicianEntity);
            await _context.SaveChangesAsync();
        }

    }
}
