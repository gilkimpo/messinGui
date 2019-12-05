using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Team> team = new List<Team>();
        public List<Function> function = new List<Function>();
        List<Book> books = Book.BookList;
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Whaddup suckas!";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //just making list and class in main for now rather than creating class for itself

            team.Add(new Team { FirstName = "Joseph" });
            team.Add(new Team { FirstName = "Randy" });
            team.Add(new Team { FirstName = "Gina" });
            team.Add(new Team { FirstName = "Gil" });
            mycombox.ItemsSource = books;

            function.Add(new Function { DoSomethingPlease = "This should do something" });
            function.Add(new Function { DoSomethingPlease = "i wish this would do somthing too" });
            function.Add(new Function { DoSomethingPlease = "guess what i'm thinking..." });
            otherbox.ItemsSource = function;
        }
        public class Function
        {
            public string DoSomethingPlease { get; set; }
        }
        public class Team
        {
            public string FirstName { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mmsg= MessageBox.Show("u wanna check out book?", "hello", MessageBoxButton.YesNoCancel);
            if (mmsg==MessageBoxResult.Yes)
            {
                //this is all practice stuff, not very good execution of logic
                Close();
            }
            else
            {
                
            }                       
                   
        }
       //testing how to get mult buttons on

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("u wanna return book", "HI again", MessageBoxButton.YesNoCancel);
            
        }
        class Book
        {
            public string title;
            public string author;
            public int status;
            public string dueDate;
            public static List<Book> BookList = Book.TxtToBook(); //Creates a Static List

            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            public string Author
            {
                get { return author; }
                set { author = value; }
            }

            public int Status
            {
                get { return status; }
                set { status = value; }
            }

            public string DueDate
            {
                get { return dueDate; }
                set { dueDate = value; }
            }



            public Book() { }


            public Book(string title, string author, int status)
            {
                Title = title;
                Author = author;
                Status = status;
            }

            public Book(string title, string author, int status, string dueDate)
            {
                Title = title;
                Author = author;
                Status = status;
                DueDate = dueDate;
            }

            //
            public static void BookToTxtFile(List<Book> books)
            {
                StreamWriter bks = new StreamWriter(@"C:\Users\gilki\source\repos\WpfApp1\WpfApp1\booklist.txt");

                foreach (Book book in books)
                {
                    string csv = "";
                    if (book.Status == 1)
                    {
                        csv = $"{book.Title},{book.Author},{book.Status},{book.DueDate}";
                    }
                    else if (book.Status == 0)
                    {
                        csv = $"{book.Title},{book.Author},{book.Status}";
                    }
                    bks.WriteLine(csv);
                }
                bks.Close();
            }
            //This Method Takes the Textfile 
            public static List<Book> TxtToBook()
            {
                List<Book> tempBookList = new List<Book>();

                List<string> bkList = new List<string>();

                StreamReader sr = new StreamReader(@"C:\Users\gilki\source\repos\WpfApp1\WpfApp1\booklist.txt");

                string line = sr.ReadLine();

                while (line != null)
                {
                    bkList.Add(line);
                    line = sr.ReadLine();
                }

                foreach (string bk in bkList)
                {
                    string[] bkArray = bk.Split(',');
                    if (bkArray[2] == "1")
                    {
                        tempBookList.Add(new Book(bkArray[0], bkArray[1], 1, bkArray[3]));
                    }
                    else if (bkArray[2] == "0")
                    {
                        tempBookList.Add(new Book(bkArray[0], bkArray[1], 0));
                    }
                }
                sr.Close();
                return tempBookList;
            }




        }

        class Search : Book
        {


            public static Book GetBookByAuthorName(string titleByAuthor)
            {
                foreach (Book b in BookList)
                {
                    if (b.Author.Contains(titleByAuthor))
                    {
                        return b;  // returns book found by author name
                    }
                }
                //if we are here we did not find the book; so return null
                return null;

            }

            public static Book GetBookByKeyword(string titleByKeyword)
            {
                foreach (Book b in BookList)
                {
                    if (b.Title.Contains(titleByKeyword))
                    {
                        return b;
                    }
                }
                return null;
            }

            public static List<Book> GetBookListByKeyword(string titleByKeyword)
            {
                List<Book> MatchingBooks = new List<Book>();

                foreach (Book b in BookList)
                {
                    if (b.Title.Contains(titleByKeyword))
                    {
                        MatchingBooks.Add(b);
                    }
                }
                return MatchingBooks;
            }

        }

        class Validator : Book
        {

            //Validates user input in beginning of program 
            public static int inputCheck()
            {
                int input = 0;
                bool repeat = true;
                while (repeat == true)
                {
                    try
                    {
                        input = int.Parse(Console.ReadLine());
                        if (input == 1 || input == 2 || input == 3 || input == 4 || input == 5 || input == 6)
                        {
                            repeat = false;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid input (1-5)");
                            input = int.Parse(Console.ReadLine());
                        }
                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please enter a valid input (1-5)");

                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("Please enter a valid input (1-5)");

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Please enter a valid input (1-5)");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please enter a valid input (1-5)");

                    }
                }
                return input;
            }
            //Validates whenever you need to check for Author
            public static string ValidateAuthor(string author, List<Book> books)
            {
                bool check = false;
                while (check == false)
                {
                    try
                    {
                        foreach (Book b in books)
                        {
                            if (b.Author.Contains(author))
                            {
                                check = true;
                            }
                        }
                        if (check == false)
                        {
                            Console.WriteLine("Please enter a valid author");
                            author = Console.ReadLine();
                        }

                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please enter a valid author");
                        author = Console.ReadLine();
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("Please enter a valid author");
                        author = Console.ReadLine();
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine("Please enter a valid author");
                        author = Console.ReadLine();
                    }
                }
                return author;
            }

            public static string ValidateAuthor()
            {
                Regex authorValid = new Regex(@"[A-Za-z\s\.]");
                string author = "";
                bool check = false;
                while (check == false)
                {
                    author = Console.ReadLine();
                    if (string.IsNullOrEmpty(author))
                    {
                        Console.WriteLine("Please Enter a proper name.");
                    }
                    else if (!authorValid.IsMatch(author))
                    {
                        Console.WriteLine("Please Enter a proper name.");
                    }
                    else if (authorValid.IsMatch(author))
                    {
                        check = true;
                    }
                }
                return author;
            }
            //Validates whenever you need to check for title
            public static string ValidateTitle(string title, List<Book> books)
            {
                bool check = false;
                while (check == false)
                {
                    try
                    {

                        foreach (Book b in books)
                        {
                            if (b.Title.Contains(title))
                            {
                                check = true;
                            }
                        }
                        if (check == false)
                        {
                            Console.WriteLine("Please enter a valid title");
                            title = Console.ReadLine();
                        }
                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please enter a valid title");
                        title = Console.ReadLine();
                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("Please enter a valid title");
                        title = Console.ReadLine();
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine("Please enter a valid title");
                        title = Console.ReadLine();
                    }
                }
                return title;
            }

            public static string inputCheck(string input)
            {
                bool repeat = true;
                while (repeat == true)
                {
                    try
                    {
                        if (input == "yes" || input == "no" || input == "y" || input == "n")
                        {
                            repeat = false;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid input (yes or no)");
                            input = Console.ReadLine();
                        }
                    }

                    catch (FormatException ex)
                    {
                        Console.WriteLine("Please enter a valid input (yes or no)");

                    }
                    catch (ArgumentNullException ex)
                    {
                        Console.WriteLine("Please enter a valid (yes or no)");

                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Please enter a valid input (yes or no)");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Please enter a valid input (yes or no))");

                    }
                }
                return input;
            }

            public static string ValidateTitle()
            {
                string response = "";
                bool check = false;
                while (check == false)
                {
                    response = Console.ReadLine();
                    if (string.IsNullOrEmpty(response))
                    {
                        Console.WriteLine("Please enter a valid title");
                        //response = Console.ReadLine();
                    }
                    else
                    {
                        check = true;
                    }
                }
                return response;
            }

        }

        class Menu : Book
        {
            //Main Menu
            //Display List?
            //-----Checkout Method
            //Search by Keyword?
            //-----Checkout Method
            //Search by Author?
            //-----Checkout Method
            //Return a Book

            public static void DisplayBooks(List<Book> bookList)
            {

                Console.WriteLine($"{"Title",-40}{"Author",-25}{"Availability",-20} {"Due Date",-20}\n------------------------------------------------------------------------------------------------");
                foreach (Book book in bookList)
                {
                    string available = "";
                    string tooLong = "";
                    if (book.Status == 0)
                    {
                        available = "On Shelf";
                    }
                    else if (book.Status == 1)
                    {
                        available = "Checked Out";
                    }

                    if (book.Title.Length > 25 && available == "On Shelf")
                    {
                        tooLong = $"{book.Title.Substring(0, 24)}...";
                        Console.WriteLine($"{tooLong,-40}{book.Author,-25}{available,-20}");
                    }
                    else if (book.Title.Length > 25 && available == "Checked Out")
                    {
                        tooLong = $"{book.Title.Substring(0, 24)}...";
                        Console.WriteLine($"{tooLong,-40}{book.Author,-25}{available,-20}{book.DueDate,-20}");
                    }
                    else if (available == "Checked Out")
                    {
                        Console.WriteLine($"{book.Title,-40}{book.Author,-25}{available,-20}{book.DueDate,-20}");
                    }
                    else
                    {
                        Console.WriteLine($"{book.Title,-40}{book.Author,-25}{available,-20}");

                    }
                }
            }

            public static void CheckoutBook(Book book)
            {
                book.Status = 1;
                book.DueDate = DateTime.Now.AddDays(14).ToString("MM/dd/yyyy");

            }

            public static void ReturnBook(Book book)
            {
                book.Status = 0;
                book.DueDate = null;

            }

            public static void Options()
            {
                BookList.Sort((a, b) => a.Title.CompareTo(b.Title));
                Console.WriteLine("\nWelcome to the library. Please choose a number from the following menu");
                Console.WriteLine("1. Search for a book to check out by AUTHOR NAME.");
                Console.WriteLine("2. Search for a book to check out by a WORD IN THE TITLE.");
                Console.WriteLine("3. Return a book.");
                Console.WriteLine("4. Display Current Book List.");
                Console.WriteLine("5. Donate a Book.");
                Console.WriteLine("6. Exit the library app");
            }

            public static void StartMenu()
            {
                Options();
                int optionInput = Validator.inputCheck();

                while (optionInput != 6)
                {
                    if (optionInput == 1)
                    {
                        #region CALL METHOD to check out book by Author Name
                        Console.WriteLine("Choose a book to check out by the Author's Name.");

                        string reply = Console.ReadLine();
                        string userInput = Validator.ValidateAuthor(reply, BookList);

                        Book myBook = Search.GetBookByAuthorName(userInput);

                        Console.WriteLine($"\nFOUND BOOK BY {myBook.Author}: {myBook.Title}");


                        if (myBook.Status == 0)  // 0 = not checked out
                        {
                            Console.WriteLine($"\nDo you want to check out {myBook.Title} by {myBook.Author}?");

                            string wantCheckOut = Validator.inputCheck(Console.ReadLine());
                            if (wantCheckOut == "yes")
                            {
                                Menu.CheckoutBook(myBook);
                                Console.WriteLine($"\nYOU ARE CHECKING OUT: {myBook.Title} by {myBook.Author}\nThe due date for {myBook.Title} is: {myBook.DueDate}");
                                Book.BookToTxtFile(BookList);
                            }
                            else
                            {
                                Console.WriteLine("Ok");
                            }

                        }
                        else  //1 = book is already checked out 
                        {
                            Console.WriteLine($"\n{myBook.Title} is currently checked out. It is due back on {myBook.DueDate}.");
                        }

                        #endregion
                    }
                    else if (optionInput == 2)
                    {
                        #region CALL METHOD to GetBookListByKeyword
                        Console.WriteLine("Choose a book to check out by a word or letter in the book's title.");
                        string reply = Console.ReadLine();
                        string bookSearch = Validator.ValidateTitle(reply, BookList);
                        List<Book> MatchingBooklist = Search.GetBookListByKeyword(bookSearch);

                        if (MatchingBooklist != null && MatchingBooklist.Count > 0)
                        {
                            Console.WriteLine($"\nBooks matching your search term '{bookSearch}' are listed below:\n");

                            DisplayBooks(MatchingBooklist);

                            foreach (Book b in MatchingBooklist)
                            {

                                if (b.Status == 0)
                                {
                                    Console.WriteLine($"\nWould you like to check out {b.Title}? yes or no");
                                    string userReply = Console.ReadLine();
                                    userReply = Validator.inputCheck(userReply);

                                    if (userReply == "yes" || userReply == "y")
                                    {
                                        Menu.CheckoutBook(b);
                                        Console.WriteLine($"\nYou are checking out: {b.Title}.\nThe due date for {b.Title} is: {DateTime.Now.AddDays(14).ToString("MM/dd/yyyy")}");
                                    }
                                    else if (userReply == "no" || userReply == "n")
                                    {
                                        Console.WriteLine($"\n{b.Title} not checked out.");
                                    }
                                }
                                Book.BookToTxtFile(BookList);

                            }

                        }
                        #endregion
                    }
                    else if (optionInput == 3)
                    {
                        #region RETURN BOOK
                        Console.WriteLine("Please enter the Title of the book you are returning?");
                        string bookReturn = Console.ReadLine();

                        string bookReturned = Validator.ValidateTitle(bookReturn, BookList);

                        List<Book> returnBook = Search.GetBookListByKeyword(bookReturned);
                        Console.WriteLine($"\nThe Books matching your query of '{bookReturned}' are below:\n");
                        DisplayBooks(returnBook);
                        foreach (Book b in returnBook)
                        {

                            if (b.Status == 1)
                            {
                                Console.WriteLine($"\nWould you like to Return {b.Title}? yes or no");
                                string userReply = Console.ReadLine();
                                userReply = Validator.inputCheck(userReply);


                                if (userReply == "yes" || userReply == "y")
                                {
                                    Menu.ReturnBook(b);
                                    Console.WriteLine($"\n{b.Title} returned.");

                                }
                                else if (userReply == "no" || userReply == "n")
                                {
                                    Console.WriteLine($"\n{b.Title} will not be returned.\nBook is due {b.DueDate}.");
                                }
                            }

                            Book.BookToTxtFile(BookList);

                        }

                    }
                    #endregion
                    else if (optionInput == 4)
                    {
                        DisplayBooks(BookList);
                    }
                    else if (optionInput == 5)
                    {
                        Menu.AddBook();
                    }

                    Options();
                    optionInput = Validator.inputCheck();

                }


            }

            public static void AddBook()
            {
                Console.WriteLine("Would you like to donate a book? (Yes or No)");
                string reply = Validator.inputCheck(Console.ReadLine());
                if (reply == "yes" || reply == "y")
                {
                    Console.WriteLine("What is the title?");
                    string title = Validator.ValidateTitle();
                    Console.WriteLine("Who is the author?");
                    string author = Validator.ValidateAuthor();
                    BookList.Add(new Book(title, author, 0));
                }
                else if (reply == "no" || reply == "n")
                {
                    Console.WriteLine("Scruffy Nerf Herder");
                }

                BookToTxtFile(BookList);

            }
        }




        
        private void mycombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ind = mycombox.SelectedIndex;
            MessageBoxResult mmsg = MessageBox.Show("u wanna check out book?", "hello", MessageBoxButton.YesNoCancel);
            if(mmsg == MessageBoxResult.Yes)
            {
                
                Menu.CheckoutBook(Book.BookList[ind]);
                Book.BookToTxtFile(Book.BookList);
            }
            mycombox.Items.Refresh();

        }
    }
}
