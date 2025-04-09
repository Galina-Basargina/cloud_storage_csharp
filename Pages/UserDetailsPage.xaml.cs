using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using coursework3.Class;
using coursework3.Classes;
using coursework3.Model;

namespace coursework3.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserDetailsPage.xaml
    /// </summary>
    public partial class UserDetailsPage : Page
    {
        User user = null;
        ObservableCollection<File> files = new ObservableCollection<File>();
        ObservableCollection<Auth> auths = new ObservableCollection<Auth>();
        DetailUserFromDb detailUserFromDb = new DetailUserFromDb();

        public UserDetailsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            // Вывод информации на страницу
            // прогрузка файлов и дат из базы данных
            TextBoxLogin.Text = user.Login;
            if (user.Login.Length > 32)
                TextBoxLogin.Text = user.Login.Substring(0, 32-6) + "...";
            auths.Clear();
            files = detailUserFromDb.GetFiles(user.UserId);
            FileListListView.ItemsSource = files;
            auths.Clear();
            auths = detailUserFromDb.GetAuths(user.UserId);
            AuthListListView.ItemsSource = auths;
            TextBoxCurrentSession.Text = user.LastSession.ToString();
            if (user.LastSession.ToString() == "01.01.0001 0:00:00")
                TextBoxCurrentSession.Text = "Не сохранено";
            TextBoxAllFileSize.Text = user.AllFileSize.ToString() + " ГБ";
            TextBoxFileCount.Text = "Всего файлов: " + user.FileCount.ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, возможен ли переход назад
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                // Альтернативное поведение, если история навигации пуста
                MessageBox.Show("Невозможно вернуться назад");
        }

        // Удаление данных о авторизаций пользователя
        private void onLogoff(object sender, RoutedEventArgs e)
        {
            detailUserFromDb.DeleteAuth(user.UserId);

            auths = new ObservableCollection<Auth>();
            AuthListListView.ItemsSource = auths;
        }
    }
}
