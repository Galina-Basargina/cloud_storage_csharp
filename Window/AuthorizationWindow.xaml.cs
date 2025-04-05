using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using coursework3.Class;
using coursework3.Classes;
using coursework3.Model;

namespace coursework3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private const string loginHash = "f871d0d042b502b86208fdf066a1f7d3";
        private const string passwordHash = "ee8f2d74ae14d9018c4b8719de61950e";

        public static User currentUser { get; set; } = null;
        public AuthorizationWindow()
        {
            InitializeComponent();
            textBoxLogin.Text = "am#$4dgC@fhJ";
            passwordBoxPsw.Password = "23j{9ETdcHbk";
        }

        private void onLogin(object sender, EventArgs e)
        {
            string password = textBoxPsw.Text;
            if (passwordBoxPsw.Visibility == Visibility.Visible)
                password = passwordBoxPsw.Password;
            if (textBoxLogin.Text == string.Empty || password == string.Empty)
            {
                MessageBox.Show("Введите логин или пароль");
                passwordBoxPsw.Password = string.Empty;
                textBoxPsw.Text = string.Empty;
            }
            else if (Verification.VerifyPassword(Verification.HashPassword(password), passwordHash) &&
                Verification.VerifyPassword(Verification.HashPassword(textBoxLogin.Text), loginHash))
            {
                MessageBox.Show("Авторизация прошла успешно");
                DbConnection.connect();
                if (DbConnection.getConnection() == null)
                {
                    MessageBox.Show("Не удалось подключиться к базе данных");
                    passwordBoxPsw.Password = string.Empty;
                    textBoxPsw.Text = string.Empty;
                    return;
                }
                MainWindow MainWindow = new MainWindow();
                MainWindow.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Ошибка в логине или пароле");
                passwordBoxPsw.Password = string.Empty;
                textBoxPsw.Text = string.Empty;
            }
        }

        private void openPassword(object sender, RoutedEventArgs e)
        {
            if (passwordBoxPsw.Visibility == Visibility.Collapsed)
            {
                passwordBoxPsw.Visibility = Visibility.Visible;
                textBoxPsw.Visibility = Visibility.Collapsed;
                passwordBoxPsw.Password = textBoxPsw.Text;
            }
            else
            {
                passwordBoxPsw.Visibility = Visibility.Collapsed;
                textBoxPsw.Visibility = Visibility.Visible;
                textBoxPsw.Text = passwordBoxPsw.Password;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = 837;
            this.Height = 450;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DbConnection.disconnect();
        }
    }
}

