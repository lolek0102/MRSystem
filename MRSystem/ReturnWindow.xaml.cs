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
    /// Logika interakcji dla klasy ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=movie_rental;Integrated Security=True;TrustServerCertificate=true";

        public ReturnWindow()
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

        private void FindCardNumber_Click(object sender, RoutedEventArgs e)
        {
            using (MRdbContext db = new MRdbContext(connectionString))
            {
                var query = from u in db.Customers where u.UserCardNumber == tboxCardNumber.Text select u;
                if (query.Count() > 0)
                {
                    cbRent.Items.Clear();
                    Customer customer = db.Customers.Where(u => u.UserCardNumber == tboxCardNumber.Text).First();
                    tbFirst.Text = customer.FirstName;
                    tbLast.Text = customer.LastName;
                    BindCombo();
                }
                else
                {
                    MessageBox.Show("There is no user with such card number!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbFirst.Text = "";
                    tbLast.Text = "";
                    cbRent.Items.Clear();
                }


            }
        }

        private void BindCombo()
        {
            using (MRdbContext db = new MRdbContext(connectionString))
            {
                var query = from rt in db.Borrows where rt.UserCardNumber == Convert.ToInt32(tboxCardNumber.Text) && rt.MovieReturn == null select rt.MovieTitle;
                var listrt = query.ToList();
                for (int i = 0; i < listrt.Count; i++)
                {
                    cbRent.Items.Add(listrt[i]);
                }

            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            if (cbRent.Text != "")
            {
                using (MRdbContext db = new MRdbContext(connectionString))
                {
                    Borrow borrow = db.Borrows.Where(u => u.MovieTitle == cbRent.Text && u.UserCardNumber == Convert.ToInt32(tboxCardNumber.Text)).First();
                    borrow.MovieReturn = DateTime.Now;
                    db.SaveChanges();
                    MessageBox.Show("Movie returned successfuly!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    cbRent.Items.Clear();
                    BindCombo();
                }
            }
            else
            {
                MessageBox.Show("Choose movie to return!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
    }

