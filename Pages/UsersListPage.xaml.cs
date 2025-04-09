using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using coursework3.Class;
using coursework3.Model;

namespace coursework3.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class UsersListPage : Page
    {
        UserFromDb userFromDb = new UserFromDb();
        ObservableCollection<User> users = new ObservableCollection<User>();
        private static int SelectTypeView = 1;

        public UsersListPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InitialUsers();
            switch (SelectTypeView)
            {
                case 0: grid.IsChecked = true; break;
                case 1: table.IsChecked = true; break;
                case 2: list.IsChecked = true; break;
            }
            InitialItemSource(users);
            if (users != null)
                countAllFile.Text = users.Sum(u => u.FileCount).ToString();
        }

        // Получение списка пользователей из базы данных
        private void InitialUsers()
        {
            if (users != null)
                users.Clear();
            users = userFromDb.GetUsers();
        }

        // Поиск пользователя по логину
        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = TextBoxSearch.Text;
            ObservableCollection<User> temp = new ObservableCollection<User>();

            foreach(User user in users)
                if (user.Login.Contains(text))
                    temp.Add(user);

            InitialItemSource(temp);
        }

        // Переключение режимов отображения пользователей (карточки, таблица, список)
        private void typeView_Checked(object sender, RoutedEventArgs e)
        {
            if (grid.IsChecked is true)
            {
                SelectTypeView = 0;
                UserCardScrollViewer.Visibility = Visibility.Visible;
                UserTableScroll.Visibility = Visibility.Collapsed;
                UserListListView.Visibility = Visibility.Collapsed;
            }
            else if (table.IsChecked is true)
            {
                SelectTypeView = 1;
                UserTableScroll.Visibility = Visibility.Visible;
                UserListListView.Visibility = Visibility.Collapsed;
                UserCardScrollViewer.Visibility = Visibility.Collapsed;
            }
            else if (list.IsChecked is true)
            {
                SelectTypeView = 2;
                UserListListView.Visibility = Visibility.Visible;
                UserTableScroll.Visibility = Visibility.Collapsed;
                UserCardScrollViewer.Visibility = Visibility.Collapsed;
            }
        }

        private void refreshUsers(object sender, RoutedEventArgs e)
        {
            InitialUsers();
            countAllFile.Text = users.Sum(u => u.FileCount).ToString();
            SortUserList.SelectedIndex = -1;
            InitialItemSource(users);
        }

        // Сортировка списка пользователей
        private void SortUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortUserList == null) return;
            if (users == null) return;
            if (users.Count == 0 || users.Count == 1) return;

            switch (SortUserList.SelectedIndex)
            {
                case 0:  // Сортировка по логину, по алфовиту
                    users = new ObservableCollection<User>(
                        users.OrderBy(u => u.Login));
                    break;
                case 1:  // Сортировка по логину, наоборот
                    users = new ObservableCollection<User>(
                        users.OrderByDescending(u => u.Login));
                    break;
                case 2:  // Сортировка по количеству файлов, возр.
                    users = new ObservableCollection<User>(
                        users.OrderBy(u => u.FileCount));
                    break;
                case 3:  // Сортировка по количеству файлов, убыв.
                    users = new ObservableCollection<User>(
                        users.OrderByDescending(u => u.FileCount));
                    break;
                case 4:  // Сортировка по объему памяти, возр.
                    users = new ObservableCollection<User>(
                        users.OrderBy(u => u.AllFileSize));
                    break;
                case 5:  // Сортировка по объему памяти, убыв.
                    users = new ObservableCollection<User>(
                        users.OrderByDescending(u => u.AllFileSize));
                    break;
            }

            InitialItemSource(users);
        }

        // Определение ресурсов списков пользователей
        private void InitialItemSource(ObservableCollection<User> values)
        {
            UserTableListView.ItemsSource = values;
            UserCardsItemsControl.ItemsSource = values;
            UserListListView.ItemsSource = values;
        }

        // Метод, срабатывающий при выборе пользователя
        // переводит на другую страницу с деталями выбранного пользователя
        private void UserSelected(object sender, MouseButtonEventArgs e)
        {
            User user = null;
            if (sender is Border border && border.DataContext is User selectedUser)
                user = selectedUser;
            else if (UserTableScroll.Visibility == Visibility.Visible)
                user = (User)UserTableListView.SelectedItem;
            else 
                user = (User)UserListListView.SelectedItem;
            NavigationService.Navigate(new UserDetailsPage(user));
        }
    }
}