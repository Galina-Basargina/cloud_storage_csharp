using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        AuthFromDb authFromDb = new AuthFromDb();

        public UserDetailsPage(User user)
        {
            InitializeComponent();
            this.user = user;
            TextBoxLogin.Text = user.Login;
            if (user.Login.Length > 32)
                TextBoxLogin.Text = user.Login.Substring(0, 32-6) + "...";
            InitialFiles();
            FileListListView.ItemsSource = files;
            InitialAuths();
            AuthListListView.ItemsSource = auths;
            TextBoxCurrentSession.Text = user.LastSession.ToString();
            TextBoxAllFileSize.Text = user.AllFileSize.ToString() + " MB";
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

        private void DeleteAuth(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(AuthListListView.SelectedItems.Count.ToString());
            ObservableCollection<Auth> list_temp = new ObservableCollection<Auth>();
            foreach(Auth temp in AuthListListView.SelectedItems)
                list_temp.Add(temp);
            authFromDb.DeleteListAuth(list_temp);
        }


        private void InitialFiles()
        {
            auths.Clear();
            // Тестовые данные
            if (1 == 1)
            {
                // Изображения
                files.Add(new File(1, "vacation_photo.jpg", 4.2, "image/jpeg", new DateTime(2023, 9, 1)));
                files.Add(new File(2, "company_logo.png", 1.8, "image/png", new DateTime(2023, 9, 2)));

                // Документы
                files.Add(new File(3, "project_report.docx", 2.5, "application/document", new DateTime(2023, 9, 3)));
                files.Add(new File(4, "financial_plan.pdf", 3.1, "application/pdf", new DateTime(2023, 9, 4)));

                // Медиа
                files.Add(new File(5, "birthday_video.mp4", 158.7, "video/mp4", new DateTime(2023, 9, 5)));
                files.Add(new File(6, "podcast_episode.mp3", 45.3, "audio/mpeg", new DateTime(2023, 9, 6)));

                // Архивы
                files.Add(new File(7, "backup_2023.zip", 89.6, "application/zip", new DateTime(2023, 9, 7)));
                files.Add(new File(8, "database_dump.rar", 204.9, "application/x-rar-compressed", new DateTime(2023, 9, 8)));

                // Системные файлы
                files.Add(new File(9, "system_logs.txt", 0.8, "text/plain", new DateTime(2023, 9, 9)));
                files.Add(new File(10, "config_settings.ini", 0.1, "text/plain", new DateTime(2023, 9, 10)));

                // Дополнительные примеры
                files.Add(new File(11, "presentation_slides.pptx", 12.4, "application/presentation", new DateTime(2023, 9, 11)));
                files.Add(new File(12, "mobile_app.apk", 78.2, "application/vnd.android.package-archive", new DateTime(2023, 9, 12)));
                files.Add(new File(13, "game_assets.bundle", 512.0, "application/octet-stream", new DateTime(2023, 9, 13)));
            }
        }


        private void InitialAuths()
        {
            auths.Clear();
            // Тестовые данные
            if (1 == 1)
            {
                auths.Add(new Auth("2021-06-26 11:09:40.000"));
                auths.Add(new Auth("2024-07-14 11:38:53.800"));
                auths.Add(new Auth("2025-02-20 02:10:12.980"));
                auths.Add(new Auth("2022-03-10 08:43:23.402"));
                auths.Add(new Auth("2025-03-30 05:43:42.627"));
            }
            auths.OrderBy(a => a.AuthDate);
        }

        private void onLogoff(object sender, RoutedEventArgs e)
        {
            authFromDb.DeleteListAuth(auths);
        }
    }
}
