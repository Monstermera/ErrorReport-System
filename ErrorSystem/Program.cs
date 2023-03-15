using ErrorSystem.Services;

var menu = new MenuService();
while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome to the error reporting system!\nPlease choose one of the following\n");
    Console.WriteLine("1. Add person");
    Console.WriteLine("2. Add error report - for customers");
    Console.WriteLine("3. Update status on error reports - for technicians");
    Console.WriteLine("4. Show all error reports");
    Console.WriteLine("5. Show one specific error report");
    Console.WriteLine("6. Add a comment - for customers");
    Console.WriteLine("7. Delete");


    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            await menu.AddPersonAsync();
            break;
        case "2":
            Console.Clear();
            await menu.AddReportAsync();
            break;
        case "3":
            Console.Clear();
            await menu.UpdateStatusAsync();
            break;
        case "4":
            Console.Clear();
            await menu.AllReportsAsync();
            break;
        case "5":
            Console.Clear();
            await menu.SpecificReportAsync();
            break;
        case "6":
            Console.Clear();
            await menu.AddCommentAsync();
            break;
        case "7":
            Console.Clear();
            await menu.DeleteReportsAsync();
            break;
    }
    Console.ReadKey();
}
