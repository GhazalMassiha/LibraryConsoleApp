using HW12_Issue1.Application_Services;
using HW12_Issue1.Application_Services.ServiceExceptions;
using HW12_Issue1.Application_Services.Services;
using HW12_Issue1.Domain.Entities;
using HW12_Issue1.Enums;
using HW12_Issue1.Interfaces.Service_Contracts;

IAuthenticationService _authenticationService = new AuthenticationService();
IUserService _userService = new UserService();
IBookService _bookService = new BookService();
IAdminService _adminService = new AdminService();


while (true)
{
    Console.Clear();
    string title = @"
 
  _     _ _                               _                
 | |   (_) |__  _ __ __ _ _ __ _   _     / \   _ __  _ __  
 | |   | | '_ \| '__/ _` | '__| | | |   / _ \ | '_ \| '_ \ 
 | |___| | |_) | | | (_| | |  | |_| |  / ___ \| |_) | |_) |
 |_____|_|_.__/|_|  \__,_|_|   \__, | /_/   \_\ .__/| .__/ 
                               |___/          |_|   |_|    
 ";
    Console.WriteLine(title);
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.ResetColor();
    Console.WriteLine("________________________________________________");
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("1.Login");
    Console.WriteLine("2.Register");
    Console.WriteLine("0.Exit");
    Console.ResetColor();
    Console.WriteLine("________________________________________________");
    Console.WriteLine();

    string input = Console.ReadLine()!;
    switch (input)
    {
        case "1":
            Login();
            Console.ReadKey();
            break;

        case "2":
            Register();
            Console.ReadKey();
            break;

        case "0":
            Console.ReadKey();
            return;

        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid Input. Please Enter 0, 1, or 2.");
            Console.ResetColor();
            Console.ReadKey();
            break;
    }
}


void Login()
{
    try
    {
        Console.Write("\n\nUsername: ");
        string username = Console.ReadLine()!;
        Console.Write("\n\nPassword: ");
        string password = Console.ReadLine()!;

        var (user, admin) = _authenticationService.Login(username, password);

        if (admin != null)
        {
            AdminMenu();
        }
        else if (user != null)
        {
            UserMenu(user);
        }
    }
    catch (UsernameNotFoundException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
    }
    catch (InvalidPasswordException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.ResetColor();
        Console.ReadKey();
    }
}

void Register()
{
    try
    {
        Console.Write("Register As (User = 1, Admin = 2): ");
        if (!Enum.TryParse<RoleEnum>(Console.ReadLine(), out var role))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid Role Selection. Please Enter 1 For User or 2 For Admin.");
            Console.ResetColor();
            return;
        }

        Console.Write("\n\nUsername: ");
        string username = Console.ReadLine()!;

        Console.Write("\n\nPassword: ");
        string password = Console.ReadLine()!;

        switch (role)
        {
            case RoleEnum.User:
                _authenticationService.RegisterUser(username, password);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRegistered User Successfully.");
                Console.ReadKey();
                break;

            case RoleEnum.Admin:
                _authenticationService.RegisterAdmin(username, password);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRegistered Admin Successfully.");
                Console.ReadKey();
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Role Selection.");
                Console.ReadKey();
                break;
        }
    }
    catch (UsernameAlreadyExistsException ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
    }
    finally
    {
        Console.ResetColor();
    }
}

void UserMenu(User user)
{
    while (true)
    {
        Console.Clear();
        string title = @"
 
   _   _                 __  __                  
 | | | |___  ___ _ __  |  \/  | ___ _ __  _   _ 
 | | | / __|/ _ \ '__| | |\/| |/ _ \ '_ \| | | |
 | |_| \__ \  __/ |    | |  | |  __/ | | | |_| |
  \___/|___/\___|_|    |_|  |_|\___|_| |_|\__,_|
                                                
 ";
        Console.WriteLine(title);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.ResetColor();
        Console.WriteLine("________________________________________________");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("1. View All Books");
        Console.WriteLine("2. View All Categories");
        Console.WriteLine("3. Borrow Book");
        Console.WriteLine("4. Return Book");
        Console.WriteLine("5. View My Borrowed Books");
        Console.WriteLine("6. View My Reviews");
        Console.WriteLine("7. Add Review");
        Console.WriteLine("8. Edit Review");
        Console.WriteLine("9. Delete Review");
        Console.WriteLine("0. Logout");
        Console.ResetColor();
        Console.WriteLine("________________________________________________");
        Console.WriteLine();

        string input = Console.ReadLine()!;

        switch (input)
        {
            case "1":
                _bookService.ViewAllBooks();
                Console.ReadKey();
                break;

            case "2":
                _bookService.ViewAllCategories();
                Console.ReadKey();
                break;

            case "3":
                _bookService.ViewAllBooks();
                Console.Write("\n\nEnter Book ID to Borrow: ");
                if (int.TryParse(Console.ReadLine(), out int bookId))
                {
                    bool success = _userService.BorrowBook(user, bookId);
                    if (success == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nBook Borrowed Successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFailed To Borrow book (Already Borrowed or Doesn't Exist).");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Book ID.");
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "4":
                _userService.ViewMyBorrowedBooks(user);
                Console.Write("\n\nEnter Borrowed ID to Return: ");
                if (int.TryParse(Console.ReadLine(), out int borrowedId))
                {
                    bool success = _userService.ReturnBook(user, borrowedId);
                    if (success == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nBook Returned Successfully");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFailed To Return Book.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Book ID.");
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "5":
                _userService.ViewMyBorrowedBooks(user);
                Console.ReadKey();
                break;

            case "6":
                _userService.ViewMyReviews(user);
                Console.ReadKey();
                break;

            case "7":
                try
                {
                    _bookService.ViewAllBooks();
                    Console.Write("\n\nBook ID: ");
                    int rBookId = int.Parse(Console.ReadLine()!);

                    Console.Write("\n\nRating (1–5): ");
                    if (!int.TryParse(Console.ReadLine(), out int rating)
                        || rating < 1
                        || rating > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Rating.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    }

                    Console.Write("\n\nComment: ");
                    string? comment = Console.ReadLine();

                    _userService.AddReview(user, rBookId, comment, rating);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nReview Added Successfully.");
                    Console.ResetColor();
                }
                catch (BookNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "8":
                try
                {
                    _userService.ViewMyReviews(user);
                    Console.Write("\n\nReview ID to Edit: ");
                    int editId = int.Parse(Console.ReadLine()!);

                    Console.Write("\n\nNew Rating: ");
                    if (!int.TryParse(Console.ReadLine(), out int newRating)
                        || newRating < 1
                        || newRating > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Rating Number.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    }

                    Console.Write("\n\nNew Comment: ");
                    string? newComment = Console.ReadLine();

                    _userService.EditReview(user, editId, newComment, newRating);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nReview Updated Successfully.");
                    Console.ResetColor();
                }
                catch (ReviewNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "9":
                try
                {
                    _userService.ViewMyReviews(user);
                    Console.Write("\n\nReview ID to Delete: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    _userService.DeleteReview(user, deleteId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nReview Deleted Successfully.");
                    Console.ResetColor();
                }
                catch (ReviewNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "0":
                Console.ReadKey();
                return;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Input.");
                Console.ResetColor();
                Console.ReadKey();
                break;
        }
    }
}
    


void AdminMenu()
{
    while (true)
    {
        Console.Clear();
        string title = @"
 
     _       _           _         __  __                  
    / \   __| |_ __ ___ (_)_ __   |  \/  | ___ _ __  _   _ 
   / _ \ / _` | '_ ` _ \| | '_ \  | |\/| |/ _ \ '_ \| | | |
  / ___ \ (_| | | | | | | | | | | | |  | |  __/ | | | |_| |
 /_/   \_\__,_|_| |_| |_|_|_| |_| |_|  |_|\___|_| |_|\__,_|
                                                           
                                                
 ";
        Console.WriteLine(title);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.ResetColor();
        Console.WriteLine("________________________________________________");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("1. View All Books");
        Console.WriteLine("2. View All Categories");
        Console.WriteLine("3. View All Reviews");
        Console.WriteLine("4. Add Category");
        Console.WriteLine("5. Add Book");
        Console.WriteLine("6. Delete Review");
        Console.WriteLine("7. Delete Book");
        Console.WriteLine("8. Delete Category");
        Console.WriteLine("0. Logout");
        Console.ResetColor();
        Console.WriteLine("________________________________________________");
        Console.WriteLine();

        string input = Console.ReadLine()!;

        switch (input)
        {
            case "1":
                _bookService.ViewAllBooks();
                Console.ReadKey();
                break;

            case "2":
                _bookService.ViewAllCategories();
                Console.ReadKey();
                break;

            case "3":
                _adminService.ViewAllReviews();
                Console.ReadKey();
                break;

            case "4":
                try
                {
                    _bookService.ViewAllCategories();
                    Console.Write("\n\nCategory Name: ");
                    string categoryName = Console.ReadLine();

                    if (categoryName == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nField Cannot Be Empty.");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    }

                    _adminService.AddCategory(categoryName);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nCategory Added Successfully.");
                    Console.ResetColor();
                }
                catch (CategoryAlreadyExists ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "5":
                Console.Write("\nBook title: ");
                string bookTitle = Console.ReadLine()!;

                if (bookTitle == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nField Cannot Be Empty.");
                    Console.ResetColor();
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine();
                _bookService.ViewAllCategories();

                Console.Write("\n\nCategory ID: ");
                if (int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    _adminService.AddBook(bookTitle, categoryId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nBook Added Successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid Category ID.");
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "6":
                try
                {
                    _adminService.ViewAllReviews();
                    Console.Write("\n\nReview ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int reviewId))
                    {
                        _adminService.DeleteReview(reviewId);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nReview Deleted Successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Review ID.");
                        Console.ResetColor();
                    }
                }
                catch (ReviewNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "7":
                try
                {
                    _bookService.ViewAllBooks();
                    Console.Write("\n\nBook ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int bookId))
                    {
                        _adminService.DeleteBook(bookId);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nBook Deleted Successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Book ID.");
                        Console.ResetColor();
                    }
                }
                catch (BookNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "8":
                try
                {
                    _bookService.ViewAllCategories();
                    Console.Write("\n\nCategory ID to Delete: ");
                    if (int.TryParse(Console.ReadLine(), out int categoryID))
                    {
                        _adminService.DeleteCategory(categoryID);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCategory Deleted Successfully.");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInvalid Category ID.");
                        Console.ResetColor();
                    }
                }
                catch (CategoryNotFoundException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.ResetColor();
                }
                Console.ReadKey();
                break;

            case "0":
                Console.ReadKey();
                return;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid Input.");
                Console.ResetColor();
                Console.ReadKey();
                break;
        }
    }
}