﻿using System;
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
using System.Security.Cryptography;
using MRSystem.Models;

namespace MRSystem
{
    /// <summary>
    /// Logika interakcji dla klasy SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public SignUp()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            if (tboxUsername.Text.Length < 3)
            {
                MessageBox.Show("Login is too short! At least 3 characters.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (policy.IsValid(boxPassword.Password))
                {
                    string connectionString = @"Data Source=localhost;Initial Catalog=movie_rental;Integrated Security=True;TrustServerCertificate=true";
                    using (MRdbContext db = new MRdbContext(connectionString))
                    {
                        var query = from u in db.Admins where u.Username == tboxUsername.Text select u;
                        if (query.Count() > 0)
                        {
                            MessageBox.Show("Login already in use!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            if (boxPassword.Password != boxPassword2.Password)
                            {
                                MessageBox.Show("Passwords are different!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                var pass = GetHashString(boxPassword.Password);

                                db.Add(new Admin
                                {
                                    Username = tboxUsername.Text,
                                    Password = pass
                                });
                                db.SaveChanges();
                                MessageBox.Show("Sign up successful!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Password is too weak! Check our password policy.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
