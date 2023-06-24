using MRSystem.Models;
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
using System.Windows.Shapes;

namespace MRSystem
{
    /// <summary>
    /// Logika interakcji dla klasy BorrowWindow.xaml
    /// </summary>
    public partial class BorrowWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=movie_rental;Integrated Security=True;TrustServerCertificate=true";

        public BorrowWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindCard_Click(object sender, RoutedEventArgs e)
        {
            using (MRdbContext db = new MRdbContext(connectionString))
            {
                var query = from u in db.Customers where u.UserCardNumber == tboxFindCard.Text select u;
                if (query.Count() > 0)
                {
                    Customer customer = db.Customers.Where(u => u.UserCardNumber == tboxFindCard.Text).First();
                    tbFirst.Text = customer.FirstName;
                    tbLast.Text = customer.LastName;
                    tbAddress.Text = customer.Address + " " + customer.City;
                    tbEmail.Text = customer.Email;
                    tbPhoneNumber.Text = customer.PhoneNumber;
                }
                else
                {
                    MessageBox.Show("There is no customer with such usercard number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbFirst.Text = "";
                    tbLast.Text = "";
                    tbAddress.Text = "";
                    tbPhoneNumber.Text = "";
                    tbEmail.Text = "";
                }


            }
        }

        private void btnBorrow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
