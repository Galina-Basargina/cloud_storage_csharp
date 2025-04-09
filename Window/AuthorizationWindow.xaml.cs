using System;
using System.Windows;
using coursework3.Class;
using coursework3.Model;

namespace coursework3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        UserFromDb userFromDb = new UserFromDb();
        public static User currentUser { get; set; } = null;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        // Проверка авторизации админимтратора
        private void onLogin(object sender, EventArgs e)
        {
            string password = textBoxPsw.Text;
            if (passwordBoxPsw.Visibility == Visibility.Visible)
                password = passwordBoxPsw.Password;
            // Создание подключения к базе данных
            // (делается только 1 раз за всю жизнь программы)
            if (DbConnection.getConnection() == null)
                DbConnection.connect();
            if (textBoxLogin.Text == string.Empty || password == string.Empty)
            {
                MessageBox.Show("Введите логин или пароль");
                passwordBoxPsw.Password = string.Empty;
                textBoxPsw.Text = string.Empty;
            }
            else if (userFromDb.AuthAdmin(textBoxLogin.Text, password))
            {
                MessageBox.Show("Авторизация прошла успешно");
                
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

        // Переключение режимов видимости пароля
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

        // Задание фиксированных значений размера окна приложения
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = 837;
            this.Height = 450;
        }

        // Обработка действий при закрытии приложения
        private void Window_Closed(object sender, EventArgs e)
        {
            DbConnection.disconnect();
        }
    }
}

