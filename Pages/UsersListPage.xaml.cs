using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using static System.Net.Mime.MediaTypeNames;

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
            //MessageBox.Show(this.Parent.ToString());
            //mainWindow.onLoad();
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

        private void InitialUsers()
        {
            if (users != null)
                users.Clear();

            // Тестовые данные
            if (1 == 1)
            {
                users.Add(new User(1, "debug_mode", 6, 7034.40, new DateTime(2023, 10, 15, 3, 55, 35)));
                users.Add(new User(2, "api_client", 8, 7034.40, new DateTime(2023, 10, 14)));
                users.Add(new User(3, "alex_ivanov_92", 10, 7034.40, new DateTime(2023, 10, 12)));
                users.Add(new User(4, "audit_user", 7, 5432.54, new DateTime(2023, 10, 11)));
                users.Add(new User(5, "finance_demo", 93, 888.9, new DateTime(2023, 10, 10)));
                users.Add(new User(6, "mock_user", 89, 7034.40, new DateTime(2023, 10, 9)));
                users.Add(new User(7, "temp_account", 6, 7034.40, new DateTime(2023, 10, 8)));
                users.Add(new User(8, "john_doe_2023", 2, 7034.40, new DateTime(2023, 10, 7)));
                users.Add(new User(9, "user_backup", 0, 7034.40, new DateTime(2023, 10, 6)));
                users.Add(new User(10, "security_test", 7, 7034.40, new DateTime(2023, 10, 5)));
                users.Add(new User(11, "demo_operator", 9, 7034.40, new DateTime(2023, 10, 4)));
                users.Add(new User(12, "qa_tester", 32, 7034.40, new DateTime(2023, 10, 3)));
                users.Add(new User(13, "guest_access", 9, 7034.40, new DateTime(2023, 10, 2)));
                users.Add(new User(14, "support_temp", 12, 7034.40, new DateTime(2023, 10, 1)));
                users.Add(new User(15, "dev_master", 34, 7034.40, new DateTime(2023, 9, 30)));
                users.Add(new User(16, "admin_demo", 9, 7034.40, new DateTime(2023, 9, 29)));
                users.Add(new User(17, "anna_secure", 7, 73.90, new DateTime(2023, 9, 28)));
                users.Add(new User(18, "test_user_01", 15, 4, new DateTime(2023, 9, 27)));
                users.Add(new User(19, "mariya_smith", 19, 704, new DateTime(2023, 9, 26)));
                users.Add(new User(20, "petr2000", 1, 34.40, new DateTime(2023, 9, 25)));
            }

            //users = userFromDb.GetUsers();
        }


        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //listViewDish.ItemsSource = SearchDish(txbSearch.Text, out count_dish_select);

            string text = TextBoxSearch.Text;
            ObservableCollection<User> temp = new ObservableCollection<User>();

            foreach(User user in users)
            {
                if (user.Login.Contains(text))
                {
                    temp.Add(user);
                }
            }
            InitialItemSource(temp);

        }

        private void typeView_Checked(object sender, RoutedEventArgs e)
        {
            if (grid.IsChecked is true)
            {
                SelectTypeView = 0;
                //MessageBox.Show("grid");
                UserCardScrollViewer.Visibility = Visibility.Visible;
                UserTablePanel.Visibility = Visibility.Collapsed;
                UserListListView.Visibility = Visibility.Collapsed;
            }
            else if (table.IsChecked is true)
            {
                SelectTypeView = 1;
                //MessageBox.Show("table");
                UserTablePanel.Visibility = Visibility.Visible;
                UserListListView.Visibility = Visibility.Collapsed;
                UserCardScrollViewer.Visibility = Visibility.Collapsed;
            }
            else if (list.IsChecked is true)
            {
                SelectTypeView = 2;
                //MessageBox.Show("list");
                UserListListView.Visibility = Visibility.Visible;
                UserTablePanel.Visibility = Visibility.Collapsed;
                UserCardScrollViewer.Visibility = Visibility.Collapsed;
            }
        }

        private void refreshUsers(object sender, RoutedEventArgs e)
        {
            InitialUsers();

            countAllFile.Text = users.Sum(u => u.FileCount).ToString();

            SortUserList.SelectedIndex = -1;

            users = userFromDb.GetUsers();
        }

        private void SortUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortUserList == null) return;
            if (users == null) return;
            if (users.Count == 0 || users.Count == 1) return;

            sortUsersList(SortUserList.SelectedIndex);

            InitialItemSource(users);
        }

        private void sortUsersList(int index)
        {
            if (index == 0)
                users = new ObservableCollection<User>(users.OrderBy(u => u.Login));
            else if (index == 1)
                users = new ObservableCollection<User>(users.OrderByDescending(u => u.Login));
            else if (index == 2)
                users = new ObservableCollection<User>(users.OrderBy(u => u.FileCount));
            else if (index == 3)
                users = new ObservableCollection<User>(users.OrderByDescending(u => u.FileCount));
            else if (index == 4)
                users = new ObservableCollection<User>(users.OrderBy(u => u.AllFileSize));
            else if (index == 5)
                users = new ObservableCollection<User>(users.OrderByDescending(u => u.AllFileSize));
        }

        private void InitialItemSource(ObservableCollection<User> values)
        {
            UserTableListView.ItemsSource = values;
            UserCardsItemsControl.ItemsSource = values;
            UserListListView.ItemsSource = values;
        }

        private void UserSelected(object sender, MouseButtonEventArgs e)
        {
            // логика выбора пользователя из карточек
            if (sender is Border border && border.DataContext is User selectedUser)
            {
                //MessageBox.Show($"Selected user: {selectedUser.Login}");
                User user = selectedUser;
                NavigationService.Navigate(new UserDetailsPage(user));

            }
            // логика выбора пользователя из таблицы
            if (UserTablePanel.Visibility == Visibility.Visible)
            {
                //MessageBox.Show("Table");
                User user = (User)UserTableListView.SelectedItem;
                NavigationService.Navigate(new UserDetailsPage(user));
            }

            // логика выбора пользователя из списка
            if (UserListListView.Visibility == Visibility.Visible)
            {
                //MessageBox.Show("List");
                User user = (User)UserListListView.SelectedItem;
                NavigationService.Navigate(new UserDetailsPage(user));
            }
        }
    }
}