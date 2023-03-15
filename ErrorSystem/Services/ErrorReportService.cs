using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Contexts;
using ErrorSystem.Models;
using ErrorSystem.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ErrorSystem.Services
{
    internal class ErrorReportService
    {
        private static readonly DataContext _context = new();

        // ADD REPORT
        public static async Task AddReportAsync(ErrorReport errorReport)
        {
            Console.WriteLine("Please enter your first name:");
            errorReport.FirstName = Console.ReadLine() ?? "";

            Console.WriteLine("\nPlease enter your last name:");
            errorReport.LastName = Console.ReadLine() ?? "";

            var customerExists = await _context.Customers.AnyAsync(e => e.FirstName == errorReport.FirstName && e.LastName == errorReport.LastName);
            if (!customerExists)
            {
                Console.Clear();
                Console.WriteLine("No customer found.\nPlease go back to the menu and add your details.");
                return;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(e => e.FirstName == errorReport.FirstName && e.LastName == errorReport.LastName);
            if (customer == null)
            {
                Console.Clear();
                Console.WriteLine("No customer found.\nPlease go back to the menu and add your details.");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Is this you? Press Y for yes or N for no\n");
                Console.WriteLine($"First name: {customer.FirstName}");
                Console.WriteLine($"Last name: {customer.LastName}");
                Console.WriteLine($"E-mail: {customer.Email}\n");
                var answer = Console.ReadLine().ToLower();

                if (answer == "y")
                {
                    Console.Clear();
                    Console.WriteLine("Please describe the issue:");
                    errorReport.Description = Console.ReadLine() ?? "";
                }
                else if (answer == "n")
                {
                    Console.Clear();
                    Console.WriteLine("Error reporting has been cancelled.");
                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                    return;
                }
            }

            Console.WriteLine("\nPress 1 to save\nPress 2 to cancel\n");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                var _errorReportEntity = new ErrorReportEntity
                {
                    CustomId = customer.CustomId,
                    Description = errorReport.Description,
                    DateReported = DateTime.Now,
                };
                _context.Add(_errorReportEntity);
                await _context.SaveChangesAsync();

                Console.Clear();
                Console.WriteLine("Thank you! Your error report has been submitted.");
            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine("Error reporting has been cancelled.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");
            }
        }

        // GET ALL
        public static async Task<IEnumerable<ErrorReport>> GetAllAsync()
        {
            var _errorReports = new List<ErrorReport>();

            foreach (var _errorReport in await _context.ErrorReports
                .Include(x => x.Customer.Address)
                .Include(e => e.Comment)
                .Include(x => x.Technician)
                .Include(x => x.StatusReport)
                .ToListAsync())
                _errorReports.Add(new ErrorReport
                {
                    ErrorId = _errorReport.ErrorId,
                    Description = _errorReport.Description,
                    DateReported = _errorReport.DateReported,
                    CustomId = _errorReport.Customer.CustomId,
                    FirstName = _errorReport.Customer.FirstName,
                    LastName = _errorReport.Customer.LastName,
                    Email = _errorReport.Customer.Email,
                    PhoneNumber = _errorReport.Customer.PhoneNumber,

                    StreetName = _errorReport.Customer.Address.StreetName,
                    ZipCode = _errorReport.Customer.Address.ZipCode,
                    City = _errorReport.Customer.Address.City,

                    StatusId = _errorReport.StatusReport.StatusId,
                    StatusName = _errorReport.StatusReport.StatusName,
                    DateUpdated = _errorReport.StatusReport.DateUpdated,

                    CommentId = _errorReport?.Comment?.CommentId,
                    CommentText = _errorReport?.Comment?.CommentText,
                    CommentCreated = _errorReport?.Comment?.CommentCreated,

                    TechnicianId = _errorReport?.Technician?.TechnicianId,
                    TechFirstName = _errorReport?.Technician?.TechFirstName,
                    TechLastName = _errorReport?.Technician?.TechLastName

                });

            return _errorReports;
        }

        // GET ONE 
        public static async Task<ErrorReport> GetAsync(int ErrorId)
        {
            var _errorReport = await _context.ErrorReports
                .Include(x => x.Customer.Address)
                .Include(x => x.StatusReport)
                 .Include(e => e.Comment)
                .Include(x => x.Technician)
                .FirstOrDefaultAsync(x => x.ErrorId == ErrorId);
            if (_errorReport != null)
                return new ErrorReport
                {
                    ErrorId = _errorReport.ErrorId,
                    Description = _errorReport.Description,
                    DateReported = _errorReport.DateReported,
                    CustomId = _errorReport.Customer.CustomId,
                    FirstName = _errorReport.Customer.FirstName,
                    LastName = _errorReport.Customer.LastName,
                    Email = _errorReport.Customer.Email,
                    PhoneNumber = _errorReport.Customer.PhoneNumber,
                    StreetName = _errorReport.Customer.Address.StreetName,
                    ZipCode = _errorReport.Customer.Address.ZipCode,
                    City = _errorReport.Customer.Address.City,

                    CommentId = _errorReport?.Comment?.CommentId,
                    CommentText = _errorReport?.Comment?.CommentText,
                    CommentCreated = _errorReport?.Comment?.CommentCreated,

                    StatusId = _errorReport.StatusReport.StatusId,
                    StatusName = _errorReport.StatusReport.StatusName,
                    DateUpdated = _errorReport.StatusReport.DateUpdated,

                    TechnicianId = _errorReport?.Technician?.TechnicianId,
                    TechFirstName = _errorReport?.Technician?.TechFirstName,
                    TechLastName = _errorReport?.Technician?.TechLastName
                };
            else
                return null!;
        }

        // UPDATE STATUS AND ASSIGN
        public static async Task InProgressAsync(int errorId, string newStatus)
        {
            var errorReports = await _context.ErrorReports
                  .Include(e => e.StatusReport)
                  .FirstOrDefaultAsync(x => x.ErrorId == errorId);
            if (errorReports == null)
            {
                Console.Clear();
                Console.WriteLine("Error report was not found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Please enter your first name:");
            string firstName = Console.ReadLine() ?? "";
            Console.WriteLine("\nPlease enter your last name:");
            string lastName = Console.ReadLine() ?? "";

            var technician = await _context.Technicians.FirstOrDefaultAsync(t => t.TechFirstName == firstName && t.TechLastName == lastName);
            if (technician == null)
            {
                Console.Clear();
                Console.WriteLine("Technician not found.");
                return;
            }

            Console.WriteLine($"\n\nWhen you update the status to \"started\", you assign yourself the error report.\nDo you approve this?\n\nPress Y for yes or N for no\n");
            var answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                errorReports.StatusReport.StatusName = newStatus;
                errorReports.TechnicianId = technician.TechnicianId;
                errorReports.StatusReport.DateUpdated = DateTime.Now;

                _context.Update(errorReports);
                await _context.SaveChangesAsync();
                Console.Clear();
                Console.WriteLine($"You have been assigned error report {errorId}.");
            }
            else if (answer == "n")
            {
                Console.Clear();
                Console.WriteLine("The update has been cancelled.");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");
            }
        }

        public static async Task CompletedAsync(int errorId, string newStatus)
        {
            var errorReports = await _context.ErrorReports
                  .Include(e => e.StatusReport)
                  .FirstOrDefaultAsync(x => x.ErrorId == errorId);
            if (errorReports == null)
            {
                Console.Clear();
                Console.WriteLine("Error report was not found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Please enter your first name:");
            string firstName = Console.ReadLine() ?? "";
            Console.WriteLine("\nPlease enter your last name:");
            string lastName = Console.ReadLine() ?? "";


            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                Console.Clear();
                Console.WriteLine("No technician found.\nPlease go back to the menu and add your details.");
                return;
            }


            var technician = await _context.Technicians.FirstOrDefaultAsync(t => t.TechFirstName == firstName && t.TechLastName == lastName);
            if (technician == null)
            {
                Console.Clear();
                Console.WriteLine("Technician was not found.");
                return;
            }

            Console.WriteLine($"\n\nWhen you update the status to \"completed\", you are announcing that the error report has been completed.\nDo you approve this?\n\nPress Y for yes or N for no\n");
            var answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                errorReports.StatusReport.StatusName = newStatus;
                errorReports.TechnicianId = technician.TechnicianId;
                errorReports.StatusReport.DateUpdated = DateTime.Now;

                _context.Update(errorReports);
                await _context.SaveChangesAsync();
                Console.Clear();
                Console.WriteLine($"The error report {errorId} is now completed.");
            }
            else if (answer == "n")
            {
                Console.Clear();
                Console.WriteLine("The update has been cancelled.");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");
            }
        }

        // ADD COMMENT
        public static async Task AddCommentAsync(int errorId, string commentText)
        {
            var errorReport = await _context.ErrorReports
                .FirstOrDefaultAsync(x => x.ErrorId == errorId);
            if (errorReport == null)
            {
                Console.Clear();
                Console.WriteLine("Error report was not found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("Please enter your first name:");
            string firstName = Console.ReadLine() ?? "";

            Console.WriteLine("\nPlease enter your last name:");
            string lastName = Console.ReadLine() ?? "";

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                Console.Clear();
                Console.WriteLine("No customer found.\nPlease go back to the menu and add your details.");
                return;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(t => t.FirstName == firstName && t.LastName == lastName);
            if (customer == null)
            {
                Console.Clear();
                Console.WriteLine("Customer was not found.");
                return;
            }


            Console.WriteLine("\nPress 1 to save\nPress 2 to cancel\n");
            var answer = Console.ReadLine().ToLower();
            if (answer == "1")
            {
                errorReport.Comment = new CommentEntity
                {
                    CommentText = commentText,
                    CommentCreated = DateTime.Now,
                    CustomId = customer.CustomId
                };

                await _context.SaveChangesAsync();
                Console.Clear();
                Console.WriteLine($"The comment has been saved.");
            }
            else if (answer == "2")
            {
                Console.Clear();
                Console.WriteLine("Operation cancelled.");
                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");
            }
        }

        // DELETE
        public static async Task DeleteReportsAsync(int ErrorId)
        {
            var _errorReport = await _context.ErrorReports
                .Include(e => e.StatusReport)
                .Include(e => e.Customer.Address)
                .Include(e => e.Technician)
                .Include(e => e.Comment)
                .FirstOrDefaultAsync(x => x.ErrorId == ErrorId);
            if (_errorReport == null)
            {
                Console.Clear();
                Console.WriteLine("Error report was not found.");
                return;
            }

            int customerCount = await _context.ErrorReports.CountAsync(x => x.CustomId == _errorReport.CustomId);

            if (customerCount == 1)
            {
                var customer = await _context.Customers.FindAsync(_errorReport.CustomId);
                _context.Customers.Remove(customer);
            }

            if (_errorReport.TechnicianId != null)
            {
                int technicianCount = await _context.ErrorReports.CountAsync(x => x.TechnicianId == _errorReport.TechnicianId);

                if (technicianCount == 1)
                {
                    var technician = await _context.Technicians.FindAsync(_errorReport.TechnicianId);
                    _context.Technicians.Remove(technician);
                }
            }

            if (_errorReport.Comment != null)
            {
                _context.Comments.Remove(_errorReport.Comment);
            }

            if (_errorReport.StatusReport != null)
            {
                _context.StatusReports.Remove(_errorReport.StatusReport);
            }


            Console.WriteLine($"\nAre you sure you want to delete error report with ID {_errorReport.ErrorId}?\nPress Y for yes or N for no");
            var answer = Console.ReadLine().ToLower();
            if (answer == "y")
            {
                _context.Remove(_errorReport);
                await _context.SaveChangesAsync();
                Console.Clear();
                Console.WriteLine("Error report has been deleted successfully.");
            }
            else if (answer == "n")
            {
                Console.Clear();
                Console.WriteLine("The removal failed.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid choice.");       
            }
        }
    }
}
