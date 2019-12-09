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
        List<Book> books = Book.BookList;
        List<Book> currentList = new List<Book>();
        List<Book> clear = new List<Book>();
        public MainWindow()
        {
            InitializeComponent();
           
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        class Book
        {
            public string title;
            public string author;
            public string status;
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

            public string Status
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


            public Book(string title, string author, string status)
            {
                Title = title;
                Author = author;
                Status = status;
            }

            public Book(string title, string author, string status, string dueDate)
            {
                Title = title;
                Author = author;
                Status = status;
                DueDate = dueDate;
            }

            //
            public static void BookToTxtFile(List<Book> books)
            {
                StreamWriter bks = new StreamWriter(@"..\..\..\booklist.txt");
                var orderedBookList = books.OrderBy(b => b.Title).ToList(); // Orders books by alpha in Combobox
                foreach (Book book in books)
                {
                    string csv = "";
                    if (book.Status == "Checked Out")
                    {
                        csv = $"{book.Title},{book.Author},{book.Status},{book.DueDate}";
                    }
                    else if (book.Status == "On Shelf")
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

                StreamReader sr = new StreamReader(@"..\..\..\booklist.txt");

                string line = sr.ReadLine();

                while (line != null)
                {
                    bkList.Add(line);
                    line = sr.ReadLine();
                }

                foreach (string bk in bkList)
                {
                    string[] bkArray = bk.Split(',');
                    if (bkArray[2] == "Checked Out")
                    {
                        tempBookList.Add(new Book(bkArray[0], bkArray[1], "Checked Out", bkArray[3]));
                    }
                    else if (bkArray[2] == "On Shelf")
                    {
                        tempBookList.Add(new Book(bkArray[0], bkArray[1], "On Shelf"));
                    }
                }
                sr.Close();
                return tempBookList;
            }

        }

        class Search : Book
        {


            public static List<Book> GetBookByAuthorName(string titleByAuthor)
            {
                List<Book> MatchingBooks = new List<Book>();

                foreach (Book b in BookList)
                {
                    if (b.Author.Contains(titleByAuthor))
                    {
                        MatchingBooks.Add(b);
                    }
                }
                return MatchingBooks;

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
       
            //Validates whenever you need to check for Author
            public static bool ValidateAuthor(string author, List<Book> books)
            {

                try
                {
                    foreach (Book b in books)
                    {
                        if (b.Author.Contains(author))
                        {
                            return true;
                        }
                    }
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);

                }
                return false;

            }

            public static bool ValidateAuthor(string author)
            {
                Regex authorValid = new Regex(@"^[A-Za-z\s\.]+$");


                if (string.IsNullOrEmpty(author))
                {
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);
                }
                else if (!authorValid.IsMatch(author))
                {
                    MessageBox.Show("Please Enter a Valid Author", "Invalid Input", MessageBoxButton.OK);
                }
                else if (authorValid.IsMatch(author))
                {
                    return true;
                }
                return false;
            }
            public static bool ValidateTitle(string title)
            {
                try
                {
                    if (string.IsNullOrEmpty(title))
                    {
                        MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);
                        return false;
                    }
                   
                }

                catch (FormatException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);

                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);
                }

                return true;
            }


            //Validates whenever you need to check for title
            public static bool ValidateTitle(string title, List<Book> books)
            {

                try
                {

                    foreach (Book b in books)
                    {
                        if (b.Title.Contains(title))
                        {
                            return true;
                        }
                    }
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);
                }

                catch (FormatException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);

                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);

                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Please Enter a Valid Title", "Invalid Input", MessageBoxButton.OK);
                }

                return false;
            }

        }

        class Menu : Book
        {

            public static void CheckoutBook(Book book)
            {
                book.Status = "Checked Out";
                book.DueDate = DateTime.Now.AddDays(14).ToString("MM/dd/yyyy");

            }

            public static void ReturnBook(Book book)
            {
                book.Status = "On Shelf";
                book.DueDate = null;

            }

        }

        private void Display_List(object sender, RoutedEventArgs e)
        {
            currentList = books.OrderBy(b => b.Title).ToList();
            mycombox.ItemsSource = currentList;
            mycombox.Items.Refresh();
        }
    
        private void Author_Search(object sender, RoutedEventArgs e)
        {
            mycombox.Items.Refresh();
            var text = AuthorSearch.Text;
            if (Validator.ValidateAuthor(text, Book.BookList))
            {
                currentList = Search.GetBookByAuthorName(text);
                mycombox.ItemsSource = currentList;
            }
            AuthorSearch.Text = "";

        }
        private void Keyword_Search(object sender, RoutedEventArgs e)
        {
            mycombox.Items.Refresh();
            var text = KeywordSearch.Text;
            if (Validator.ValidateTitle(text, Book.BookList))
            {
                currentList = Search.GetBookListByKeyword(text);
                mycombox.ItemsSource = currentList;
            }
            KeywordSearch.Text = "";

        }

        private void Donate_Book(object sender, RoutedEventArgs e)
        {
            if (Validator.ValidateTitle(AddTitle.Text) && Validator.ValidateAuthor(AddAuthor.Text))
            {
                Book.BookList.Add(new Book(AddTitle.Text, AddAuthor.Text, "On Shelf"));
                Book.BookToTxtFile(Book.BookList);
                //mycombox.Items.Refresh();  --COMMENTED THIS OUT BECAUSE IS IN LINE 706 BELOW
                MessageBoxResult donatedBook = MessageBox.Show("Thank you for your donation!", "   ", MessageBoxButton.OK);
                currentList = books.OrderBy(b => b.Title).ToList();
            }
           
            // TITLE IN ALPHA ORDER IN COMBO BOX
            mycombox.ItemsSource = currentList;
            mycombox.Items.Refresh();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            
            teamPic.Visibility = Visibility.Visible;       
            MessageBoxResult exit = MessageBox.Show("Goodbye!", "   ", MessageBoxButton.OK);
            Close();
        }

        private void Clear_Display(object sender, RoutedEventArgs e)
        {
            currentList = clear;
            mycombox.ItemsSource = currentList;
            mycombox.Items.Refresh();
           
        }

        private void mycombox_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            mycombox.Items.Refresh();
            int ind = mycombox.SelectedIndex;
            if (currentList[ind].Status == "On Shelf")
            {
                MessageBoxResult mmsg = MessageBox.Show("Would you like to check out a book?", "Hello!", MessageBoxButton.YesNoCancel);
                if (mmsg == MessageBoxResult.Yes)
                {
                    Menu.CheckoutBook(currentList[ind]);
                    Book.BookToTxtFile(Book.BookList);
                    mycombox.Items.Refresh();
                    MessageBoxResult checkedoutBook = MessageBox.Show($"You have checked out {currentList[ind].title}  ", "Thank you!", MessageBoxButton.OK);
                }

                else if (mmsg == MessageBoxResult.No)
                {
                    mycombox.Items.Refresh();
                }
                else if (mmsg == MessageBoxResult.Cancel)
                {
                    mycombox.Items.Refresh();
                }

            }
            else if (currentList[ind].Status == "Checked Out")
            {
                MessageBoxResult mmsg = MessageBox.Show("Do you want to return the book?", "Hello", MessageBoxButton.YesNoCancel);
                if (mmsg == MessageBoxResult.Yes)
                {
                    Menu.ReturnBook(currentList[ind]);
                    Book.BookToTxtFile(Book.BookList);
                    mycombox.Items.Refresh();
                    MessageBoxResult returnedBook = MessageBox.Show($"You have now returned {currentList[ind].title}", "Thank you!", MessageBoxButton.OK);


                }
            }
            mycombox.Items.Refresh();
        }
    }
}



