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
    /// Logika interakcji dla klasy MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {

        readonly string connectionString = @"Data Source=localhost;Initial Catalog=movie_rental;Integrated Security=True;TrustServerCertificate=true";
        public MainPanel()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (MRdbContext db = new MRdbContext(connectionString))
            {
                var query = from mv in db.Movies select new { mv.Id, mv.Title, mv.Director, mv.Genre, mv.MovieStudio };
                dgMovieView.ItemsSource = query.ToList();
            }

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void borrowBtn_Click(object sender, RoutedEventArgs e)
        {
            BorrowWindow bw = new BorrowWindow();
            bw.Show();
        }

        private void returnBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnWindow rw = new ReturnWindow();
            rw.Show();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
