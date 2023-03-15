using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Models;
using ErrorSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ErrorSystem.Services
{
    internal class MenuService
    {
        // ADD PERSON
        public async Task AddPersonAsync()
        {
            try
            {
                Console.WriteLine("Please enter the person type you want to add:\n");
                Console.WriteLine("1. Technician");
                Console.WriteLine("2. Customer\n");

                int answer = int.Parse(Console.ReadLine());

                if (answer == 1)
                {
                    await TechnicianService.AddTechAsync(new Technician());
                }
                else if (answer == 2)
                {
                    await CustomerService.AddCustomerAsync(new Customer());
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("The input was not in a correct format.");
            }
        }


        // ADD REPORT
        public async Task AddReportAsync()
        {
            var errorReport = new ErrorReport();
            await ErrorReportService.AddReportAsync(errorReport);
        }

        // GET ALL
        public async Task AllReportsAsync()
        {
            var errorReports = await ErrorReportService.GetAllAsync();

            if (errorReports.Any())
            {
                foreach (ErrorReport errorReport in errorReports)
                {
                    Console.WriteLine("Error report:");
                    Console.WriteLine($"ID: {errorReport.ErrorId}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Date reported: {errorReport.DateReported}\n");

                    Console.WriteLine("Status:");
                    Console.WriteLine($"Value: {errorReport.StatusName}");
                    if (!string.IsNullOrEmpty(errorReport.TechFirstName) && !string.IsNullOrEmpty(errorReport.TechLastName))
                    {
                        Console.WriteLine($"By: {errorReport.TechFirstName} {errorReport.TechLastName}");
                    }

                    var comment = await CommentService.GetAsync(errorReport.CommentId.GetValueOrDefault());


                    if (comment != null)
                    {
                        Console.WriteLine("\nComment:");
                        Console.WriteLine($"Text: {comment.CommentText}");
                        Console.WriteLine($"By: {comment.FirstName} {comment.LastName}\n");
                    }

                    Console.WriteLine("-----------------------------------------------------\n");


                }
            }

            else
            {
                Console.WriteLine("List is empty");
            }
        }

        // GET ONE
        public async Task SpecificReportAsync()
        {
            Console.WriteLine("Enter the ID of the error report you want to receive:");
            var errorIdString = Console.ReadLine();

            if (int.TryParse(errorIdString, out int errorId))
            {
                var errorReport = await ErrorReportService.GetAsync(errorId);
                if (errorReport != null)
                {
                    Console.Clear();
                    Console.WriteLine("Error report:");
                    Console.WriteLine($"ID: {errorReport.ErrorId}");
                    Console.WriteLine($"Description: {errorReport.Description}");
                    Console.WriteLine($"Date reported: {errorReport.DateReported}\n");

                    Console.WriteLine("Customer:");
                    Console.WriteLine($"ID: {errorReport.CustomId}");
                    Console.WriteLine($"Name: {errorReport.FirstName} {errorReport.LastName}");
                    Console.WriteLine($"E-mail: {errorReport.Email}");
                    Console.WriteLine($"Phone number: {errorReport.PhoneNumber}");
                    Console.WriteLine($"Address: {errorReport.StreetName}, {errorReport.ZipCode} {errorReport.City}\n");

                    Console.WriteLine("Status:");
                    Console.WriteLine($"Value: {errorReport.StatusName}");
                    Console.WriteLine($"By: {errorReport.TechFirstName} {errorReport.TechLastName}");
                    Console.WriteLine($"Date updated: {errorReport.DateUpdated}\n");

                    var comment = await CommentService.GetAsync(errorReport.CommentId.GetValueOrDefault());
                    Console.WriteLine("Comment:");
                    Console.WriteLine($"Text: {comment?.CommentText}");
                    Console.WriteLine($"By: {comment?.FirstName} {comment?.LastName}");
                    Console.WriteLine($"Date created: {comment?.CommentCreated}\n\n");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                }

            }
        }

        // UPDATE
        public async Task UpdateStatusAsync()
        {
            try
            {
                Console.WriteLine("Please enter the person type you want to add:\n");
                Console.WriteLine("1. Updatera till 'på börjad'");
                Console.WriteLine("2. Uppdatera till 'avslutad'\n");

                int answer = int.Parse(Console.ReadLine());

                if (answer == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the ID of the error report you want to update:");
                    int errorId = int.Parse(Console.ReadLine());
                    await ErrorReportService.InProgressAsync(errorId, "In Progress");
                }
                else if (answer == 2)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the ID of the error report you want to update:");
                    int errorId = int.Parse(Console.ReadLine());
                    await ErrorReportService.CompletedAsync(errorId, "Completed");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("The input was not in a correct format.");
            }
        }

        // ADD COMMENT
        public async Task AddCommentAsync()
        {
            try
            {
                Console.WriteLine("Enter the ID of the report you want to comment: ");
                int errorId = int.Parse(Console.ReadLine());

                Console.WriteLine("\nEnter your comment:");
                string commentText = Console.ReadLine() ?? "";

                await ErrorReportService.AddCommentAsync(errorId, commentText);

            }
            catch
            {
                Console.Clear();
                Console.WriteLine("The input was not in a correct format.");
            }
        }


        // DELETE REPORT
        public async Task DeleteReportsAsync()
        {
            Console.Write("Enter the ID of the report to delete: ");
            int errorReportId = int.Parse(Console.ReadLine());

            await ErrorReportService.DeleteReportsAsync(errorReportId);
        }



    }
}
